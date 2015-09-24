#pragma once
struct ServerCall
{
	int callType;
	int memberId;
	char firstName[32];
	char lastName[32];
	char dOB[11];
} typedef ServerCall;
