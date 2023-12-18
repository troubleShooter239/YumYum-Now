$(document).ready(function () {
	let slideIndex = 0
	showSlide(slideIndex)

	setInterval(showSlide(++slideIndex), 5000)
	function showSlide(n) {
		const slides = $('.slide')
		if (n >= slides.length) {
			slideIndex = 0
		}
		if (n < 0) {
			slideIndex = slides.length - 1
		}
		slides.removeClass('active')
		slides.eq(slideIndex).addClass('active')

		const imageUrl = slides.eq(slideIndex).data('image')
		slides.eq(slideIndex).css('background-image', `url(${imageUrl})`)
	}

	toggle()

	ymaps.ready(init)
})

function toggle() {
	$('.gram-button').on('click', () => {
		$('.gram-button').removeClass('active')
		$(this).addClass('active')
	})
}

function scrollToElement(id) {
	const element = document.getElementById(id)
	if (element) {
		const elementPosition = element.offsetTop
		const duration = 1000
		const start = window.pageYOffset // ! DEPRECATED, need to fix !
		const distance = elementPosition - start
		let startTime = null

		function animation(currentTime) {
			if (startTime === null) {
				startTime = currentTime
			}

			const timeElapsed = currentTime - startTime
			const run = ease(timeElapsed, start, distance, duration)
			window.scrollTo(0, run)

			if (timeElapsed < duration) {
				requestAnimationFrame(animation)
			}
		}

		function ease(t, b, c, d) {
			t /= d / 2
			if (t < 1) return (c / 2) * t * t + b
			t--
			return (-c / 2) * (t * (t - 2) - 1) + b
		}

		requestAnimationFrame(animation)
	}
}

function init() {
	var myMap = new ymaps.Map('map', {
		center: [53.955008, 27.69267],
		zoom: 15,
	})

	myPlacemark = new ymaps.Placemark([53.955008, 27.69267], {
		hintContent: 'Trash',
		balloonContent: 'Trash Trash Trash',
	})

	myMap.geoObjects.add(myPlacemark)
}
