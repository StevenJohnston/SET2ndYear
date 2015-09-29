/*
	Make shift JQUERY ($SJ)
	In seperate script tag so i can make it into a library 
*/

//<script type="text/javascript">
//Name: $SJ ???
//Decsription: Function expression to allow for selection of DOM elements. Some methods can also be 
//				run on specified element. (methods listed in return case)
//Param:
//	element: The Dom element to select
//	stackLimiter: If set will not create a second version of its self to save the stack from EXPLODING
//Return:
//	An array with a list of all the methods
var $SJ = function(elementIn,stackLimiter) {
	//Second version of its self allows for easier method calls (on same element)
	var thisElement ;
	var elements = [];
	if(stackLimiter == undefined){
		thisElement = $SJ(elementIn,"defined");
	}

	//element must be of type string
	if(typeof elementIn == "string"){
		//How to select element (by id,name,tag,classname)
		var newElements;
		if(stackLimiter == undefined){
			var thisElement = $SJ(elementIn,"defined");
		}
		switch(elementIn.charAt(0)){
			case '#': //by id
				elements.push(document.getElementById(elementIn.substring(1)));
				break;
			case("."): //by className
				newElements = document.getElementsByClassName(elementIn.substring(1));
				for(var i = 0; i < newElements.length;i++)
				{
					elements.push(newElements[i]);
				}
				break;
			case("*"): //by localName
				newElements = document.getElementsByName(elementIn.substring(1));
				for(var i = 0; i < newElements.length;i++)
				{
					elements.push(newElements[i]);
				}
				break;
			default: //by TagName
				if(elementIn == "window"){
					elements.push(document.getElementsByTagName(elementIn));
				}
				else
				{
					newElements = document.getElementsByTagName(elementIn);
					for(var i = 0; i < newElements.length;i++)
					{
						elements.push(newElements[i]);
					}
				}
				break;
		}
	}

	//Array of methods
    return {
    	//Name: innerHTML
		//Decsription: Sets innerHTML equal to parameter
		//Param:
		//	html: What to be set in the innerHTML of element
		//Return:
		//	
        innerHTML: function(val) {
        	if(val != undefined)
        	{
	        	for(var element in elements)
	        	{
	            	elements[element].innerHTML = val;
	        	}
	        	return thisElement;
	        }else
	        {
	        	var innerHTMLs = [];
	        	for(var element in elements)
	        	{
	            	innerHTMLs.push(elements[element].innerHTML);
	        	}
	        	if(innerHTMLs.length == 1)
	        	{
	        		return innerHTMLs[0];
	        	}
	        	else
	        		return innerHTMLs;
	        }
        },
    	//Name: style
		//Decsription: set specified style attribute to specified value
		//Param: 
		//	attr: The style attribute to change
		//Return:
		//	value: Value to set attribute to
        style: function(attr,val) {
        	//Different types of style attributes
        	for (var element in elements)
        	{
	        	switch(attr){
	        		case "display":
	        			if(val == undefined)
	        			{
	        				return elements[element].style.display;
	        			}
	        			else
	        			{
	        				elements[element].style.display = val;
	        			}
	        			break;
	        		case "background-color":
	        			if(val == undefined)
	        			{
	        				return elements[element].style.backgroundColor;
	        			}
	        			else
	        			{
	        				elements[element].style.backgroundColor = val;
	        			}
	        			break;
	        		default:
	        			break;
	        	}
	        }
        	
        },
    	//Name: ready
		//Decsription: Set function to be called when element's onload function is called
		//Param:
		//	func: The function to set element's onload to
		//Return:
		//	???
        ready: function(func) {
        	for(var element in elements)
        	{
	        	if(elements[element] == document.getElementsByTagName("window")){
	        		elements[element] = window;
	        		elements[element].onload = func;
	        	}
	        }
        },
    	//Name: click
		//Decsription: Set function to be called when element's onclick function is called
		//Param:
		//	func: The function to set element's onclick to
		//Return:
		//	
        click: function(func){
        	for (var element in elements)
        	{
	        	if(func == undefined){
	        		//Credit to KooiInc
	        		//http://stackoverflow.com/questions/2705583/how-to-simulate-a-click-with-javascript
	        		var evObj = document.createEvent('Events');
					evObj.initEvent("click", true, false);
					elements[element].dispatchEvent(evObj);
	        	}
	        	else{
	        		elements[element].onclick = func;
	        	}
	        }
        },
    	//Name: value
		//Decsription: Get or Set the value of element
		//Param:
		//	val: if set, sets value of element equal to val else return element's current value
		//Return:
		//	
        value: function(val){
        	for (var element in elements)
        	{
	        	if(val != undefined){
	        		elements[element].value = val;
	        	}else{
	        	   	return elements[element].value;
	        	}
	        }
        },
    	//Name: changeColor
		//Decsription: Changes background color of element
		//Param: 
		//	time: How long the change color animation should take (milli seconds)
		//	toColor: Color to change to
		//	rewind: If set will return color back to oringal after animation
		//Return:
		//	
        changeColor: function(time,toColor,rewind)
        {
        	for (var element in elements)
        	{
	        	var speed =100;
	        	var current = 0;
	        	var last = time/speed/2;
	        	var shadeUp = true;

	        	var incrementer = 1;
	        	//Check if originalcolor attribute has been set if not create it
	        	var originalColor = thisElement.attr("originalcolor");
	        	if(originalColor == undefined){
					originalColor = window.getComputedStyle(elements[element],null).getPropertyValue("background-color");
	        		thisElement.attr("originalcolor",originalColor);
	        		thisElement.style("background-color",originalColor);
	        	}
	        	//Starting color
	        	var fromColor = originalColor.substring(originalColor.indexOf("(")+1,originalColor.length-1).split(", ");
	        	if(originalColor[4] != undefined){
	        		for (var i = fromColor.length - 1; i >= 0; i--) {
	        			fromColor[i] = 255;
	        		};
	        	}
	        	//ending color
	        	var toColor = toColor.substring(4,toColor.length-1).split(",");

	        	//check if element is already been colorchanged
	        	if(originalColor == elements[element].style.backgroundColor){
	        		var loop = setInterval(animateColor,speed);
	        	}

	        	//Name: animateColor
				//Decsription: Used in a setInterval call to animate color change
				//Param:
				//	
				//Return:
				//	
	        	function animateColor()
	        	{
					thisElement.attr("colorchange","1");
					//set color inbetween from and to color
					var newBack = "rgb(";
					for(var i = 0 ; i < 3 ; i++){
						newBack += (parseInt(fromColor[i]) + parseInt((parseInt(toColor[i]) - parseInt(fromColor[i]))/last)*current) + ",";
					}
					newBack = newBack.slice(0,-1);
					newBack += ")";
					elements[element].style.backgroundColor = newBack;
					
					current+= incrementer;
					if(current >=last && shadeUp == true){
						incrementer = -1;
						shadeUp = false;
						if(rewind == undefined){
							elements[element].style.backgroundColor = originalColor;
							clearInterval(loop);
						}
					}
					else if(current <= 0 && shadeUp == false){
						elements[element].style.backgroundColor = originalColor;
						clearInterval(loop);
					}
	        	}
	        }
        },
    	//Name: attr
		//Decsription: Get or Set attribute of element
		//Param:
		//	name: The name of the element to get
		//	val: The value to set the attribute to. If not set function will return current value of attribute
		//Return:
		//	
        attr: function(name,val){
        	for (var element in elements)
        	{
	        	if(val == undefined){
	        		return elements[element].getAttribute(name);
	        	}else{
		        	elements[element].setAttribute(name,val);
	        	}
	        }
        },
    	//Name: keydown
		//Decsription: Set function to be called when element's onkeydown function is called
		//Param:
		//	func: The function to set element's onkeydown to
		//Return:
		//	
        keydown: function(func){
        	for (var element in elements)
        	{
        		elements[element].onkeydown = func;
        	}
        },
    	//Name: focus
		//Decsription: Sets focus to element 
		//Param:
		//	
		//Return:
		//	
        focus: function(){
        	for (var element in elements)
        	{
        		elements[element].focus();
        	}
        },
        selectedId: function()
        {
        	for (var element in elements)
        	{
        		return elements[element].options[elements[element].selectedIndex].value;
        	}
        },
        checked: function(val)
        {
        	for (var element in elements)
        	{
	        	if(val == undefined)
	        	{
	        		return elements[element].checked;
	        	}
	        	else
	        	{
	        		elements[element].checked = val;
	        	}
	        }
        },
        delay: function(val)
        {
        	setTimeout(pause, val);
        	var timedOut = false;
        	while(!timedOut)
        	{

        	}
        	return;
        	function pause()
        	{
        		timedOut =true;
        	}
        },
    }
};