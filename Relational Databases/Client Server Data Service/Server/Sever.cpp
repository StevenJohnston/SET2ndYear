/*
File: Server.cpp
Name: Matthew Warren, Steven johnston
Assignment: Client Server I/O Database Assignment #1
Date: 9/25/2015
Description: includes, defines, enums, structs, and prototypes required for Client
*/
#include "Server.h"
int __cdecl main(int argc, char* argv[])
{
	if (argc != 2) {
		printf("usage: %s file-name\n", argv[0]);
		return 1;
	}
	SOCKET clientSocket[MAX_CLIENTS];
	for (int x = 0; x < MAX_CLIENTS; x++)
	{
		clientSocket[x] = INVALID_SOCKET;
	}

	WSADATA wsaData;
	int iResult;
	bool *serverShutDown = (bool*)malloc(sizeof(bool));
	*serverShutDown = false;

	SOCKET ListenSocket = INVALID_SOCKET;
	SOCKET ClientSocket = INVALID_SOCKET;

	struct addrinfo *result = NULL;
	struct addrinfo hints;

	int iSendResult;
	char recvbuf[DEFAULT_BUFLEN];
	int recvbuflen = DEFAULT_BUFLEN;

	// Initialize Winsock
	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed with error: %d\n", iResult);
		return 1;
	}

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	// Resolve the server address and port
	iResult = getaddrinfo(NULL, DEFAULT_PORT, &hints, &result);
	if (iResult != 0) {
		printf("getaddrinfo failed with error: %d\n", iResult);
		WSACleanup();
		return 1;
	}

	// Create a SOCKET for connecting to server
	ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
	if (ListenSocket == INVALID_SOCKET) {
		printf("socket failed with error: %ld\n", WSAGetLastError());
		freeaddrinfo(result);
		WSACleanup();
		return 1;
	}

	// Setup the TCP listening socket
	iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR) {
		printf("bind failed with error: %d\n", WSAGetLastError());
		freeaddrinfo(result);
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}
	iResult = listen(ListenSocket, SOMAXCONN);
	if (iResult == SOCKET_ERROR) {
		WSACleanup();
		return 1;
	}

	u_long iMode = 1;
	ioctlsocket(ListenSocket, FIONBIO, &iMode);


	Database memberDB(argv[1]);
	HANDLE fileIO;
	fileIO = CreateThread(NULL, 0, getInput, serverShutDown, 0, NULL);
	// Accept a client socket
	printf("Accepting clients...\n");
	for (;!*serverShutDown;)
	{
		//add new client
		int freeSocket = 0;
		for (; freeSocket < MAX_CLIENTS && clientSocket[freeSocket] != INVALID_SOCKET; freeSocket++){}
		if (freeSocket < MAX_CLIENTS)
		{
			clientSocket[freeSocket] = accept(ListenSocket, NULL, NULL);

			if (clientSocket[freeSocket] != INVALID_SOCKET) {
				std::cout << "Client #" << freeSocket << " connected" << std::endl;
			}
		}
		//read from clients
		for (int cClientSocket = 0; cClientSocket < MAX_CLIENTS; cClientSocket++)
		{
			if (clientSocket[cClientSocket] != INVALID_SOCKET)
			{
				ServerCall inMessage;
				char * charInMessage = (char*)&inMessage;

				ClientCall outMessage = {1, "Insert Perfect"};
				char * charOutMessage = (char*)&outMessage;

				do {
					recvbuf[0] = '\0';
					iResult = recv(clientSocket[cClientSocket], charInMessage, sizeof(ServerCall), 0);
					if (iResult > 0) {
						outMessage = memberDB.doStatment(inMessage);
						if (outMessage.member.memberId == -1)
						{
							outMessage.error = 1;
						}

						// Echo the buffer back to the sender
						iSendResult = send(clientSocket[cClientSocket], charOutMessage, sizeof(ClientCall), 0);
						if (iSendResult == SOCKET_ERROR) {
							std::cout << "Sent to server Failed" << std::endl;
							closesocket(clientSocket[cClientSocket]);
							WSACleanup();
							return 1;
						}
						//printf("Bytes sent: %d\n", iSendResult);
						break;
					}
					else if (iResult == 0)
					{
						printf("client #%d closing...\n",cClientSocket);
						closesocket(clientSocket[cClientSocket]);
						clientSocket[cClientSocket] = INVALID_SOCKET;
					}
					else {
					}

				} while (iResult > 0);
			}
		}
	}
	// cleanup
	closesocket(ClientSocket);
	WSACleanup();

	return 0;
}


DWORD WINAPI getInput(void *exit)
{
	for (;;)
	{
		std::cout << "Enter 1 to close Server\n";
		int input;
		std::cin >> input;
		if (input == 1)
		{
			bool* bExit = (bool*)exit;
			*bExit = true;
			break;
		}
	}
	return 1;
}