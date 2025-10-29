document.addEventListener("DOMContentLoaded", () => {
    // === Menu toggle cho Header (User pages) ===
    const menuToggle = document.getElementById("menu-toggle");
    const navList = document.querySelector(".nav-list");

    if (menuToggle && navList) {
        menuToggle.addEventListener("click", () => {
            navList.classList.toggle("active");
        });

        // Đóng menu khi click bên ngoài
        document.addEventListener("click", (e) => {
            if (!menuToggle.contains(e.target) && !navList.contains(e.target)) {
                navList.classList.remove("active");
            }
        });
    }

    // === Menu toggle cho Admin Sidebar ===
    const adminMenuToggle = document.getElementById("menuToggle");
    const adminSidebar = document.getElementById("sidebar");

    if (adminMenuToggle && adminSidebar) {
        adminMenuToggle.addEventListener("click", () => {
            adminSidebar.classList.toggle("sidebar-open");
        });

        // Đóng sidebar khi click vào overlay (mobile)
        const mainContent = document.querySelector(".main-content");
        if (mainContent) {
            mainContent.addEventListener("click", () => {
                if (window.innerWidth <= 991) {
                    adminSidebar.classList.remove("sidebar-open");
                }
            });
        }
    }
});