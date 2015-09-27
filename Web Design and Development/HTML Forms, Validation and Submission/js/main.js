var StuFacSelected = false;
$SJ("window").ready(function(){
	$SJ("#rStudent").click(function()
	{
		StuFacSelected =true;
		$SJ("#divStuCheck").style("display","block");
		$SJ("#divFacCheck").style("display","none");
	});
	$SJ("#rFaculty").click(function()
	{
		StuFacSelected =true;
		$SJ("#divFacCheck").style("display","block");
		$SJ("#divStuCheck").style("display","none");
	});

});

function validateRegisterForm()
{
	var validationPass = true;
	if(!/^[a-zA-Z]+$/.test($SJ("#txtName").value())){
		validationPass = false;
	}
	if(!/^[a-zA-Z]+$/.test($SJ("#txtStreet").value())){
		validationPass = false;
	}
	if(!/^[a-zA-Z][0-9][a-zA-Z][ ]?[0-9][a-zA-Z][0-9]$/.test($SJ("#txtPostal").value()))
	{
		validationPass = false;
	}
	if(!/^[a-zA-Z]+$/.test($SJ("#txtCity").value())){
		validationPass = false;
	}
	if($SJ("#sProvince").selectedId()=="NA")
	{
		validationPass = false;
	}
	if(StuFacSelected == false)
	{
		validationPass = false;
	}
	if(!validationPass)
		alert("eee");
	return validationPass;

}