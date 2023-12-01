function validatePassword() {
    var password = document.getElementById("password").value;
    var passwordError = document.getElementById("password-error");
    var passwordLabel = document.getElementById("password-label");

    if (password.length < 8 || password.length > 16 || !/[A-Z]/.test(password)) {
        // Display error message and update styles
        passwordError.innerHTML = "Invalid password. Please check the requirements.";
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