document.getElementById("profileButton").onclick = function () {
  window.location.href = "/Home/Profile";
};
document.getElementById("confidentialityButton").onclick = function () {
  window.location.href = "/Home/Confidentiality";
};
document.getElementById("orderHistoryButton").onclick = function () {
  window.location.href = "/Profile/OrderHistory";
};
document.addEventListener("DOMContentLoaded", function () {
  // Получаем текущий URL страницы
  var currentUrl = window.location.href;

  // Получаем все кнопки и перебираем их
  var buttons = document.querySelectorAll(".button-panel");
  buttons.forEach(function (button) {
    // Получаем атрибут href кнопки
    var buttonUrl = button.id.toLowerCase() + ".html"; // Предполагается, что ваши страницы имеют такие же имена, как идентификаторы кнопок

    // Добавляем обработчик события для изменения стилей при наведении
    button.addEventListener("mouseover", function () {
      // Убираем класс "active" у всех кнопок
      buttons.forEach(function (btn) {
        btn.classList.remove("active");
      });

      // Добавляем класс "active" к текущей наведенной кнопке
      button.classList.add("active");
    });

    // Добавляем обработчик события для изменения стилей при убирании курсора
    button.addEventListener("mouseout", function () {
      // Добавляем класс "active" только к активной кнопке
      if (currentUrl.includes(buttonUrl)) {
        button.classList.add("active");
      } else {
        // Убираем класс "active" у всех кнопок, кроме активной
        buttons.forEach(function (btn) {
          if (!btn.classList.contains("active")) {
            btn.classList.remove("active");
          }
        });
      }
    });
  });
});
