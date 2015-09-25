#include "Database.h"
ClientCall Database::doStatment(ServerCall statment)
{
	ClientCall returnClientCall = { 0,"",{-1,"","",""} };
	switch (statment.callType)
	{
	case 1:
		returnClientCall = insert(statment);
		break;
	case 2:
		returnClientCall = update(statment);
		break;
	case 3:
		returnClientCall = find(statment);
		break;
	default:
		break;
	}
	switch (returnClientCall.error)
	{
	case 0:// No error
		break;
	case 1: //database full
		strcpy_s(returnClientCall.message, "Database has 40,000 records (Full)");
		break;
	case 2: //member id pass last filled index
		strcpy_s(returnClientCall.message, "Member Id does not exist in database");
		break;
	default:
		break;
	}
	return returnClientCall;
}
Database::Database()
{
	memberTable.reserve(MAX_RECORD_COUNT);
	try
	{
		HANDLE fileIO;
		fileIO = CreateThread(NULL,0,fileAccess,this,0, NULL);
	}
	catch (std::exception e)
	{

	}
	//std::thread fileIO(fileAccess,this);
	 
}

Database::~Database()
{
}

ClientCall Database::insert(ServerCall statement)
{
	ClientCall returnClientCall = { 0,"",{ -1,"","","" } };
	if (firstEmptyIndex < MAX_RECORD_COUNT)
	{
		statement.member.memberId = newMemberId();
		memberTable.push_back(statement.member);
		firstEmptyIndex++;
		returnClientCall.member.memberId = 0;
	}
	else
	{
		returnClientCall.error = 1;
	}
	return returnClientCall;
}
ClientCall Database::update(ServerCall statement)
{
	ClientCall returnClientCall = { 0,"",{ -1,"","","" } };
	if (getMemberIndex(statement.member.memberId) < firstEmptyIndex)
	{
		statement.member.memberId = memberTable[getMemberIndex(statement.member.memberId)].memberId;
		memberTable[getMemberIndex(statement.member.memberId)] = statement.member;
		updateQue.push_back(statement.member.memberId);
	}
	else
	{
		returnClientCall.error = 2;
	}
	return returnClientCall;
}
ClientCall Database::find(ServerCall statement)
{
	ClientCall returnClientCall = { 0,"",{ -1,"","","" } };
	if (getMemberIndex(statement.member.memberId) < firstEmptyIndex)
	{
		returnClientCall.member = memberTable[getMemberIndex(statement.member.memberId)];
	}
	else
	{
		returnClientCall.error = 2;
	}
	return returnClientCall;
}
int Database::newMemberId()
{
	return firstEmptyIndex + 1;
}
int Database::getMemberIndex(int memberId)
{
	return memberId - 1;
}

