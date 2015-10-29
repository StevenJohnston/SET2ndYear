<%

'	Name: Steven Johnston
'	File: ServerHiLo.asp
'	Assignment: Server Hi Lo Assignment #3
'	Date: 10/28/2015
'	Purpos: Learn asp by recreating client hi lo game using web server technologies.

'Added modifications to 
'https://msdn.microsoft.com/en-us/library/tdte5kwf(v=VS.85).aspx
Function RegExpTest(patrn, strng)
    Dim regEx, Match, Matches
    ' Create the regular expression.
    Set regEx = New RegExp
    regEx.Pattern = patrn
    regEx.IgnoreCase = True
    regEx.Global = True
    Matches = regEx.Test(strng)
    RegExpTest = Matches
End Function

Dim state, valid, message, regExResults, guess, name, validString, maxNum
state  = ""
valid  = false
message  = ""
'Check if state is in the querystring. Indicates if the user is new
If Request.QueryString("state") <> "" then
	state  = Request.QueryString("state")
	'User State: entered name
	If state = "name" then
		name = Request.QueryString("name")
		'Validates name
		valid = RegExpTest("^[a-zA-Z]+$",name)
		'validation passed
		If valid <> true then
			message  = "Name must consist of only letters"
		End If
	'User State: entered maxNum
	ElseIf state = "maxNum" then
		maxNum = Request.QueryString("maxNum")
		'Validates maxNum
		valid = RegExpTest("^[0-9]+$",maxNum)
		'validation passed
		If valid = true then
			'Radomize seed
			Randomize 
			'Set max to number user entered
			Session("maxNum") = Cint(maxNum)
			'Set Min number to 1
			Session("minNum") = 1
			'Generate random number from 1 to maxNum to be the answer
			Session("answer") = Int((Rnd * Cint(maxNum)) +1)
			'Message for user
			message  = "Enter a Number from " & Session("minNum") & " to " & Session("maxNum")
		Else
			'Message for user
			message  = "Enter a positive integer"
		End If
	'User State: entered a guess
	ElseIf state = "guess" then
		guess = Request.QueryString("guess")
		'Validates guess
		valid = RegExpTest("^[0-9]+$",guess)
		'validation passed
		If valid = true then
			'Guess is lower the min Guess
			If Cint(guess) < Session("minNum") then
			'Message for user
				message  = "Your guess was to LOW try "  &  Session("minNum") &  " To "  &  Session("maxNum")
			Else
				'Guess is higher then max Guess
				If Cint(guess) > Session("maxNum") then
				'Message for user
					message  = "Your guess was to high try "  &  Session("minNum") &  " To "  &  Session("maxNum")
				Else
					'Guess is lower then correct guess(answer)
					If Cint(guess) < Session("answer") then
						'Change min Guess to 
						Session("minNum")  = Cint(guess) + 1
						'Message for user
						message  = "That guess was LOW. <br >Enter a Number from "  &  Session("minNum") &  " to "  &  Session("maxNum")
						'Answer was wrong input since it was incorrect.
						valid = false
					'Guees is higher then the correct guess (answer)
					ElseIf Cint(guess) > Session("answer") then
						Session("maxNum")  = Cint(guess) - 1
						'Message for user
						message  = "That guess was HIGH. <br >Enter a Number from "  &  Session("minNum") &  " to "  &  Session("maxNum")
						'Answer was wrong input since it was incorrect.
						valid = false
					'Guess is correct 
					Else
						'Message for user
						message  = "WINNER"
					End If
				End If
			End If
		Else
			'Message for user
			message  = "Enter a positive integer"
		End If
	End If
	'String version of the bool valid
	validString = "false"
	If valid then
		validString = "true"
	End if
	'Manual stringify to json object 
	Response.Write "{""valid"":" & validString & ",""message"":""" & message & """}"
'New user. Needs the page
Else
%>
	<!--#include file="client.asp"-->
<%
End If
%>
