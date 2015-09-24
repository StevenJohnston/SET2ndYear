#pragma once
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <vector>

using namespace std;

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

class Database
{
public:
	Database();
	~Database();
	MemberRecord doStatment(ServerCall);

private:
	MemberRecord insert(ServerCall statement);
	MemberRecord update(ServerCall statement);
	MemberRecord find(ServerCall statement);
	//MemberRecord memberTable[10];
	//typedef std::vector<MemberRecord> memberTable
	vector<MemberRecord> memberTable;
	int firstEmptyIndex;
	int newMemberId();
	int getMemberIndex(int);
};
