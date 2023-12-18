function showOrderInfo(itemName, quantity, address) {
    const modal = document.getElementById("modalOverlay");
    const orderInfo = document.getElementById("orderInfo");
  
    orderInfo.innerHTML = `
      <h2>Информация о заказе</h2>
      <p>Товар: ${itemName}</p>
      <p>Количество: ${quantity}</p>
      <p>Адрес: ${address}</p>
      <!-- Добавьте другие данные о заказе -->
    `;
  
    modal.style.display = "block";
  }
  
  function closeModal() {
    const modal = document.getElementById("modalOverlay");
    modal.style.display = "none";
  }
  