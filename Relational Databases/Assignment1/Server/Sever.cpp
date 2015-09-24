#undef UNICODE

#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
//#include <stdlib.h>
//#include <stdio.h>
#include "Database.h"

// Need to link with Ws2_32.lib
#pragma comment (lib, "Ws2_32.lib")
// #pragma comment (lib, "Mswsock.lib")

#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"
#define MAX_CLIENTS 64


int __cdecl main(void)
{
	Database memberDB;

	SOCKET clientSocket[MAX_CLIENTS];
	for (int x = 0; x < MAX_CLIENTS; x++)
	{
		clientSocket[x] = INVALID_SOCKET;
	}

	WSADATA wsaData;
	int iResult;

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

	/* mine*/
	
	/**/

	// Setup the TCP listening socket
	iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR) {
		printf("bind failed with error: %d\n", WSAGetLastError());
		freeaddrinfo(result);
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}

	//freeaddrinfo(result);

	iResult = listen(ListenSocket, SOMAXCONN);
	if (iResult == SOCKET_ERROR) {
		//printf("listen failed with error: %d\n", WSAGetLastError());
		//closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}

	u_long iMode = 1;
	ioctlsocket(ListenSocket, FIONBIO, &iMode);

	// Accept a client socket
	for (;;)
	{
		//add new client
		int freeSocket = 0;
		for (; freeSocket < MAX_CLIENTS && clientSocket[freeSocket] != INVALID_SOCKET; freeSocket++){}
		if (freeSocket < MAX_CLIENTS)
		{
			clientSocket[freeSocket] = accept(ListenSocket, NULL, NULL);
			if (clientSocket[freeSocket] == INVALID_SOCKET) {
				//printf("accept failed with error: %d\n", WSAGetLastError());
				//closesocket(ListenSocket);
				//WSACleanup();
				//return 1;
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
						
						//printf("Bytes received: %d: %s\n", iResult,inMessage.member.firstName);
						outMessage = memberDB.doStatment(inMessage);
						if (outMessage.member.memberId == -1)
						{
							outMessage.error = 1;
							//outMessage.message = "..";
						}

						// Echo the buffer back to the sender
						iSendResult = send(clientSocket[cClientSocket], charOutMessage, sizeof(ClientCall), 0);
						if (iSendResult == SOCKET_ERROR) {
							printf("send failed with error: %d\n", WSAGetLastError());
							closesocket(clientSocket[cClientSocket]);
							WSACleanup();
							return 1;
						}
						//printf("Bytes sent: %d\n", iSendResult);
						break;
					}
					else if (iResult == 0)
					{
						printf("Connection closing...\n");
						closesocket(clientSocket[cClientSocket]);
						clientSocket[cClientSocket] = INVALID_SOCKET;
					}
					else {
						//will be spammed thanks to non blovking
						//printf("recv failed with error: %d\n", WSAGetLastError());
						//closesocket(ClientSocket);
						//WSACleanup();
						//return 1;
					}

				} while (iResult > 0);
			}
		}
		// No longer need server socket
		//closesocket(ListenSocket);

		// shutdown the connection since we're done
		//iResult = shutdown(ClientSocket, SD_SEND);
		if (iResult == SOCKET_ERROR) {
			//printf("shutdown failed with error: %d\n", WSAGetLastError());
			//closesocket(ClientSocket);
			//WSACleanup();
			//return 1;
		}
	}
	// cleanup
	closesocket(ClientSocket);
	WSACleanup();

	return 0;
}