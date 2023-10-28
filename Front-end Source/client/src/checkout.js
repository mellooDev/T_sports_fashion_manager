// const form = document.getElementById('form');
// const username = document.getElementById('username');
// const email = document.getElementById('email');
// const password = document.getElementById('address');

// form.addEventListener('submit', (event) =>{
//     event.preventDefault();

//     Validate();
// })

// const SuccessMsg = (usernameVal) => {
//     let formContr = document.getElementsByClassName('inputBox');
//     var Count = formContr.length - 1;
//     for(var i = 0; i < formContr.length; i++){
//         if(formContr[i].className === "inputBox success"){
//             var sRate = 0 + i;
//             console.log(sRate);
//             sendData(usernameVal, sRate, Count);
//         }
//         else{
//             return false;
//         }
//     }
// }

// const isEmail = (emailVal) =>{
//     var atSymbol = emailVal.indexOf('@');
//     if(atSymbol < 1) return false;
//     var dot = emailVal.lastIndexOf('.');
//     if(dot <= atSymbol + 2) return false;
//     if(dot === emailVal.length -1) return false;
//     return true;
// }

// function Validate(){
//     const usernameVal = username.value.trim();
//     const emailVal = email.value.trim();
//     const passwordVal = password.value.trim();

//     //username
//     if(usernameVal === ""){
//         setErrorMsg(username, 'first name cannot be blank');
//     }
//     else if(usernameVal.length <=2){
//         setErrorMsg(username, 'min 3 char');
//     }
//     else{
//         setSuccessMsg(username);
//     }

//     //email
//     if(emailVal === ""){
//         setErrorMsg(email, 'email cannot be blank');
//     }
//     else if(!isEmail(emailVal)){
//         setErrorMsg(email, 'email is not valid');
//     }
//     else{
//         setSuccessMsg(email);
//     }

//     //password
//     if(passwordVal === ""){
//         setErrorMsg(password, 'password cannot be blank');
//     }
//     else if(passwordVal.length <= 7){
//         setErrorMsg(password, 'min 8 char');
//     }
//     else{
//         setSuccessMsg(password);
//     }

// }

// function setErrorMsg(input, errormsgs){
//     const formControl = input.parentElement;
//     const small = formControl.querySelector('small');
//     formControl.className = "inputBox error";
//     small.innerText = errormsgs;
// }

// function setSuccessMsg(input){
//     const formControl = input.parentElement;
//     formControl.className = "inputBox success";
// }

const submitBtn = document.querySelector('.submit-btn');

submitBtn.addEventListener('click', function(){
    alert('Check out successfully!');
});