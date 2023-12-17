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
function validateData() {
  var emailOrPhone = document.getElementById("emailOrPhone").value;
  var emailOrPhoneError = document.getElementById("emailOrPhone-error");
  var emailOrPhonelabel = document.getElementById("emailOrPhone-label");
  var forgotPasswordPanel = document.querySelector(".validate-panel");
  var emailRegex =
    /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/iu;
  var phoneRegex =
    /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

  if (emailOrPhone.trim() === "") {
    emailOrPhoneError.innerHTML = "Please enter an email or phone number.";
    forgotPasswordPanel.style.display = "flex"; // Показываем панель с ошибкой
    emailOrPhonelabel.style.color = "red";
    emailOrPhoneError.style.color = "red";
    return;
  }

  // Check if it's an email
  if (emailRegex.test(emailOrPhone)) {
    emailOrPhoneError.innerHTML = "";
    forgotPasswordPanel.style.display = "none"; // Скрываем панель, так как ошибок нет
    emailOrPhonelabel.style.color = "";
    emailOrPhoneError.style.color = "";
  } else if (phoneRegex.test(emailOrPhone)) {
    // Check if it's a phone number
    emailOrPhoneError.innerHTML = "";
    forgotPasswordPanel.style.display = "none"; // Скрываем панель, так как ошибок нет
    emailOrPhonelabel.style.color = "";
    emailOrPhoneError.style.color = "";
  } else {
    // Display error message for invalid input
    if (emailOrPhone.includes("@")) {
      emailOrPhoneError.innerHTML = "Inccorect email.";
      emailOrPhonelabel.style.color = "red";
      emailOrPhoneError.style.color = "red";
    } else {
      emailOrPhoneError.innerHTML = "Incorrect pnone number.";
      emailOrPhonelabel.style.color = "red";
      emailOrPhoneError.style.color = "red";
    }
    forgotPasswordPanel.style.display = "flex"; // Показываем панель с ошибкой
  }
}
