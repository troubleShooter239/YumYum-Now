document.addEventListener('DOMContentLoaded', function () {
    const addressInfoInputs = document.querySelectorAll('.address-info .input-field input');
  
    addressInfoInputs.forEach(input => {
      const label = input.nextElementSibling;
  
      // Function to set styles when there is a value in the input field
      const setInitialStyles = () => {
        if (input.value.trim() !== '') {
          label.classList.add('filled');
          label.style.top = '-20px';
          label.style.fontSize = '30px';
        } else {
          label.classList.remove('filled');
          label.style.top = '50%';
          label.style.fontSize = '18px';
        }
      };
  
      // Call the function on page load
      setInitialStyles();
  
      // Focus event handler
      input.addEventListener('focus', () => {
        label.classList.add('filled');
        label.style.top = '-20px';
        label.style.fontSize = '30px';
      });
  
      // Blur event handler
      input.addEventListener('blur', () => {
        if (input.value.trim() === '') {
          label.classList.remove('filled');
          label.style.top = '50%';
          label.style.fontSize = '18px';
        }
      });
    });
  });
  