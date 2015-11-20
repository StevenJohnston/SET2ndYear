/*
	File: Client.h
	Name: Matthew Warren, Steven johnston
	Assignment: Client Server I/O Database Assignment #1
	Date: 9/25/2015
	Description: includes, defines, enums, structs, and prototypes required for Client
*/
#pragma once
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

#define NAME_LENGTH 32
#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27015"
#define MAX_RECORD_COUNT 40000
#define DOB_ALLOCATE_SIZE 11
#define MAX_AGE 115
#define NUM_MONTHS 12
#define NUM_DAYS 28
#define MIN_YEAR 1900

//Types of server DB calls
enum StatmentType
{
	statmenZero,
	insert,
	update,
	find
}typedef StatmentType;

//Member info struct
struct MemberRecord
{
	int memberId;
	char firstName[NAME_LENGTH];
	char lastName[NAME_LENGTH];
	char dOB[DOB_ALLOCATE_SIZE];
}typedef MemberRecord;
//Covers all calls to server
struct ServerCall
{
	int callType;
	MemberRecord member;
} typedef ServerCall;
//Cover all calls to client
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
