#include "database.h"

class Database
{

public:
	Database();
	~Database();
	bool doStatment(ServerCall statment)
	{
		switch (statment.callType)
		{
		case 1:
			insert(statment);
			break;
		case 2:
			update(statment);
			break;
		case 3:
			find(statment);
			break;
		default:
			break;
		}
	}
private:
	bool insert(ServerCall statment)
	{
	}
	bool update(ServerCall statment)
	{
	}
	bool find(ServerCall statment)
	{
	}
};

Database::Database()
{
}

Database::~Database()
{
}