/*
File: Database.h
Name: Matthew Warren, Steven johnston
Assignment: Client Server I/O Database Assignment #1
Date: 9/25/2015
Description: includes, defines, enums, structs, and prototypes required for database object
*/
#pragma once
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <string>
#include <vector>
#include <iostream>
#include <fstream>
#include "windows.h"
#include <exception>
#include <algorithm>
#include <iterator>
#include <list>

#define MAX_RECORD_COUNT 40000

//DB commands
enum StatmentType
{
	zero,
	insert,
	update,
	find
}typedef StatmentType;
//Database errors
enum DatabaseError
{
	noError,
	databaseFull,
	memberIdPass,
}typedef DatabaseError;
//Member fields
struct MemberRecord
{
	int memberId;
	char firstName[32];
	char lastName[32];
	char dOB[11];
}typedef MemberRecord;
//Struct for Calls to server
struct ServerCall
{
	int callType;
	MemberRecord member;
} typedef ServerCall;
//Struct for calls to client
struct ClientCall
{
	int error;
	char message[64];
	MemberRecord member;
} typedef ClientCall;

class Database
{
public:
	Database();
	~Database();
	ClientCall doStatment(ServerCall);

private:
	ClientCall insert(ServerCall statement);
	ClientCall update(ServerCall statement);
	ClientCall find(ServerCall statement);
	std::vector<MemberRecord> memberTable;
	std::list<int> updateQue;
	int firstEmptyIndex;
	int lastUpdatedIndex;
	int newMemberId();
	int getMemberIndex(int);
	
	static DWORD WINAPI fileAccess(void* database)
	{
		Database* db = (Database*)database;
		return db->fileIOThread(db);
	};
	DWORD fileIOThread(Database* database);
};

