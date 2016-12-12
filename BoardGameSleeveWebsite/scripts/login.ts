class LoginAdmin
{

}


var loginAdmin: LoginAdmin;
$(document).ready(() =>
{
    loginAdmin = new LoginAdmin();
})

function loginClicked()
{
	var $loginName = $("#loginName");
	var $loginPassword = $("#loginPassword");

	var name = $loginName.val();
	var password = $loginPassword.val();

	document.cookie = "username=" + name;
	document.cookie = "password=" + password;

	console.log(name);
	console.log(password);
}