var max = 0; //max Number in guess
	var min = 1; //min Number in guess
	var answer; //Answer for guesses
	var name; //Name of Player
	//When window is ready
	

	$SJ("window").ready(function(){
		//Set focus to element with id = txtName
		$SJ("#txtName").focus();
		//btnName on click
		$SJ("#btnName").click(function(){
			//Get value of txtName
			name = $SJ("#txtName").value();
			//Check if name is only letters
			if(/^[a-zA-Z]+$/.test(name)){
				//hide divName
				$SJ("#divName").style("display","none");
				//set lblName innterHTML = name
				$SJ("#lblName").innerHTML(name);
				//set divMax to visable
				$SJ("#divMax").style("display","block");
				$SJ("#divResult").innerHTML("");
				$SJ("#txtMax").focus();
			}
			else{
				$SJ("#divResult").innerHTML("<h2>Name must consist of only character<h2>");
				//Change color of divResult. animiate for 500ms and rewind color back to original color
				$SJ("#divResult").changeColor(500,"rgb(255,0,0)","rewind");
			}
		});
		//btnMax on click
		$SJ("#btnMax").click(function(){
			//get max num
			var maxInput = $SJ("#txtMax").value();
			//check input is an intger
			if(/^\d+$/.test(maxInput)){
				//number greater than 0
				if(+maxInput > 0){
					$SJ("#divMax").style("display","none");
					$SJ("#divGame").style("display","block");
					$SJ("#lblMin").innerHTML(1);
					$SJ("#lblMax").innerHTML(maxInput);
					$SJ("#txtGuess").focus();
					max = +maxInput;
					min = 1;
					//Create random number from 1 to max num
					answer = Math.floor(Math.random()*maxInput+1);
				}
			}
			else{
				$SJ("#divResult").innerHTML("<h2>Max must be an positve whole number<h2>");
				$SJ("#divResult").changeColor(500,"rgb(255,0,0)","rewind");
			}
		});
		//btnGuess on click
		$SJ("#btnGuess").click(function(){
			//get guess
			var guess = +$SJ("#txtGuess").value();
			//check if guess in an intger
			if(/^\d+$/.test(guess)){
				//guess is under min
				if(guess < min){
					$SJ("#divResult").innerHTML("<h2>Number Entered ("+ guess +") is below Range<h2>");
					$SJ("#divResult").changeColor(500,"rgb(255,0,0)","rewind");
				}
				//guess is above max
				else if(guess > max){
					$SJ("#divResult").innerHTML("<h2>Number Entered ("+ guess +") is above Range<h2>");
					$SJ("#divResult").changeColor(500,"rgb(255,0,0)","rewind");
					$SJ("#lblMax").innerHTML(max);
				}
				//guess is in range
				else{
					//guess in lower then answer
					if(guess < answer){
						min = guess + 1;
						$SJ("#divResult").innerHTML("<h2>" + guess + " Low<h2>");
						$SJ("#divResult").changeColor(500,"rgb(255,140,0)","rewind");
						$SJ("#lblMin").innerHTML(min);
					}
					//guess in higher then answer
					else if(guess > answer){
						max = guess - 1;
						$SJ("#divResult").innerHTML("<h2>" + guess + " High<h2>");
						$SJ("#divResult").changeColor(500,"rgb(255,140,0)","rewind");
						$SJ("#lblMax").innerHTML(max);
					}
					//guess is equal to answer
					else{
						$SJ("#divResult").innerHTML("<h2>Winner<h2>");
						$SJ("body").style("background-color","rgb(0,204,0)");
						//winner pop up message
						alert("You win " + name + " play again");
						$SJ("body").style("background-color","white");
						$SJ("#divGame").style("display","none");
						$SJ("#divMax").style("display","block");
						$SJ("#divResult").innerHTML("");
						$SJ("#txtMax").value("");
						$SJ("#txtMax").focus();
					}
				}
			}
			//guess was no a positive intger 
			else{
				$SJ("#divResult").innerHTML("<h2>Your guess needs to be a whole number<h2>");
				$SJ("#divResult").changeColor(500,"rgb(255,0,0)","rewind");
			}
			$SJ("#txtGuess").value("");
		});
		//txtName on key down
		$SJ("#txtName").keydown(function(event){
			if(event.keyCode == 13){
		        $SJ("#btnName").click();
		    }
		});
		//txtMax on key down
		$SJ("#txtMax").keydown(function(event){
			if(event.keyCode == 13){
		        $SJ("#btnMax").click();
		    }
		});
		//txtGuess on key down
		$SJ("#txtGuess").keydown(function(event){
			if(event.keyCode == 13){
		        $SJ("#btnGuess").click();
		    }
		});
	});	