// ============================================
// MAIN.JS - COMPLETE FIX
// ============================================

document.addEventListener("DOMContentLoaded", () => {
    console.log("✅ main.js loaded");

    // ==========================================
    // MENU TOGGLE - USER PAGES
    // ==========================================
    const menuToggle = document.getElementById("menu-toggle");
    const navList = document.querySelector(".nav-list");

    if (!menuToggle || !navList) {

        return;
    }

    console.log("✅ Menu elements found!");

    // ===== TOGGLE MENU =====
    menuToggle.addEventListener("click", function (e) {
        e.preventDefault();
        e.stopPropagation(); // Ngăn sự kiện lan ra document

        const isCurrentlyActive = navList.classList.contains("active");

        if (isCurrentlyActive) {
            // Đang MỞ → ĐÓNG lại
            navList.classList.remove("active");
            menuToggle.setAttribute("aria-expanded", "false");
        } else {
            // Đang ĐÓNG → MỞ ra
            navList.classList.add("active");
            menuToggle.setAttribute("aria-expanded", "true");
        }
    });

    // ===== ĐÓNG KHI CLICK VÀO LINK =====
    const navLinks = navList.querySelectorAll('.nav-link');
    console.log(`📌 Found ${navLinks.length} nav links`);

    navLinks.forEach(link => {
        link.addEventListener('click', () => {
            if (window.innerWidth <= 991 && navList.classList.contains("active")) {
                navList.classList.remove('active');
                menuToggle.setAttribute("aria-expanded", "false");
            }
        });
    });

    // ===== ĐÓNG KHI CLICK BÊN NGOÀI =====
    // Sử dụng setTimeout để đảm bảo chạy SAU khi toggle hoàn tất
    let canCheckOutsideClick = true;

    menuToggle.addEventListener('click', () => {
        canCheckOutsideClick = false;
        setTimeout(() => {
            canCheckOutsideClick = true;
        }, 50);
    });

    document.addEventListener("click", (e) => {
        if (!canCheckOutsideClick) {
            return;
        }

        // Chỉ xử lý khi menu đang MỞ
        if (!navList.classList.contains("active")) {
            return;
        }

        // Kiểm tra click có nằm NGOÀI menu và toggle không
        const clickedInside = navList.contains(e.target) || menuToggle.contains(e.target);

        if (!clickedInside) {
            navList.classList.remove("active");
            menuToggle.setAttribute("aria-expanded", "false");
        } else {
        }
    });

    // ===== ĐÓNG KHI RESIZE =====
    window.addEventListener('resize', () => {
        if (window.innerWidth > 991 && navList.classList.contains("active")) {
            navList.classList.remove('active');
            menuToggle.setAttribute("aria-expanded", "false");
        }
    });

    // ===== PREVENT SCROLL WHEN MENU OPEN (BONUS) =====
    const observer = new MutationObserver(() => {
        if (navList.classList.contains("active")) {
            document.body.style.overflow = "hidden";
        } else {
            document.body.style.overflow = "";
        }
    });

    observer.observe(navList, {
        attributes: true,
        attributeFilter: ['class']
    });

    // ==========================================
    // ADMIN SIDEBAR TOGGLE
    // ==========================================
    const adminToggle = document.getElementById("menuToggle");
    const adminSidebar = document.getElementById("sidebar");

    if (adminToggle && adminSidebar) {

        adminToggle.addEventListener("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            adminSidebar.classList.toggle("sidebar-open");
        });

        // Đóng admin sidebar khi click outside (chỉ trên mobile)
        document.addEventListener("click", (e) => {
            if (window.innerWidth <= 991 && adminSidebar.classList.contains("sidebar-open")) {
                const clickedInside = adminSidebar.contains(e.target) || adminToggle.contains(e.target);
                if (!clickedInside) {
                    adminSidebar.classList.remove("sidebar-open");
                }
            }
        });
    }
});