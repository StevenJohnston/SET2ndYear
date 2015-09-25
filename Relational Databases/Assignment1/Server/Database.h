/*
File: Client.h
Name: Matthew Warren, Steven johnston
Assignment: Client Server I/O Database Assignment #1
Date: 9/25/2015
Description: includes, defines, enums, structs, and prototypes required for Client
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
//#include <thread>

enum StatmentType
{
	zero,
	insert,
	update,
	find
}typedef StatmentType;

enum DatabaseError
{
	noError,
	databaseFull,
	memberIdPass,
}typedef DatabaseError;


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
	//MemberRecord memberTable[10];
	//typedef std::vector<MemberRecord> memberTable
	std::vector<MemberRecord> memberTable;
	std::list<int> updateQue;
	int firstEmptyIndex;
	int lastUpdatedIndex;
	int newMemberId();
	int getMemberIndex(int);
	static DWORD WINAPI fileAccess(void* database)
	{
		Database* db = (Database*)database;
		
		return db->ThreadStart(db);
	};
	DWORD ThreadStart(Database* database);
};

