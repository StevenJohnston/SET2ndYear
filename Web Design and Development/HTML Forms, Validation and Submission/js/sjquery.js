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
var $SJ = function(element,stackLimiter) {
	//Second version of its self allows for easier method calls (on same element)
	var thisElement;

	//element must be of type string
	if(typeof element == "string"){
		//How to select element (by id,name,tag,classname)
		switch(element.charAt(0)){
			case '#': //by id
				element = document.getElementById(element.substring(1));
				if(stackLimiter == undefined){
					var thisElement = $SJ("#"+element.id,"defined");
				}
				break;
			case("."): //by className
				element = document.getElementByClassName(element.substring(1));
				if(stackLimiter == undefined){
					var thisElement = $SJ("."+element.className,"defined");
				}
				break;
			case("["): //by localName
				element = document.getElementByName(element.substring(1));
				if(stackLimiter == undefined){
					var thisElement = $SJ("["+element.localName,"defined");
				}
				break;
			default: //by TagName
				if(element == "window"){
					element = document.getElementsByTagName(element);
				}
				else{
					element = document.getElementsByTagName(element)[0];
				}

				if(stackLimiter == undefined){
					var thisElement = $SJ(element,"defined");
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
        innerHTML: function(html) {
            element.innerHTML = html;
        },
    	//Name: style
		//Decsription: set specified style attribute to specified value
		//Param: 
		//	attr: The style attribute to change
		//Return:
		//	value: Value to set attribute to
        style: function(attr,value) {
        	//Different types of style attributes
        	switch(attr){
        		case "display":
        			element.style.display = value;
        			break;
        		case "background-color":
        			element.style.backgroundColor = value;
        			break;
        		default:
        			break;
        	}
        	
        },
    	//Name: ready
		//Decsription: Set function to be called when element's onload function is called
		//Param:
		//	func: The function to set element's onload to
		//Return:
		//	
        ready: function(func) {
        	if(element == document.getElementsByTagName("window")){
        		element = window;
        		element.onload = func;
        	}
        },
    	//Name: click
		//Decsription: Set function to be called when element's onclick function is called
		//Param:
		//	func: The function to set element's onclick to
		//Return:
		//	
        click: function(func){
        	if(func == undefined){
        		//Credit to KooiInc
        		//http://stackoverflow.com/questions/2705583/how-to-simulate-a-click-with-javascript
        		var evObj = document.createEvent('Events');
				evObj.initEvent("click", true, false);
				element.dispatchEvent(evObj);
        	}
        	else{
        		element.onclick = func;
        	}
        },
    	//Name: value
		//Decsription: Get or Set the value of element
		//Param:
		//	val: if set, sets value of element equal to val else return element's current value
		//Return:
		//	
        value: function(val){
        	if(val != undefined){
        		element.value = val;
        	}else{
        	   	return element.value;
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
        	var speed =100;
        	var current = 0;
        	var last = time/speed/2;
        	var shadeUp = true;

        	var incrementer = 1;
        	//Check if originalcolor attribute has been set if not create it
        	var originalColor = thisElement.attr("originalcolor");
        	if(originalColor == undefined){
				originalColor = window.getComputedStyle(element,null).getPropertyValue("background-color");
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
        	if(originalColor == element.style.backgroundColor){
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
				element.style.backgroundColor = newBack;
				
				current+= incrementer;
				if(current >=last && shadeUp == true){
					incrementer = -1;
					shadeUp = false;
					if(rewind == undefined){
						element.style.backgroundColor = originalColor;
						clearInterval(loop);
					}
				}
				else if(current <= 0 && shadeUp == false){
					element.style.backgroundColor = originalColor;
					clearInterval(loop);
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
        	if(val == undefined){
        		return element.getAttribute(name);
        	}else{
	        	element.setAttribute(name,val);
        	}
        },
    	//Name: keydown
		//Decsription: Set function to be called when element's onkeydown function is called
		//Param:
		//	func: The function to set element's onkeydown to
		//Return:
		//	
        keydown: function(func){
        	element.onkeydown = func;
        },
    	//Name: focus
		//Decsription: Sets focus to element 
		//Param:
		//	
		//Return:
		//	
        focus: function(){
        	element.focus();
        },
        selectedId: function()
        {
        	return element.options[element.selectedIndex].value;
        },
        checked: function(val)
        {
        	if(val == undefined)
        	{
        		return element.checked;
        	}
        	else
        	{
        		element.checked = val;
        	}
        },
        

    }
};