// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$(document).ready(function() {
    let slideIndex = 0;
    showSlide(slideIndex);
  
    setInterval(function() {
      showSlide(++slideIndex);
    }, 5000); // Задайте интервал времени в миллисекундах (в данном случае 5 секунд)
  
    function showSlide(n) {
      const slides = $(".slide");
      if (n >= slides.length) {
        slideIndex = 0;
      }
      if (n < 0) {
        slideIndex = slides.length - 1;
      }
      slides.removeClass("active");
      slides.eq(slideIndex).addClass("active");
  
      // Загрузка изображения для активного слайда
      const imageUrl = slides.eq(slideIndex).data("image");
      slides.eq(slideIndex).css("background-image", `url(${imageUrl})`);
    }
  });
