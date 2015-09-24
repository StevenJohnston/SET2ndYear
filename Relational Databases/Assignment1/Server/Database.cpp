#include "database.h"

MemberRecord Database::doStatment(ServerCall statment)
{
	MemberRecord returnMember = {-1,"","",""};
	switch (statment.callType)
	{
	case 1:
		returnMember = insert(statment);
		break;
	case 2:
		returnMember = update(statment);
		break;
	case 3:
		returnMember = find(statment);
		break;
	default:
		break;
	}

	return returnMember;
}

Database::Database()
{
	memberTable.reserve(40000);
}

Database::~Database()
{
}

MemberRecord Database::insert(ServerCall statement)
{
	MemberRecord returnVal = {-1, "", "",""};
	if (firstEmptyIndex < 40000)
	{
		statement.member.memberId = newMemberId();
		memberTable.push_back(statement.member);
		firstEmptyIndex++;
		returnVal.memberId = 0;
	}
	return returnVal;
}
MemberRecord Database::update(ServerCall statement)
{
	MemberRecord returnMember;
	if (getMemberIndex(statement.member.memberId) < firstEmptyIndex)
	{
		statement.member.memberId = memberTable[getMemberIndex(statement.member.memberId)].memberId;
		memberTable[getMemberIndex(statement.member.memberId)] = statement.member;
		returnMember.memberId = 0;
	}
	return returnMember;
}
MemberRecord Database::find(ServerCall statement)
{
	MemberRecord member;
	member.memberId = -1;
	if (getMemberIndex(statement.member.memberId) < firstEmptyIndex)
	{
		member = memberTable[getMemberIndex(statement.member.memberId)];
	}
	return member;
}
int Database::newMemberId()
{
	return firstEmptyIndex + 1;
}
int Database::getMemberIndex(int memberId)
{
	return memberId - 1;
}