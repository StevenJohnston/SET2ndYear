/*
File: Client.h
Name: Matthew Warren, Steven johnston
Assignment: Client Server I/O Database Assignment #1
Date: 9/25/2015
Description: includes, defines, enums, structs, and prototypes required for Client
*/
#pragma once
#undef UNICODE

#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include "Database.h"

// Need to link with Ws2_32.lib
#pragma comment (lib, "Ws2_32.lib")
// #pragma comment (lib, "Mswsock.lib")

#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"
#define MAX_CLIENTS 64
