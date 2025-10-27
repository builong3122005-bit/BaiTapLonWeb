/*
 * =========================================
 * ADMIN PANEL JAVASCRIPT
 * =========================================
 */

// Chạy code khi tài liệu (DOM) đã tải xong
document.addEventListener("DOMContentLoaded", function () {

    // --- 1. HỆ THỐNG CHUYỂN PAGE (TAB) ---

    const navLinks = document.querySelectorAll(".nav-link");
    const pages = document.querySelectorAll(".page-content");

    /**
     * Ẩn tất cả các trang và hiển thị trang được chỉ định.
     */
    window.showPage = function (pageName) {
        pages.forEach((page) => {
            page.classList.add("hidden");
        });

        const targetPage = document.getElementById(pageName + "-page");
        if (targetPage) {
            targetPage.classList.remove("hidden");
        } else {
            document.getElementById("dashboard-page").classList.remove("hidden");
        }

        navLinks.forEach((link) => {
            link.classList.remove("active");
            if (link.dataset.page === pageName) {
                link.classList.add("active");
            }
        });

        localStorage.setItem("adminLastPage", pageName);
    };

    navLinks.forEach((link) => {
        link.addEventListener("click", function (e) {
            e.preventDefault();
            const pageName = this.dataset.page;

            if (pageName && pageName !== "logout-btn") {
                window.showPage(pageName);
            } else if (pageName === "logout-btn") {
                // Xử lý đăng xuất: Xóa cookie "User" và chuyển về trang Login
                document.cookie = "User=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                window.location.href = 'Login.aspx';
            }
        });
    });

    // --- 2. KHỞI TẠO PAGE KHI TẢI LẠI TRANG ---
    const lastPage = localStorage.getItem("adminLastPage");
    const urlParams = new URLSearchParams(window.location.search);

    if (!urlParams.has('action')) {
        if (lastPage) {
            window.showPage(lastPage);
        } else {
            window.showPage("dashboard");
        }
    }


    // --- 3. BẬT/TẮT SIDEBAR TRÊN DI ĐỘNG ---
    const menuToggle = document.getElementById("menuToggle");
    const sidebar = document.getElementById("sidebar");

    if (menuToggle && sidebar) {
        menuToggle.addEventListener("click", function () {
            sidebar.classList.toggle("sidebar-open");
        });
    }
});


// --- 4. HÀM ĐIỀU KHIỂN MODAL (Global) ---

/**
 * Mở một modal dựa theo ID
 */
window.openModal = function (modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
        modal.style.display = "block";
    }
};

/**
 * Đóng một modal dựa theo ID
 */
window.closeModal = function (modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
        modal.style.display = "none";
    }

    // Khi đóng modal, reset form tương ứng
    if (modalId === 'productModal') {
        window.resetProductForm();
    } else if (modalId === 'categoryModal') {
        window.resetCategoryForm();
    } else if (modalId === 'customerModal') {
        window.resetCustomerForm();
    }
};

/**
 * Tìm control ASP.NET 
 */
const findControl = (id) => document.querySelector(`[id$="_${id}"]`);

// --- 4a. Hàm reset Form Sản phẩm ---
window.resetProductForm = function () {
    const hdnId = findControl("hdnProductId");
    const txtName = findControl("txtName");
    const ddlCategory = findControl("ddlCategory");
    const txtPrice = findControl("txtPrice");
    const txtStock = findControl("txtStock");
    const fileUpload = findControl("fileUploadImage");

    if (hdnId) hdnId.value = "";
    if (txtName) txtName.value = "";
    if (ddlCategory) ddlCategory.selectedIndex = 0;
    if (txtPrice) txtPrice.value = "";
    if (txtStock) txtStock.value = "";
    if (fileUpload) fileUpload.value = null;
};

// --- 4b. Hàm reset Form Danh mục (MỚI) ---
window.resetCategoryForm = function () {
    const hdnId = findControl("hdnCategoryId");
    const txtName = findControl("txtCategoryName");
    const ddlPage = findControl("ddlCategoryPageData");

    if (hdnId) hdnId.value = "";
    if (txtName) txtName.value = "";
    if (ddlPage) ddlPage.selectedIndex = 0;
};

// --- 4c. Hàm reset Form Khách hàng  ---
window.resetCustomerForm = function () {
    const hdnId = findControl("hdnCustomerId");
    const txtName = findControl("txtCustomerName");
    const txtEmail = findControl("txtCustomerEmail");
    const txtPass = findControl("txtCustomerPassword");
    const ddlRole = findControl("ddlCustomerRole");

    if (hdnId) hdnId.value = "";
    if (txtName) txtName.value = "";
    if (txtEmail) txtEmail.value = "";
    if (txtPass) txtPass.value = "";
    if (ddlRole) ddlRole.value = "USER";
};

/**
 * Các hàm "showAdd...Modal" để reset form và đổi tiêu đề
 */
window.showAddProductModal = function () {
    window.resetProductForm();
    const modalTitle = document.querySelector("#productModal .modal-title");
    if (modalTitle) modalTitle.textContent = "Thêm sản phẩm mới";
    window.openModal('productModal');
};

window.showAddCategoryModal = function () {
    window.resetCategoryForm();
    const modalTitle = document.querySelector("#categoryModal .modal-title");
    if (modalTitle) modalTitle.textContent = "Thêm danh mục mới";
    window.openModal('categoryModal');
};

window.showAddCustomerModal = function () {
    window.resetCustomerForm();
    const modalTitle = document.querySelector("#customerModal .modal-title");
    if (modalTitle) modalTitle.textContent = "Thêm khách hàng mới";
    window.openModal('customerModal');
};

/**
 * Hàm điều hướng cho các thẻ card trên dashboard
 */
window.navigateToPage = function (pageName) {
    window.showPage(pageName);
}