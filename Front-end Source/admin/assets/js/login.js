const container = document.getElementById('container');
const registerBtn = document.getElementById('register');
const loginBtn = document.getElementById('login');

registerBtn.addEventListener('click', () => {
    container.classList.add("active");
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});


var warningUsername = document.querySelector(".warning-username");
var warningUsernameLogin = document.querySelector(".warning-username-login");
var warningPasswordLogin = document.querySelector(".warning-pass-login");

function checkuserlogin(){
    var inputLogin = document.querySelector(".input_username_login").value
    // var check = /^[A-Z][a-zA-Z0-9]{7,}$/;
    if(inputLogin==""){
        warningUsernameLogin.innerHTML = '*Username cannot empty';
        warningUsernameLogin.style.display= 'block';
    }
    else{
        warningUsernameLogin.style.display= 'none';
    }
}

function checkpasslogin(){
    var passLogin = document.querySelector(".input_pass_login").value
    // var check = /^[A-Z][a-zA-Z0-9]{7,}$/;
    if(passLogin==""){
        warningPasswordLogin.innerHTML = '*Password cannot empty';
        warningPasswordLogin.style.display= 'block';
    }
    else{
        warningPasswordLogin.style.display= 'none';
    }
}

function checkinputuser(){
    var inputUser = document.querySelector(".input_username").value
    var check = /^[A-Z][a-zA-Z0-9]{7,}$/;
    if(inputUser==""){
        warningUsername.innerHTML = '*Username cannot empty';
        warningUsername.style.display= 'block';
    }
    else if(inputUser.length < 8) {
        warningUsername.innerHTML = '*Username must be at least 8 characters long';
        warningUsername.style.display= 'block';
    }
    else if(check.test(inputUser)===false) {
        warningUsername.innerHTML = '*Username is invalid';
        warningUsername.style.display= 'block';
    }
    else{
        warningUsername.style.display= 'none';
    }
}

var warningEmail = document.querySelector(".warning-email");
function checkinputemail(){
    var inputEmail = document.querySelector(".input_email").value
    var check = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if(inputEmail==""){
        warningEmail.innerHTML = '*Email cannot empty';
        warningEmail.style.display= 'block';
    }
    else if(check.test(inputEmail)===false) {
        warningEmail.innerHTML = '*Invalid email format';
        warningEmail.style.display= 'block';
    }
    else{
        warningEmail.style.display= 'none';
    }
}

var warningPass = document.querySelector(".warning-pass")
function checkinputpass(){
    var inputPass = document.querySelector(".input-pass").value
    var check = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if(inputPass==""){
        warningPass.innerHTML = '*Password cannot empty';
        warningPass.style.display= 'block';
    }
    else if (check.test(inputPass)===false) {
        warningPass.innerHTML = '*Password is invalid';
        warningPass.style.display= 'block';
    }
    else{
        warningPass.style.display= 'none'
    }
}

function login(event){
    event.preventDefault(); 
    var item = {}
    item.username = $('.input_username_login').val()
    item.password = $('.input_pass_login').val()
    $.ajax({
            type: "POST",
            url: current_url+"/api/Account/login",
            dataType: "json",
            contentType: 'application/json',
            data: JSON.stringify(item)
        }).done(function (data) {
            if (data != null && data.message != null && data.message != 'undefined') {
                alert(data.message);
            } else {
                localStorage.setItem("user", JSON.stringify(data));
                window.location.href = "./product.html";
            }
            
        }) .fail(function() {
            alert('Tài khoản hoặc mật khẩu không chính xác');
        }); 
    return
}

