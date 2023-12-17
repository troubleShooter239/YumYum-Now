let storedStreet = '';

function openModal() {
    document.getElementById('modalOverlay').style.display = 'block';
}

function closeModal() {
    document.getElementById('modalOverlay').style.display = 'none';
}

function updateAddress(event) {
    event.preventDefault(); // Предотвращаем стандартное поведение кнопки "Сохранить"

    const street = document.querySelector('.search input').value;

    storedStreet = '';

    if (street) {
        storedStreet = 'Улица. ' + street;
    }

    const streetInfo = document.querySelector('.address-info p');
    streetInfo.innerHTML = storedStreet;
    closeModal();
}

document.querySelector('.open-button').addEventListener('click', openModal);
document.querySelector('.cancel-button').addEventListener('click', closeModal);
document.querySelector('.save-button').addEventListener('click', updateAddress);
