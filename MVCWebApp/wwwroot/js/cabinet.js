function toggleFields() {
    var labels = document.querySelectorAll('.input-field');
  
    labels.forEach(function(label) {
      var input = label.querySelector('input');
      if (!input) {
        var labelText = label.textContent.split(':')[0];
        var newInput = document.createElement('input');
        newInput.setAttribute('type', 'text');
        newInput.setAttribute('value', labelText);
        label.innerHTML = '';
        label.appendChild(newInput); // Добавляем новое поле ввода
      }
    });
  }
  
  function applyChanges() {
        var inputs = document.querySelectorAll('.input-field input');
      
        inputs.forEach(function(input) {
          var parent = input.parentNode;
          var labelText = parent.textContent.split(':')[0];
          parent.textContent = `${labelText} ${input.value}`;
        });
      
  }
  