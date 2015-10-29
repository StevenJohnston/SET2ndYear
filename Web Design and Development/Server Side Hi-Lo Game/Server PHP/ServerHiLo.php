<?php
/*	Name: Steven Johnston
	File: ServerHiLo.asp
	Assignment: Server Hi Lo Assignment #3
	Date: 10/28/2015
	Purpos: Learn asp by recreating client hi lo game using web server technologies.
*/
//Starts Session
session_start();
//state of program
$state   = "";
//Is data entered valid
$valid   = false;
//Message to send to client
$message = "";
//Checks if state is set, if not user is new
if (isset($_GET["state"])) {
	//set $state
	$state = $_GET["state"];
	//if user it entering name
	if ($state == "name") {
		//set header
		header('Content-Type: application/json');
		//If name is set
		if (isset($_GET["name"])) {
			//set $name to queryString name
			$name = $_GET["name"];
			//Validate name
			if (preg_match('/^[a-zA-Z]+$/', $name)) {
				$valid = true;
			} else {
				//Message to send to client
				$message = "Name must consist of only letters";
			}
		}
	//if user entered max number
	} else if ($state == "maxNum") {
		$maxNum = $_GET['maxNum'];
		//validate maxNum
		if (preg_match('/^[0-9]{0,}$/', $maxNum)) {
			$valid              = true;
			//Set session maxNum to maxNum
			$_SESSION["maxNum"] = $maxNum;
			//Set session minNum to 1
			$_SESSION["minNum"] = 1;
			//Set session answer to random number from 1 to maxNum
			$_SESSION["answer"] = rand(1, $maxNum);
			//Message to send to client
			$message            = "Enter a Number from " . $_SESSION["minNum"] . " to " . $_SESSION["maxNum"];
		} else {
			//Message to send to client
			$message = "Enter a positive integer";
		}
	}
	//User entered a guess
	else if ($state == "guess") {
		$guess = $_GET['guess'];
		//Validate guess
		if (preg_match('/^[0-9]{0,}$/', $guess)) {
			//Guess is lower the min Guess
			if ($guess < $_SESSION["minNum"]) {
				//Message to send to client
				$message = "Your guess was to LOW try " . $_SESSION["minNum"] . " To " . $_SESSION["maxNum"];
				$valid   = false;
			} else {
				//'Guess is higher then max Guess
				if ($guess > $_SESSION["maxNum"]) {
					//Message to send to client
					$message = "Your guess was to high try " . $_SESSION["minNum"] . " To " . $_SESSION["maxNum"];
				} else {
					//Guess is lower then correct guess(answer)
					if ($guess < $_SESSION["answer"]) {
						//Change min Guess to guess +1
						$_SESSION["minNum"] = $guess + 1;
						//Message to send to client
						$message            = "That guess was LOW. <br >Enter a Number from " . $_SESSION["minNum"] . " to " . $_SESSION["maxNum"];
					//'Guees is higher then the correct guess (answer)
					} else if ($guess > $_SESSION["answer"]) {
						//Change max guess to guess -1
						$_SESSION["maxNum"] = $guess - 1;
						//Message to send to client
						$message            = "That guess was HIGH. <br >Enter a Number from " . $_SESSION["minNum"] . " to " . $_SESSION["maxNum"];
					//Guess is correct
					} else {
						//Message to send to client
						$message = "WINNER";
						$valid   = true;
					}
				}
			}
		} else {
			//Message to send to client
			$message = "Enter a positive integer";
		}
	}
	//array oject to send to client
	$array = array(
		'valid' => $valid,
		'message' => $message
	);
	//Convert the oject json string
	echo json_encode($array);
} else {//Client is new. Give him the client html
	require('client.php');
}
?>