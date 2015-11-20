<h1>Client Server Data Service</h1>
<h3>Objectives:</h3>
- To review file I/O basics
- To simulate a database service
- 
<h3>Description:</h3>
You will write a client-server software system that demonstrates how a database system normally
operates in a network environment. Generally, a database system runs as a service that responds to
requests from an arbitrary list of sources. It is up to the database system to make sure data is secure and
integrity is maintained. For example, only those sources that are permitted should access or update the
data; and only one source can update data at a time – even though several sources may be requesting to
update at the same time. 
<h3>Requirements:</h3>
<ol>
<li>The database file consists of records with the following fields:</li>
<ol type="A">
<li>MemberID (as an integer)</li>
<li>FirstName (as a variable length string)</li>
<li>LastName (as a variable length string)</li>
<li>DateOfBirth (as a Date format supported by your OS)</li>
</ol>
<li>Write a server program that will run continuously, listening for requests to write to the
database. There are only 3 requests that the server can respond to:</li>
<ol type="A">
<li>INSERT - allows the insertion of new data. The data should be provided only as the
FirstName, LastName and DateOfBirth. The MemberID is automatically generated and
written to the file with the rest of the data. The automatically generated MemberID
must be sequential.
The server should only handle up to 40,000 records. This means that IDs of 1-40,000
would be valid.
If the INSERT command is not successful, an error should be returned to the client.
</li>
<li>UPDATE – allows the modification of existing records. The client must provide a valid
MemberID, FirstName, LastName and DateOfBirth. Even though the data may not
change, all four values should be in the parameter list.
If the UPDATE command is not successful, an error should be returned to the client.</li>
<li>FIND – allows a client program to get the information of a specific MemberID. The
server should return all four data fields.
If the FIND command is not successful, an error should be returned to the client.</li>
</ol>
<li>Write a client program that can be run multiple times concurrently from one or more
computers. The purpose of this program is to randomly create the data that will be written to
the database and to demonstrate that you can handle multiple requests simultaneously</li>
<li>Write a client program that can query the database (use the FIND command) and will allow you
to update a specific record.</li>
</ol>
<h3>Hand In:</h3>
<ol>
<li>The Server command definitions/protocol document</li>
<li>All source code</li>
<li>Installation and usage instructions</li>
<li>Document stating any problems or deficiencies (bug list) in the submitted program</li>
</ol>
