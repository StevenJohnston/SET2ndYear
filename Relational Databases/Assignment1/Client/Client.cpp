#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>
#include <string>
#include <iostream>
#include <time.h>
#include <regex>


// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib
#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")


#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"

using namespace std;

struct MemberRecord
{
	int memberId;
	char firstName[32];
	char lastName[32];
	char dOB[11];
}typedef MemberRecord;

struct ServerCall
{
	int callType;
	MemberRecord member;
} typedef ServerCall;

struct ClientCall
{
	int error;
	char message[64];
	MemberRecord member;
} typedef ClientCall;

bool insertMany(SOCKET ConnectSocket, int quantity);
int getNum(int min, int max);
void randomServerCall(ServerCall *message); 
bool findMember(SOCKET ConnectSocket, int memberId);
bool updateMember(SOCKET ConnectSocket, MemberRecord member);

int __cdecl main(int argc, char **argv)
{
	srand(time(NULL));

	ServerCall tempMessage = { 1,1,"steven","johnston","9/21/2015" };
	char* toSend = (char*)&tempMessage;

	WSADATA wsaData;
	SOCKET ConnectSocket = INVALID_SOCKET;
	struct addrinfo *result = NULL,
		*ptr = NULL,
		hints;
	char *sendbuf = "this is a test55";
	char recvbuf[DEFAULT_BUFLEN];
	int iResult;
	int recvbuflen = DEFAULT_BUFLEN;

	// Validate the parameters
	if (argc != 2) {
		printf("usage: %s server-name\n", argv[0]);
		return 1;
	}

	// Initialize Winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed with error: %d\n", iResult);
		return 1;
	}

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	// Resolve the server address and port
	iResult = getaddrinfo(argv[1], DEFAULT_PORT, &hints, &result);
	if (iResult != 0) {
		printf("getaddrinfo failed with error: %d\n", iResult);
		WSACleanup();
		return 1;
	}

	// Attempt to connect to an address until one succeeds
	for (ptr = result; ptr != NULL; ptr = ptr->ai_next) {

		// Create a SOCKET for connecting to server
		ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype,
			ptr->ai_protocol);
		if (ConnectSocket == INVALID_SOCKET) {
			printf("socket failed with error: %ld\n", WSAGetLastError());
			WSACleanup();
			return 1;
		}

		// Connect to server.
		iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
		if (iResult == SOCKET_ERROR) {
			closesocket(ConnectSocket);
			ConnectSocket = INVALID_SOCKET;
			continue;
		}
		break;
	}

	freeaddrinfo(result);

	if (ConnectSocket == INVALID_SOCKET) {
		printf("Unable to connect to server!\n");
		WSACleanup();
		return 1;
	}



	printf("Connected to server:\n Select statment\n  1.Insert\n  2.Update\n  3.Find\n  4.Exit");

	int menuSelection = getNum(1,4);
	int insertQuantity;
	MemberRecord memberUpdate;

	regex name("[a-zA-Z]+");
	regex date("(\\d{2})\\/(\\d{2})(?:\\/?(\\d{4}))?");
	switch (menuSelection)
	{
	case 1:
		cout << "How many Random records would you like to input:" << endl;
		insertQuantity = getNum(1, 40000);
		insertMany(ConnectSocket, insertQuantity);
		cout << "** Begining to insert " << insertQuantity << " records, ***NOTE*** If the server DB reaches 40,000 records during this process, this process will be stopped with notic **" << endl;
		break;
	case 2:
		cout << "Member id to update:" << endl;
		memberUpdate.memberId = getNum(1, 40000);
		cin.ignore();
		for (;;){
			cout << "New First Name:" << endl;
			cin.getline(memberUpdate.firstName, sizeof(memberUpdate.firstName));
			if (regex_match(memberUpdate.firstName, name))
			{
				break;
			}
		}
		for (;;) {
			cout << "New Last Name:" << endl;
			cin.getline(memberUpdate.lastName, sizeof(memberUpdate.lastName));
			if (regex_match(memberUpdate.lastName, name))
			{
				break;
			}
		}
		for (;;) {
			cout << "New Date of Birth:" << endl;
			cin.getline(memberUpdate.dOB, sizeof(memberUpdate.dOB));
			if (regex_match(memberUpdate.dOB, date))
			{
				break;
			}
			cin.clear();
			cin.ignore(100, '\n');
		}
		updateMember(ConnectSocket,memberUpdate);
		break;
	case 3:
		cout << "Member id to find:" << endl;
		findMember(ConnectSocket,getNum(1, 40000));
		break;
	case 4:
		break;
	default:
		break;
	}
	// cleanup
	closesocket(ConnectSocket);
	WSACleanup();

	return 0;
}
bool insertMany(SOCKET ConnectSocket, int quantity)
{
	int iResult = 0;
	ServerCall newRecord;
	char* charNewRecord = (char*)&newRecord;

	ClientCall fromServer;
	char* charFromServer = (char*)&fromServer;

	for (int i = 0; i < quantity; i++)
	{
		memset(&newRecord, 0, sizeof(ServerCall));
		randomServerCall(&newRecord);
		newRecord.callType = 1;

		// Send an initial buffer
		
		iResult = send(ConnectSocket, charNewRecord, sizeof(ServerCall), 0);
		if (iResult == SOCKET_ERROR) {
			printf("send failed with error: %d\n", WSAGetLastError());
			//closesocket(ConnectSocket);
			//WSACleanup();
			//return 1;
		}
		//printf("Bytes Sent: %ld\n", iResult);

		// shutdown the connection since no more data will be sent
		//iResult = shutdown(ConnectSocket, SD_SEND);
		if (iResult == SOCKET_ERROR) {
			printf("shutdown failed with error: %d\n", WSAGetLastError());
			closesocket(ConnectSocket);
			WSACleanup();
			return 1;
		}
		// Receive until the peer closes the connection
		do {

			iResult = recv(ConnectSocket, charFromServer, sizeof(ClientCall), 0);
			//fromServer = *reinterpret_cast<ClientCall*>(charFromServer);
			if (iResult > 0)
			{
				//printf("message From server %s\n", fromServer.message);
				if (fromServer.error != 0)
				{
					cout << fromServer.message << endl;
				}
				break;
			}
			else if (iResult == 0)
				printf("Connection closed\n");
			else
				printf("recv failed with error: %d\n", WSAGetLastError());

		} while (iResult > 0);
	}
	cout << "insert Done" << endl;
	return true;
}

int getNum(int min, int max)
{
	string input;
	bool enterError;
	int menuSelection;
	do
	{
		cin >> input;
		enterError = false;
		try
		{
			menuSelection = stoi(input, nullptr, 10);
			if (menuSelection > max || menuSelection < min)
			{
				enterError = true;
			}
		}
		catch (std::invalid_argument e)
		{
			enterError = true;
		}
		if (enterError)
		{
			cout << "Enter a number listed please" << endl;
		}
	} while (enterError);
	return menuSelection;
}

void randomServerCall(ServerCall *message)
{
	char characters[] = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	int firstNameLen = rand() % 31 + 1;
	int lastNameLen = rand() % 31 + 1;
	for (int i = 0; i < firstNameLen || i < lastNameLen; i++)
	{
		if (i < firstNameLen)
		{
			message->member.firstName[i] = characters[rand() % 52];
		}
		if (i < lastNameLen)
		{
			message->member.lastName[i] = characters[rand() % 52];
		}
	}
	int year = rand() % 115 + 1900;
	int month = rand() % 12 + 1;
	int day = rand() % 28 + 1;
	sprintf_s(message->member.dOB,"%02d/%02d/%04d",month,day,year);
}
bool findMember(SOCKET ConnectSocket,int memberId)
{
	int iResult = 0;
	ServerCall findRecord;
	char* charFindRecord = (char*)&findRecord;

	ClientCall fromServer;
	char* charFromServer = (char*)&fromServer;

	memset(&findRecord, 0, sizeof(ServerCall));
	findRecord.member.memberId = memberId;
	findRecord.callType = 3;

	// Send an initial buffer

	iResult = send(ConnectSocket, charFindRecord, sizeof(ServerCall), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed with error: %d\n", WSAGetLastError());
		//closesocket(ConnectSocket);
		//WSACleanup();
		//return 1;
	}
	//printf("Bytes Sent: %ld\n", iResult);

	// shutdown the connection since no more data will be sent
	//iResult = shutdown(ConnectSocket, SD_SEND);
	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	// Receive until the peer closes the connection
	do {

		iResult = recv(ConnectSocket, charFromServer, sizeof(ClientCall), 0);
		//fromServer = *reinterpret_cast<ClientCall*>(charFromServer);
		if (iResult > 0)
		{
			//printf("message From server %s\n", fromServer.message);
			if (fromServer.error != 0)
			{
				cout << fromServer.message << endl;
			}
			else
			{
				cout << "Found member \n Member id: " << fromServer.member.memberId
					<< "\n First name: " << fromServer.member.firstName
					<< "\n Last name: " << fromServer.member.lastName
					<< "\n Date Of Birth name: " << fromServer.member.dOB << endl;
			}
			break;
		}
		else if (iResult == 0)
			printf("Connection closed\n");
		else
			printf("recv failed with error: %d\n", WSAGetLastError());

	} while (iResult > 0);
	cout << "Find Done" << endl;
	return true;
}

bool updateMember(SOCKET ConnectSocket, MemberRecord member)
{
	int iResult = 0;
	ServerCall upDateRecord;
	char* charFindRecord = (char*)&upDateRecord;

	ClientCall fromServer;
	char* charFromServer = (char*)&fromServer;

	memset(&upDateRecord, 0, sizeof(ServerCall));
	upDateRecord.member = member;
	upDateRecord.callType = 2;

	// Send an initial buffer

	iResult = send(ConnectSocket, charFindRecord, sizeof(ServerCall), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed with error: %d\n", WSAGetLastError());
		//closesocket(ConnectSocket);
		//WSACleanup();
		//return 1;
	}
	//printf("Bytes Sent: %ld\n", iResult);

	// shutdown the connection since no more data will be sent
	//iResult = shutdown(ConnectSocket, SD_SEND);
	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	// Receive until the peer closes the connection
	do {

		iResult = recv(ConnectSocket, charFromServer, sizeof(ClientCall), 0);
		//fromServer = *reinterpret_cast<ClientCall*>(charFromServer);
		if (iResult > 0)
		{
			//printf("message From server %s\n", fromServer.message);
			if (fromServer.error != 0)
			{
				cout << fromServer.message << endl;
			}
			else
			{
				cout << "Member update successful" << endl;
			}
			break;
		}
		else if (iResult == 0)
			printf("Connection closed\n");
		else
			printf("recv failed with error: %d\n", WSAGetLastError());

	} while (iResult > 0);
	cout << "Find Done" << endl;
	return true;
}