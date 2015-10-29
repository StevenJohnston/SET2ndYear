<!--
	Name: Steven Johnston
	File: client.asp
	Assignment: Server Hi Lo Assignment #3
	Date: 10/28/2015
	Purpose: The client html used for both the PHP and ASP vesions
-->
<html>
<head>
	<title>Client SErver Hi-Lo Game</title>
	<script src='js/sjquery.js'></script>
	<script src='js/main.js'></script>
</head>
<body>
	<!--Div hold the fullscreen play again button. Allows for easy hidding-->
	<div style='display:none; width:100%;height:100%;' id='divPlayAgain'>
		<button id='btnPlayAgain' style='width:100%;height:100%;'>Play Again</button>
	</div>
	<!--Div to hold entire game. Allows for easy hidding-->
	<div id='divMain'>
		<h1 id='chlg'>Client High Low Game</h1>
		<h2>Steven Johnston</h2>
		<!--Div to hold the enter name content-->
		<div id='divName'>
			<h3>Name: <input type='text' id='txtName'>
			<button id='btnName'>Submit</button></h3>
		</div>
		<!--Div to hold the enter max content-->
		<div id='divMax' style='display:none'>
			<h3>Hi, <label id='lblName'></label> enter maximum guess number</h3>
			<input type='text' id='txtMax'>
			<button id='btnMax'>Set Max</button>
		</div>
		<!--Div to hold the guessing content for the game-->
		<div id='divGame' style='display:none'>
			<div id='divGuess'>
				<input type='text' id='txtGuess'>
				<button id='btnGuess'>Guess</button>
			</div>
		</div>
	</div>
	<!--This div will hold the messages that are recived from the server. Can be errors or just updated information-->
	<div id='divResult'>
	</div>
</body>
</html>
