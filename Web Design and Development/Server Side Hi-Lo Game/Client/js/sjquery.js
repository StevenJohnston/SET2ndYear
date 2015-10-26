
/*
	Make shift JQUERY ($SJ)
	In seperate script tag so i can make it into a library
*/
var elementCallList = [];
//<script type="text/javascript">
//Name: $SJ ???
//Decsription: Function expression to allow for selection of DOM elements. Some methods can also be
//				run on specified element. (methods listed in return case)
//Param:
//	element: The Dom element to select
//	stackLimiter: If set will not create a second version of its self to save the stack from EXPLODING
//Return:
//	An array with a list of all the methods
var $SJ = function(elementIn) {
	var thisElement ; //Second version of its self allows for easier method calls (on same element)
	var elements = []; //Helps return an array rather then a nodeList

	//element must be of type string
	if(typeof elementIn == "string"){
		//How to select element (by id,name,tag,classname)
		var newElements;
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
					elements.push(window);
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
	GiveIDifNotExist(elements);
	allocateElementsInList(elements);
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
					queue(elements[element].id,function(element,val){element.innerHTML = val;},val,0);
	        	}
				return $SJObj;
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
        delay: function(val)
        {
        	for(var element in elements)
        	{
				queue(elements[element].id,function(element,val){},val,val);
        	}
			return $SJObj;
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
	        	queue(elements[element].id,function(element,func){element.onload = func;},func,0);
	        }
			return $SJObj;
        },
		//Name: focus
		//Decsription: Sets focus to element
		//Param:
		//
		//Return:
		//
        focus: function(index){

        	if(index == undefined) index = 0;
			if(index >= elements.length) index = 0;
			queue(elements[index].id,function(element,val){element.focus();},undefined,0)
			return $SJObj;
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
	        		queue(elements[element].id, function(element,obj){element.onclick = obj;},func,0);
	        	}
	        }
			return $SJObj;
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
							queue(elements[element].id,function(element,obj){element.style.display=obj;},val,0);
	        			}
	        			break;
	        		case "background-color":
					case "backgroundColor":
					case "backgroundcolor":
	        			if(val == undefined)
	        			{
	        				return elements[element].style.backgroundColor;
	        			}
	        			else
	        			{
							queue(elements[element].id,function(element,obj){element.style.backgroundColor=obj;},val,0);
	        			}
	        			break;
	        		default:
	        			break;
	        	}
	        }
			return $SJObj;
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
					queue(elements[element].id,function(element,obj){element.value=obj;},val,0);
	        	}else{
	        	   	return elements[element].value;
	        	}
	        }
			return $SJObj;
        },
		//Name: setAttr
		//Decsription: set Attribute of elements to val
		//Param:
		//	name: The name of the attribute to set
		//	val: The value to set the attribute to. If not defined nothing happens
		//Return:
		//
        setAttr: function(name, val){
			if(val != undefined)
			{
				for (var element in elements)
				{
					queue(elements[element].id,function(element,obj){element.setAttribute(obj.name,obj.val);},{"name":name,"val":val},0);
				}
			}
			return $SJObj;
        },
		//Name: getAttr
		//Decsription: Get attribute of element(s)
		//Param:
		//	name: The name of the element to get
		//	val: if set get elements with matching attribute name and value else get all element names
		//Return:
		//	Elements that match parameters
		getAttr: function(name,val)
		{
			var valElements =[];
        	for (var element in elements)
        	{
	        	if(val == undefined){
	        		valElements.push(elements[element].getAttribute(name));
	        	}else{
		        	if(elements[element].hasAttribute(name))
					{
						if(elements[element].getAttribute(name) == val)
						{
							valElements.push(elements[element]);
						}
					}
	        	}
	        }
			return valElements;
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
				queue(elements[element].id,function(element,obj){element.onkeydown = obj;},funcs,0);
        	}
			return $SJObj;
        },
		//Name: selectedId
		//Decsription: gets selected id of element
		//Param:
		//
		//Return:
		//	selecteid(s) of element
        selectedId: function()
        {
        	for (var element in elements)
        	{
        		return elements[element].options[elements[element].selectedIndex].value;
        	}
        },
		//Name: checked
		//Decsription: sets checked value of elements to val (true or false)
		//Param:
		//	val: value to set elements to
		//Return:
		//
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
					queue(elements[element].id,function(element,obj){element.checked = obj;},val,0);
	        	}
	        }
			return $SJObj;
        },
		//Name: repeat
		//Desciption: Repeats
		//Param:
		//
		//Return:
		//
		repeat: function(func,times)
		{
			for(var i= 0 ; i < times; i++)
			{

			}
		}
    }
};

//Get next free id and return it
var nextID =0;
function getNextID()
{
	for(;document.getElementById(nextID) != undefined; nextID++){}
	return nextID++;
}
//Gives each element a unique ID if it doesnt have one
function GiveIDifNotExist(elementArray)
{
	for(var element in elementArray)
	{
		if(elementArray[element].id == "" || elementArray[element].id == undefined)
		{
			elementArray[element].id = getNextID();
		}
	}
}

function allocateElementsInList(elementArray)
{
	for(var element in elementArray)
	{
		if(elementCallList[elementArray[element].id] == undefined)
		{
			elementCallList[elementArray[element].id] = {};
			elementCallList[elementArray[element].id].elementObject = elementArray[element];
			elementCallList[elementArray[element].id].running = false;
			elementCallList[elementArray[element].id].callList = [];
		}
	}
}

function startQueue(index)
{
	if(elementCallList[index].running == false)
	{
		elementCallList[index].running = true;
		dequeue(index);
	}
}
function queue(elementIndex,func,val,delay)
{
	elementCallList[elementIndex].callList.push({"func" : func,
													"val" : val,
													"delay" : delay});
	startQueue(elementIndex);
}
function dequeue(elementIndex)
{
	var thisEle = elementCallList[elementIndex];
	if(thisEle.callList.length != 0)
	{
		setTimeout(function(){
			thisEle.callList[0].func(thisEle.elementObject,thisEle.callList[0].val);
			thisEle.callList.shift();
			dequeue(elementIndex);
		},thisEle.callList[0].delay);
	}
	thisEle.running = false;
	//var thisEle = elementCallList[elementIndex];
	//thisEle.callList[0].func(thisEle.elementObject,thisEle.callList[0].val);
}
function $SJObj()
{
	$SJ(elementIn,"defined");
}