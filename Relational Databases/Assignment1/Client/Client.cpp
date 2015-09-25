/*
File: Client.cpp
Name: Matthew Warren, Steven johnston
Assignment: Client Server I/O Database Assignment #1
Date: 9/25/2015
Description: Client sided simple Databata. insert, update, and find members from server.
*/
#include "Client.h"
int __cdecl main(int argc, char **argv)
{
	srand(time(NULL));

	WSADATA wsaData;
	SOCKET ConnectSocket = INVALID_SOCKET;
	struct addrinfo *result = NULL,
		*ptr = NULL,
		hints;
	int iResult;

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
	//true to disconnect from server
	bool disconnectServer = false;
	printf("Connected to server");
	//loop till server disconnect flag = true
	for (;!disconnectServer;)
	{
		printf("Select statment\n  1.Insert\n  2.Update\n  3.Find\n  4.Exit\n");
		//get menu select
		int menuSelection = getNum(1, 4);
		int insertQuantity;
		MemberRecord memberUpdate;
		//only letters
		std::regex name("[a-zA-Z]+");
		//date format
		std::regex date("(\\d{2})\\/(\\d{2})(?:\\/?(\\d{4}))?");
		switch (menuSelection)
		{
		case StatmentType::insert:
			std::cout << "How many Random records would you like to input:" << std::endl;
			insertQuantity = getNum(1, MAX_RECORD_COUNT);
			insertMany(ConnectSocket, insertQuantity);
			std::cout << "** Begining to insert " << insertQuantity << " records, ***NOTE*** If the server DB reaches 40,000 records during this process, this process will be stopped with notic **" << std::endl;
			break;
		case StatmentType::update:
			std::cout << "Member id to update:" << std::endl;
			memberUpdate.memberId = getNum(1, MAX_RECORD_COUNT);
			std::cin.ignore();
			for (;;) {
				std::cout << "New First Name:" << std::endl;
				std::cin.getline(memberUpdate.firstName, sizeof(memberUpdate.firstName));
				if (regex_match(memberUpdate.firstName, name))
				{
					break;
				}
			}
			for (;;) {
				std::cout << "New Last Name:" << std::endl;
				std::cin.getline(memberUpdate.lastName, sizeof(memberUpdate.lastName));
				if (regex_match(memberUpdate.lastName, name))
				{
					break;
				}
			}
			for (;;) {
				std::cout << "New Date of Birth:" << std::endl;
				std::cin.getline(memberUpdate.dOB, sizeof(memberUpdate.dOB));
				if (regex_match(memberUpdate.dOB, date))
				{
					break;
				}
				std::cin.clear();
				std::cin.ignore(100, '\n');
			}
			updateMember(ConnectSocket, memberUpdate);
			break;
		case StatmentType::find:
			std::cout << "Member id to find:" << std::endl;
			findMember(ConnectSocket, getNum(1, MAX_RECORD_COUNT));
			break;
		case 4: //exit
			disconnectServer = true;
			break;
		default:
			break;
		}
	}
	printf("Disconected from server.\n");
	// cleanup
	closesocket(ConnectSocket);
	WSACleanup();

	return 0;
}
/*
Function Name: insertMany
description: Insert many randomly generated member records and send them to database
parameter:
	SOCKET ConnectSocket: Connection to server
	int quantity: Number of new records to create for server
return:
	bool: true if no errors
*/
bool insertMany(SOCKET ConnectSocket, int quantity)
{
	int iResult = 0;
	ServerCall newRecord;//new Servercall
	char* charNewRecord = (char*)&newRecord;// char* to server call for server calls

	ClientCall fromServer;
	char* charFromServer = (char*)&fromServer;
	bool serverError = false;
	//send quantity amount of calls. end early if server sends error
	for (int i = 0; i < quantity && !serverError; i++)
	{
		memset(&newRecord, 0, sizeof(ServerCall));
		randomServerCall(&newRecord);
		newRecord.callType = 1;

		// Send serverCall to server
		iResult = send(ConnectSocket, charNewRecord, sizeof(ServerCall), 0);
		if (iResult == SOCKET_ERROR) {
			printf("send failed with error: %d\n", WSAGetLastError());
		}
		if (iResult == SOCKET_ERROR) {
			printf("shutdown failed with error: %d\n", WSAGetLastError());
			closesocket(ConnectSocket);
			WSACleanup();
			return 1;
		}
		// Receive until the peer closes the connection
		do {
			//get message from server to check for errors
			iResult = recv(ConnectSocket, charFromServer, sizeof(ClientCall), 0);
			if (iResult > 0)
			{
				if (fromServer.error != 0)
				{
					std::cout << "Entered " << i << " record(s) before:" << std::endl  << fromServer.message << std::endl;
					serverError = true;
				}
				break;
			}
			else if (iResult == 0)
				printf("Connection closed\n");
			else
				printf("recv failed with error: %d\n", WSAGetLastError());

		} while (iResult > 0);
	}
	std::cout << "insert Done" << std::endl;
	return true;
}
/*
Function Name: getNum
description: Gets intget from user in range of parameters
parameter:
	int min: lowest number user can enter
	int max: highest number user can enter
return:
	int: Valid number the user entered
*/
int getNum(int min, int max)
{
	std::string input;
	bool enterError;
	int menuSelection;
	do
	{
		std::cin >> input;
		enterError = false;
		try
		{
			menuSelection = std::stoi(input, nullptr, 10);
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
			std::cout << "Enter a number listed please" << std::endl;
		}
	} while (enterError);
	return menuSelection;
}
/*
Function Name: randomServerCall
description: Generates random field for *message->member
parameter:
	ServerCall *message: pointer to ServerCall to edit data
return:
	void:
*/
void randomServerCall(ServerCall *message)
{
	//Letter to pick from
	char characters[] = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	int firstNameLen = rand() % (sizeof(firstNameLen)-1) + 1;
	int lastNameLen = rand() % (sizeof(lastNameLen)-1) + 1;
	//loop letters for first and last name
	for (int i = 0; i < firstNameLen || i < lastNameLen; i++)
	{
		if (i < firstNameLen)
		{
			message->member.firstName[i] = characters[rand() % sizeof(characters)];
		}
		if (i < lastNameLen)
		{
			message->member.lastName[i] = characters[rand() % sizeof(characters)];
		}
	}
	//randomize date
	int year = rand() % MAX_AGE + MIN_YEAR;
	int month = rand() % NUM_MONTHS + 1;
	int day = rand() % NUM_DAYS + 1;
	sprintf_s(message->member.dOB,"%02d/%02d/%04d",month,day,year);
}
/*
Function Name: findMember
description: Calls the server with memberId to get a members data
parameter:
	SOCKET ConnectSocket: connection to server
	int memberId: member id to find
return:
	bool: false if no error
*/
bool findMember(SOCKET ConnectSocket,int memberId)
{
	int iResult = 0;
	ServerCall findRecord;
	char* charFindRecord = (char*)&findRecord;

	ClientCall fromServer;
	char* charFromServer = (char*)&fromServer;

	memset(&findRecord, 0, sizeof(ServerCall));
	findRecord.member.memberId = memberId;
	findRecord.callType = StatmentType::find;

	//Send serverCall with id of member to find
	iResult = send(ConnectSocket, charFindRecord, sizeof(ServerCall), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed with error: %d\n", WSAGetLastError());
	}

	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	do {
		//recive error / member info from server
		iResult = recv(ConnectSocket, charFromServer, sizeof(ClientCall), 0);
		if (iResult > 0)
		{
			if (fromServer.error != 0)
			{
				std::cout << fromServer.message << std::endl;
			}
			else
			{
				std::cout << "Found member \n Member id: " << fromServer.member.memberId
					<< "\n First name: " << fromServer.member.firstName
					<< "\n Last name: " << fromServer.member.lastName
					<< "\n Date Of Birth name: " << fromServer.member.dOB << std::endl;
			}
			break;
		}
		else if (iResult == 0)
			printf("Connection closed\n");
		else
			printf("recv failed with error: %d\n", WSAGetLastError());

	} while (iResult > 0);
	std::cout << "Find Done" << std::endl;
	return true;
}
/*
Function Name: updateMember
description: Calls server to update members info
parameter:
	SOCKET ConnectSocket: Connection to server
return:
	MemberRecord member: future member data
*/
bool updateMember(SOCKET ConnectSocket, MemberRecord member)
{
	int iResult = 0;
	ServerCall upDateRecord;
	char* charFindRecord = (char*)&upDateRecord;

	ClientCall fromServer;
	char* charFromServer = (char*)&fromServer;

	memset(&upDateRecord, 0, sizeof(ServerCall));
	upDateRecord.member = member;
	upDateRecord.callType = StatmentType::update;

	//Send server call with member to update and info to server
	iResult = send(ConnectSocket, charFindRecord, sizeof(ServerCall), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed with error: %d\n", WSAGetLastError());
	}

	// shutdown the connection since no more data will be sent
	if (iResult == SOCKET_ERROR) {
		printf("shutdown failed with error: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	do {
		//recive error from server
		iResult = recv(ConnectSocket, charFromServer, sizeof(ClientCall), 0);
		if (iResult > 0)
		{
			if (fromServer.error != 0)
			{
				std::cout << fromServer.message << std::endl;
			}
			else
			{
				std::cout << "Member update successful" << std::endl;
			}
			break;
		}
		else if (iResult == 0)
			printf("Connection closed\n");
		else
			printf("recv failed with error: %d\n", WSAGetLastError());

	} while (iResult > 0);
	std::cout << "Find Done" << std::endl;
	return true;
}