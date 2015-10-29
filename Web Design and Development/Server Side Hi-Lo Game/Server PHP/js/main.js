var max = 0; //max Number in guess
var max = 0;//max Number in guess
var min = 1;//min Number in guess
var answer; //Answer for guesses
var name; 	//Name of Player
//When window is ready
$SJ("window").ready(function(){
//Set focus to element with id = txtName
	$SJ("#txtName").focus();
	//btnName on click
	$SJ("#btnName").click(function(){
		//Get value of txtName
		name = $SJ("#txtName").value();
		//AJAX request to server. Sends the state and name. Recives if valid and the message from the server
		$ajax({url:"ServerHiLo.php",
			//Request is asynchronous
			async:true,
			//Data to be sent to server
			data: {"state":"name","name":name},
			//Type of data to send to server
			dataType:"JSON",
			//Function to take place when the request is recived

			//Name: success
			//Decsription: Changes Dom elements depending on if the server validation passed
			//Param:
			//	data: The data recived from the server
			//Return:
			//	
			success: function(data){
				//Parse the data json string to js object
				var dataObj = JSON.parse(data);
				//If the server validation passed
				if(dataObj.valid == true)
				{
					//Hide the name div
					$SJ("#divName").style("display","none");
					//set lblName innterHTML = name
					$SJ("#lblName").innerHTML(name);
					//set divMax to visable
					$SJ("#divMax").style("display","block");
					//Clear result div
					$SJ("#divResult").innerHTML("");
					//Focus on txtMax
					$SJ("#txtMax").focus();
				}
				else//Server validation failed
				{
					//Show the server message in the divResults field
					$SJ("#divResult").innerHTML("<h2>"+dataObj.message+"<h2>");
				}
			},
		});
	});
	//btnMax on click
	$SJ("#btnMax").click(function(){
		//get max num
		var maxInput = $SJ("#txtMax").value();
		//AJAX request to server. Sends the state and name. Recives if valid and the message from the server
		$ajax({url:"ServerHiLo.php",
			//Request is asynchronous
			async:true,
			//Data to be sent to server
			data: {"state":"maxNum","maxNum":maxInput},
			//Type of data to send to server
			dataType:"JSON",

			//Function to take place when the request is recived

			//Name: success
			//Decsription: Changes Dom elements depending on if the server validation passed
			//Param:
			//	data: The data recived from the server
			//Return:
			//
			success: function(data){
				//Parse the data json string to js object
				var dataObj = JSON.parse(data);
				//If the server validation passed
				if(dataObj.valid == true)
				{
					//Hide divMax
					$SJ("#divMax").style("display","none");
					//Show divGame
					$SJ("#divGame").style("display","block");
					//Set focus to txtGuess
					$SJ("#txtGuess").focus();
				}
				//Show message from server
				$SJ("#divResult").innerHTML("<h2>"+dataObj.message+"<h2>");
			},
		});
	});
	//btnGuess on click
	$SJ("#btnGuess").click(function(){
		//get guess
		var guess = $SJ("#txtGuess").value();
		//AJAX request to server. Sends the state and name. Recives if valid and the message from the server
		$ajax({url:"ServerHiLo.php",
			//Request is asynchronous
			async:true,
			//Data to be sent to server
			data: {"state":"guess","guess":guess},
			//Type of data to send to server
			dataType:"JSON",

			//Function to take place when the request is recived

			//Name: success
			//Decsription: Changes Dom elements depending on if the server validation passed
			//Param:
			//	data: The data recived from the server
			//Return:
			//
			success: function(data){
				//Parse the data json string to js object
				var dataObj = JSON.parse(data);
				//If the server validation passed
				if(dataObj.valid == true)
				{
					//Change color of body to green
					$SJ("body").style("background-color","rgb(0,204,0)");
					//Hide divMain in 1 second
					$SJ("#divMain").delay(1000).style("display","none");
					//Show divPlayAgain in 1 second
					$SJ("#divPlayAgain").delay(1000).style("display","block");
					//Hide divGame in 1 second
					$SJ("#divGame").delay(1000).style("display","none");
					//Show divMax in 1 second
					$SJ("#divMax").delay(1000).style("display","block");
					//Clear innerHtml of divResult in 1 second
					$SJ("#divResult").delay(1000).innerHTML("");
					//clear txtMax
					$SJ("#txtMax").value("");
				}
				//Clear txtGuess
				$SJ("#txtGuess").value("");
				//Set divResult to message from server
				$SJ("#divResult").innerHTML("<h2>"+dataObj.message+"<h2>");
				//Set focus to btnPlayAgain
				$SJ("#btnPlayAgain").focus();
			},
		});
	});
	//Set Button btnPlayAgain onclick fucntion
	$SJ("#btnPlayAgain").click(function()
	{
		//Hide divPlayAgain
		$SJ("#divPlayAgain").style("display","none");
		//Show divMain
		$SJ("#divMain").style("display","block");
		//Clear divResult
		$SJ("#divResult").innerHTML("");
		//Set focus to txtMax
		$SJ("#txtMax").focus();
		//Set body background color to white
		$SJ("body").style("background-color","white");
	});
	//txtName on key down
	$SJ("#txtName").keydown(function(event){
		//Enter key 
		if(event.keyCode == 13){
	        $SJ("#btnName").click();
	    }
	});
	//txtMax on key down
	$SJ("#txtMax").keydown(function(event){
		//Enter key
		if(event.keyCode == 13){
	        $SJ("#btnMax").click();
	    }
	});
	//txtGuess on key down
	$SJ("#txtGuess").keydown(function(event){
		//Enter key
		if(event.keyCode == 13){
	        $SJ("#btnGuess").click();
	    }
	});
});	