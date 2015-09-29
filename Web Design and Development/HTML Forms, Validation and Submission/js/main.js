var StuFacSelected = false;
$SJ("window").ready(function(){
	$SJ("#rStudent").click(function()
	{
		StuFacSelected = true;
		var checkboxes = $SJ("input").getAttr("type","checkbox");
		for(checkBox in checkboxes)
		{
			checkboxes[checkBox].checked = false;
		}
		$SJ("#divFacCheck").style("display","none");
		$SJ("#divStuCheck").style("display","block");
	});
	$SJ("#rFaculty").click(function()
	{
		StuFacSelected = true;
		var checkboxes = $SJ("input").getAttr("type","checkbox");
		for(checkBox in checkboxes)
		{
			checkboxes[checkBox].checked = false;
		}
		$SJ("#divFacCheck").style("display","block");
		$SJ("#divStuCheck").style("display","none");
	});

});

function validateRegisterForm()
{
	var validationPass = true;
	var errorMessage = "";
	if(!/^[a-zA-Z ]+$/.test($SJ("#txtName").value())){
		validationPass = false;
		errorMessage += "Name must consist of only letters and spaces <br>";
	}
	if(!/^[a-zA-Z0-9 ]+$/.test($SJ("#txtStreet").value())){
		validationPass = false;
		errorMessage += "Street must consist of only letters, numbers, and spaces <br>";
	}
	if(!/^[a-zA-Z][0-9][a-zA-Z][ ]?[0-9][a-zA-Z][0-9]$/.test($SJ("#txtPostal").value()))
	{
		validationPass = false;
		errorMessage += "Postal must be in format A1A1A1 with optional space <br>";
	}
	if(!/^[a-zA-Z]+$/.test($SJ("#txtCity").value())){
		validationPass = false;
		errorMessage += "City must be only letters <br>";
	}
	if($SJ("#sProvince").selectedId()=="NA")
	{
		validationPass = false;
		errorMessage += "Please select a province from the drop down <br>";
	}
	if(StuFacSelected == false)
	{
		validationPass = false;
		errorMessage += "Please select Student or faculty form the radio buttons <br>";
	}
	if(!validationPass)
	{
		$SJ("#error").innerHTML(errorMessage);
		$SJ("#error").style("backgroundColor","red");
	}
	return validationPass;
}