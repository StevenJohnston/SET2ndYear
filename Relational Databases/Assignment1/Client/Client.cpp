#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>
#include <string>
#include <iostream>
#include <time.h>


// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib
#pragma comment (lib, "Ws2_32.lib")
#pragma comment (lib, "Mswsock.lib")
#pragma comment (lib, "AdvApi32.lib")


#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"

using namespace std;

struct ServerCall
{
	int callType;
	int memberId;
	char firstName[32];
	char lastName[32];
	char dOB[11];
} typedef ServerCall;

struct ClientCall
{
	int error;
	char message[32];
} typedef ClientCall;

bool insertMany(SOCKET ConnectSocket, int quantity);
int getNum(int min, int max);
void randomServerCall(ServerCall *message);



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
	switch (menuSelection)
	{
	case 1:
		cout << "How many Random records would you like to input:" << endl;
		insertQuantity = getNum(1,40000);
		insertMany(ConnectSocket,insertQuantity);
		cout << "** Begining to insert " << insertQuantity << " records, ***NOTE*** If the server DB reaches 40,000 records during this process, this process will be stopped with notic **" << endl;
		break;
	case 2:
		cout << "Member id to update:" << endl;
			
		cout << "New First Name:" << endl;
		cout << "New Last Name:" << endl;
		cout << "New Date of Birth:" << endl;

		break;
	case 3:
		break;
	case 4:
		break;
	default:
		break;
	}


	/*
	for (int i = 0 ; i < 5; i++)
	{
		printf("asd\m");
		// Send an initial buffer
		//for (int i = 0; i < 5; i++)
		{
			iResult = send(ConnectSocket, toSend, sizeof(ReciveMessage), 0);
			if (iResult == SOCKET_ERROR) {
				printf("send failed with error: %d\n", WSAGetLastError());
				//closesocket(ConnectSocket);
				//WSACleanup();
				//return 1;
			}
		}
		printf("Bytes Sent: %ld\n", iResult);

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

			iResult = recv(ConnectSocket, recvbuf, recvbuflen, 0);
			if (iResult > 0)
			{
				printf("Bytes received: %d\n", iResult);
				break;
			}
			else if (iResult == 0)
				printf("Connection closed\n");
			else
				printf("recv failed with error: %d\n", WSAGetLastError());

		} while (iResult > 0);
	}*/
	// cleanup
	closesocket(ConnectSocket);
	WSACleanup();

	return 0;
}
bool insertMany(SOCKET ConnectSocket, int quantity)
{
	int iResult = 0;
	ServerCall newRecord = { 1,1,"steven","johnston","9/21/2015" };
	char* charNewRecord = (char*)&newRecord;

	

	ClientCall fromServer;
	char* charFromServer = (char*)&fromServer;

	for (int i = 0; i < quantity; i++)
	{
		memset(&newRecord, 0, sizeof(ServerCall));
		randomServerCall(&newRecord);

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
	int firstNameLen = rand() % 32 + 1;
	int lastNameLen = rand() % 32 + 1;
	for (int i = 0; i < firstNameLen || i < lastNameLen; i++)
	{
		if (i < firstNameLen)
		{
			message->firstName[i] = characters[rand() % 52];
		}
		if (i < lastNameLen)
		{
			message->lastName[i] = characters[rand() % 52];
		}
	}
	int year = rand() % 115 + 1900;
	int month = rand() % 12 + 1;
	int day = rand() % 28 + 1;
	sprintf_s(message->dOB,"%02d/%02d/%04d",month,day,year);
}