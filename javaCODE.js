// this one related to automatic  WELCOM image rotation
document.addEventListener("DOMContentLoaded", function () {
    const carouselImages = document.querySelectorAll(".carousel-img");
    let currentImageIndex = 0;

    function rotateImages() {
        carouselImages[currentImageIndex].style.opacity = 0;
        currentImageIndex = (currentImageIndex + 1) % carouselImages.length;
        carouselImages[currentImageIndex].style.opacity = 1;
    }

    setInterval(rotateImages, 3000); // Change image every 3 seconds
});
////itis not working



