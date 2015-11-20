/*
File: Database.cpp
Name: Matthew Warren, Steven johnston
Assignment: Client Server I/O Database Assignment #1
Date: 9/25/2015
Description: 
	handler for all database access and file I/O
*/
#include "Database.h"

/*
Function Name: doStatment
description: Access DB according to paramerter. Insert, Update, and find
parameter:
	ServerCall statment: 
		int calltype: type of call to make on DB with member info
		MemberRecord member: member info 
return:
	ClientCall: 
		int error: error status 
		char message[64]: error message
		MemberRecord member: If calltype was find then return new member info
*/
ClientCall Database::doStatment(ServerCall statment)
{
	ClientCall returnClientCall = { 0,"",{-1,"","",""} };
	switch (statment.callType)
	{
	case StatmentType::insert:
		returnClientCall = insert(statment);
		break;
	case StatmentType::update:
		returnClientCall = update(statment);
		break;
	case StatmentType::find:
		returnClientCall = find(statment);
		break;
	default:
		break;
	}
	switch (returnClientCall.error)
	{
	case DatabaseError::noError:// No error
		break;
	case DatabaseError::databaseFull: //database full
		strcpy_s(returnClientCall.message, "Database has 40,000 records (Full)");
		break;
	case DatabaseError::memberIdPass: //member id pass last filled index
		strcpy_s(returnClientCall.message, "Member Id does not exist in database");
		break;
	default:
		break;
	}
	return returnClientCall;
}
Database::Database()
{
	
}
/*
Function Name : Database
description : Initialize DB by file name. Reads file into memory then writes new / updated data to file.
	parameter :
	return :
*/
Database::Database(char *file)
{
	dbfile = file;
	memberTable.reserve(MAX_RECORD_COUNT);

	HANDLE fileIO;
	fileIO = CreateThread(NULL, 0, fileAccess, this, 0, NULL);
}

Database::~Database()
{
}
/*
Function Name: insert
description: insert statment.member into members DB
parameter:
	ServerCall statment:
		int calltype: 
		MemberRecord member: member info to insert to database
return:
ClientCall:
	int error: error status
	char message[64]: error message
	MemberRecord member: 
*/
ClientCall Database::insert(ServerCall statement)
{
	ClientCall returnClientCall = { 0,"",{ -1,"","","" } };
	//dont insert if db is full
	if (firstEmptyIndex < MAX_RECORD_COUNT)
	{
		statement.member.memberId = newMemberId();
		memberTable.push_back(statement.member);
		firstEmptyIndex++;
		returnClientCall.member.memberId = 0;
	}
	else
	{
		returnClientCall.error = DatabaseError::databaseFull;
	}
	return returnClientCall;
}
/*
Function Name: update
description: Update member using member as new member info and positioning
parameter:
	ServerCall statment:
		int calltype:
		MemberRecord member: 
			memberId: location in database of record to update
			rest of fields: new data to replace old record
return:
ClientCall:
	int error: error status
	char message[64]: error message
	MemberRecord member: 
*/
ClientCall Database::update(ServerCall statement)
{
	ClientCall returnClientCall = { 0,"",{ -1,"","","" } };
	//DB not full
	if (getMemberIndex(statement.member.memberId) < firstEmptyIndex)
	{
		statement.member.memberId = memberTable[getMemberIndex(statement.member.memberId)].memberId;
		memberTable[getMemberIndex(statement.member.memberId)] = statement.member;
		updateQue.push_back(statement.member.memberId);
	}
	else
	{
		returnClientCall.error = DatabaseError::memberIdPass;
	}
	return returnClientCall;
}
/*
Function Name: find
description: find member using member.memberId for indexing
parameter:
	ServerCall statment:
		MemberRecord member:
			memberId: location in database of record to update
return:
ClientCall:
	int error: error status
	char message[64]: error message
	MemberRecord member: Member info
*/
ClientCall Database::find(ServerCall statement)
{
	ClientCall returnClientCall = { 0,"",{ -1,"","","" } };
	//memberId is under highest member id in db
	if (getMemberIndex(statement.member.memberId) < firstEmptyIndex)
	{
		returnClientCall.member = memberTable[getMemberIndex(statement.member.memberId)];
	}
	else
	{
		returnClientCall.error = DatabaseError::memberIdPass;
	}
	return returnClientCall;
}
/*
Function Name: newMemberId
description: Used to get the first open member ID
parameter:

return:
	int: first open memberId

*/
int Database::newMemberId()
{
	return firstEmptyIndex + 1;
}
/*
Function Name: getMemberIndex
description: Gets the index in the DB that the memberId is located
parameter:
	int memberId: member id to find
return:
	int: index the memberId lays

*/
int Database::getMemberIndex(int memberId)
{
	return memberId - 1;
}
/*
Function Name: fileIOThread
description: Reads database file on load then looks for new data inserts/updates and runs them on the DB file
parameter:
	Database* database: Pointer to the database to give access
return:
DWORD:
	
*/
DWORD Database::fileIOThread(Database* database)
{
	try
	{
		//read from file
		std::ifstream myfileRead;

		myfileRead.open(dbfile, std::fstream::binary | std::ofstream::in);
		//const char* wPointer = reinterpret_cast<const char*>(&(database->memberTable[lastUpdatedIndex]));
		std::streampos fileSize;

		myfileRead.seekg(0, std::ios::end);
		fileSize = myfileRead.tellg();
		myfileRead.seekg(0, std::ios::beg);

		//read each record form DB
		for (int i = 0; i < fileSize / sizeof(MemberRecord); i++)
		{
			MemberRecord memberFromFile;
			char* charFromFile = (char*)&memberFromFile;
			myfileRead.read(charFromFile, sizeof(memberFromFile));
			database->memberTable.push_back(memberFromFile);
		}
		firstEmptyIndex = database->memberTable.size();
		lastUpdatedIndex = database->memberTable.size();
		myfileRead.close();
		std::cout << "Database loaded\n";
		//write and update file
		for (;;)
		{
			if (database->memberTable.size() != 0)
			{
				if (lastUpdatedIndex < database->memberTable.size())
				{
					std::ofstream myfile;
					myfile.open(dbfile, std::ios::binary | std::ofstream::app);
					myfile.seekp(0, std::ios::end);
					//point at first record that has not been added to file.
					const char* pointer = reinterpret_cast<const char*>(&(database->memberTable[lastUpdatedIndex]));
					//Get size of table now. prevents get two different sizes. 
					int sizeSnapshot = database->memberTable.size();
					//Number of bytes to write 
					size_t bytes = (sizeSnapshot - lastUpdatedIndex) * sizeof(database->memberTable[0]);
					//lastupdatedIndex is now after most recent inserted record in table
					lastUpdatedIndex = sizeSnapshot;
					//Write mutiple records from table.
					myfile.write(pointer, bytes);
					myfile.close();
				}
			}
			if (updateQue.size() != 0)
			{
				std::fstream fileAppMid;
				fileAppMid.open(dbfile, std::fstream::in | std::fstream::out | std::fstream::binary);
				int memberId = updateQue.front();
				updateQue.pop_front();
				fileAppMid.seekp(getMemberIndex(memberId) * sizeof(MemberRecord));
				fileAppMid.write((char*)&(database->memberTable[getMemberIndex(memberId)]), sizeof(MemberRecord));
				fileAppMid.close();
			}
		}
	}
	catch (std::exception e)
	{
		std::cout << "Server has Shutdown preventing file thread from continuing\n";
	}
	return 0;
}

