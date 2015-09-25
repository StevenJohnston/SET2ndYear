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
	DWORD ThreadStart(Database* database)
	{

		//read from file
		std::ifstream myfileRead;

		myfileRead.open("member.db", std::fstream::binary | std::ofstream::in);
		//const char* wPointer = reinterpret_cast<const char*>(&(database->memberTable[lastUpdatedIndex]));
		std::streampos fileSize;

		myfileRead.seekg(0, std::ios::end);
		fileSize = myfileRead.tellg();
		myfileRead.seekg(0, std::ios::beg);

		for (int i = 0; i < fileSize / (16*5); i++)
		{
			MemberRecord memberFromFile;
			char* charFromFile = (char*)&memberFromFile;
			myfileRead.read(charFromFile,sizeof(memberFromFile));
			database->memberTable.push_back(memberFromFile);
		}
		firstEmptyIndex = database->memberTable.size();
		lastUpdatedIndex = database->memberTable.size();
		//write to file
		for (;;)
		{
			if (database->memberTable.size() != 0)
			{
				if (lastUpdatedIndex < database->memberTable.size())
				{
					std::ofstream myfile;
					myfile.open("member.db", std::ios::binary | std::ofstream::app);
					myfile.seekp(0, std::ios::end);
					const char* pointer = reinterpret_cast<const char*>(&(database->memberTable[lastUpdatedIndex]));
					//lastUpdatedIndex++;
					int sizeSnapshot = database->memberTable.size();
					size_t bytes = (sizeSnapshot - lastUpdatedIndex) * sizeof(database->memberTable[0]);
					lastUpdatedIndex = sizeSnapshot;
					myfile.write(pointer, bytes);
					myfile.close();
				}
			}
			
			if (updateQue.size() != 0)
			{
				std::fstream fileAppMid;
				fileAppMid.open("member.db", std::fstream::in | std::fstream::out | std::fstream::binary);
				int memberId = updateQue.front();
				updateQue.pop_front();
				fileAppMid.seekp(getMemberIndex(memberId) * (16 * 5));
				fileAppMid.write((char*)&(database->memberTable[getMemberIndex(memberId)]),sizeof(MemberRecord));
			}
		}

		//myfile << "Writing this to a file.\n";
		//myfile.close();
		return 0;
	}
};

