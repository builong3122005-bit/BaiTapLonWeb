document.addEventListener("DOMContentLoaded", () => {
  const menuToggle = document.getElementById("menu-toggle");
  const navList = document.querySelector(".nav-list");
  if (menuToggle) {
    menuToggle.addEventListener("click", () => {
      navList.classList.toggle("active");
    });
  }
});
