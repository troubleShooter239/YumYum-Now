function validatePassword() {
  var password = document.getElementById("password").value;
  var passwordError = document.getElementById("password-error");
  var passwordLabel = document.getElementById("password-label");

  if (password.length < 8 || password.length > 16 || !/[A-Z]/.test(password)) {
    // Display error message and update styles
    passwordError.innerHTML =
      "Invalid password. Please check the requirements.";
    passwordError.style.color = "red";
    document.getElementById("email").style.borderColor = "red";
    document.getElementById("password").style.borderColor = "red";
    passwordLabel.style.color = "red";
  } else {
    // Clear the error message and reset styles
    passwordError.innerHTML = "";
    passwordError.style.color = ""; // Reset to default color
    document.getElementById("email").style.borderColor = ""; // Reset to default color
    document.getElementById("password").style.borderColor = ""; // Reset to default color
    passwordLabel.style.color = ""; // Reset to default color
    // Perform your login logic here
  }
}
function validateEmail() {
    var email = document.getElementById("email").value;
    var emailError = document.getElementById("email-error");
    var emailLabel = document.getElementById("email-label");
  
    // Regular expression for a basic email validation
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  
    if (!emailRegex.test(email)) {
      // Display error message and update styles
      emailError.innerHTML = "Invalid email. Please enter a valid email address.";
      emailError.style.color = "red";
      document.getElementById("email").style.borderColor = "red";
      document.getElementById("password").style.borderColor = ""; // Сбросим стили для поля password
      emailLabel.style.color = "red";
    } else {
      // Clear the error message and reset styles
      emailError.innerHTML = "";
      emailError.style.color = ""; // Reset to default color
      document.getElementById("email").style.borderColor = ""; // Reset to default color
      document.getElementById("password").style.borderColor = ""; // Reset to default color
      emailLabel.style.color = ""; // Reset to default color
    }
  }
  

function validateData() {
  validateEmail();
  validatePassword();
}
