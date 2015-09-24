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
//#include <thread>


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
	int firstEmptyIndex;
	int lastUpdatedIndex;
	int newMemberId();
	int getMemberIndex(int);
	static DWORD WINAPI fileAccess(void* database)
	{
		Database* db = (Database*)database;
		
		return db->ThreadStart(db);
	};
	DWORD ThreadStart(Database* database)
	{
		//std::ofstream myfile;
		//myfile.open("member.db");


		std::string path("member.db");

		for (;;)
		{
			std::ofstream FILE(path, std::ios::out | std::ofstream::binary);
			//std::copy(database->memberTable.begin(), database->memberTable.end(), std::ostreambuf_iterator<char>(FILE));
			//lastUpdatedIndex = database->memberTable.size();
		}
		/*
		std::ifstream INFILE(path, std::ios::in | std::ifstream::binary);
		std::istreambuf_iterator iter(INFILE);
		std::copy(iter.begin(), iter.end(), std::back_inserter(newVector));
		//read from file
		*/
		//write to file

		

		//myfile << "Writing this to a file.\n";
		//myfile.close();
		return 0;
	}
};

