<h1>SQL Queries</h1>
<h3>Objectives:</h3>
 - Demonstrate the ability to write SQL statements to manipulate and retrieve data in a database.
<h3>Requirements:</h3>
You will be using the Northwind database (provided as script file) <br/>
Write a single SQL script file with:
<ul>
<li>Comment header meeting the same requirements as those for any submitted SET source code</li>
<li>A single comment with the question number for each solution you provide in the script</li>
<li>A single SQL statement (solution) for each question in this assignment. The statement may be
written on several lines for clarity</li>
<li>The script file must be completely and successfully executed in MySQL. Assume that the
Northwind database is already installed</li>
</ul>

<h3>Questions:</h3>
<ol>
<li>Display the CustomerID, ContactName, Country and City (in that order) in the Customers table</li>
<li>Display the countries that are in the Customers table in alphabetical order. Display each country
name only once</li>
<li>What are the CompanyName and City of all customers in Germany?</li>
<li>Display the CustomerID and ContactName for each customer that does not have a Fax number</li>
<li>How many products are in the Products tables?</li>
<li>Display the ProductID, ProductName and UnitPrice for each product in the Products table</li>
<li>Display the ProductName, UnitsInStock and UnitPrice (in that order) for all the products that
cost most than $20 (assuming the unit price is in dollars). Make sure that the list is in UnitPrice
descending order (most expensive at the top of the list).</li>
<li>How many products are discontinued? (Discontinued products are indicated by a value of -1).</li>
<li>Display the CategoryName and ProductName (in that order) for each product.</li>
<li>From the Employees table, combine the Title, FirstName and LastName to display a column
called Salutation</li>
<li>Display a list of TerritoryDescriptions with their corresponding RegionDescriptions</li>
<li>For each order detail line, display the OrderID, CustomerID, ProductID and Quantity</li>
<li>For each order detail line, display the OrderID, CustomerID, ProductID and Extended Price. (The
extended price is the product of UnitPrice and Quantity. Make sure the column is called
“Extended Price”.)</li>
<li>For each order, display the OrderID, OrderDate, CompanyName (Customers) and Employee
Name (combination of first name and last name from Employees)</li>
<li>Display the CustomerID and CustomerName of all Customers that have ever had an order</li>
<li>Display the CustomerID and CustomerName of all Customers that have never had an order</li>
<li>Add a new region, called ‘Europe’, to the Region table.</li>
<li>Remove the region called ‘Europe’ from the Region table.</li>
<li>For the company called ‘Ernst Handel’, change the name of the contact person to Hans Schmidt</li>
<li>Increase each UnitPrice in the Products table by $1 (assuming the unit price is in $’s).</li>
<li>Create a new category of products called “Discontinued”.</li>
<li>For each discontinued product, change its category to “Discontinued”.</li>
</ol>
<h3>Submit:</h3>
 - The script file, as specified above
