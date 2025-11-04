// ============================================
// MAIN.JS - FIXED VERSION
// Xử lý menu toggle cho cả User pages và Admin
// ============================================

document.addEventListener("DOMContentLoaded", () => {
    console.log("✅ main.js loaded successfully"); // Debug: Kiểm tra file đã load

    // ==========================================
    // MENU TOGGLE CHO HEADER (USER PAGES)
    // ==========================================
    const menuToggle = document.getElementById("menu-toggle");
    const navList = document.querySelector(".nav-list");

    console.log("🔍 Menu Toggle element:", menuToggle); // Debug
    console.log("🔍 Nav List element:", navList); // Debug

    if (menuToggle && navList) {
        console.log("✅ Menu toggle elements found!");

        // ✅ SỰ KIỆN CLICK VÀO NÚT MENU TOGGLE
        menuToggle.addEventListener("click", (e) => {
            e.preventDefault(); // Ngăn hành vi mặc định
            e.stopPropagation(); // Ngăn sự kiện lan truyền

            console.log("🎯 Menu toggle clicked!"); // Debug

            // Toggle class 'active' để hiện/ẩn menu
            navList.classList.toggle("active");

            // Cập nhật aria-expanded cho accessibility
            const isExpanded = navList.classList.contains("active");
            menuToggle.setAttribute("aria-expanded", isExpanded);

            console.log("📱 Menu is now:", isExpanded ? "OPEN" : "CLOSED");
        });

        // ✅ ĐÓNG MENU KHI CLICK VÀO LINK TRONG MENU (UX TỐT HƠN TRÊN MOBILE)
        const navLinks = navList.querySelectorAll('.nav-link');
        navLinks.forEach(link => {
            link.addEventListener('click', () => {
                // Chỉ đóng menu khi ở chế độ mobile
                if (window.innerWidth <= 991) {
                    navList.classList.remove('active');
                    menuToggle.setAttribute("aria-expanded", "false");
                    console.log("🔗 Nav link clicked, menu closed");
                }
            });
        });

        // ✅ ĐÓNG MENU KHI CLICK BÊN NGOÀI
        document.addEventListener("click", (e) => {
            // Kiểm tra xem click có nằm trong menu toggle hoặc nav list không
            if (!menuToggle.contains(e.target) && !navList.contains(e.target)) {
                navList.classList.remove("active");
                menuToggle.setAttribute("aria-expanded", "false");
                console.log("🖱️ Clicked outside, menu closed");
            }
        });

        // ✅ ĐÓNG MENU KHI RESIZE TỪ MOBILE LÊN DESKTOP
        window.addEventListener('resize', () => {
            if (window.innerWidth > 991) {
                navList.classList.remove('active');
                menuToggle.setAttribute("aria-expanded", "false");
            }
        });

    } else {
        console.warn("⚠️ Menu toggle or nav list NOT FOUND!"); // Debug warning
        if (!menuToggle) console.error("❌ Element with id='menu-toggle' not found");
        if (!navList) console.error("❌ Element with class='nav-list' not found");
    }

    // ==========================================
    // MENU TOGGLE CHO ADMIN SIDEBAR
    // ==========================================
    const adminMenuToggle = document.getElementById("menuToggle");
    const adminSidebar = document.getElementById("sidebar");

    if (adminMenuToggle && adminSidebar) {
        console.log("✅ Admin menu toggle found!");

        adminMenuToggle.addEventListener("click", () => {
            adminSidebar.classList.toggle("sidebar-open");
            console.log("🎯 Admin sidebar toggled");
        });

        // Đóng sidebar khi click vào overlay (mobile)
        const mainContent = document.querySelector(".main-content");
        if (mainContent) {
            mainContent.addEventListener("click", () => {
                if (window.innerWidth <= 991) {
                    adminSidebar.classList.remove("sidebar-open");
                    console.log("📱 Admin sidebar closed (mobile)");
                }
            });
        }
    }
});