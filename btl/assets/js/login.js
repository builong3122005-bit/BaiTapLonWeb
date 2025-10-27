

const $ = document.querySelector.bind(document);

// Lấy phần tử (ClientIDMode="Static" nên id giữ nguyên)
const form = $("#form1");
const emailInput = $("#email");
const passwordInput = $("#password");
const globalError = $("#errorMessage");

// Tạo/tìm <span class="field-error"> trong .form-group
function taoPhanTuLoi(input) {
    const nhom = input?.closest(".form-group");
    if (!nhom) return null;
    let spanLoi = nhom.querySelector(".field-error");
    if (!spanLoi) {
        spanLoi = document.createElement("span");
        spanLoi.className = "field-error";
        nhom.appendChild(spanLoi);
    }
    return spanLoi;
}

function ganLoi(input, message) {
    if (!input) return;
    input.classList.add("invalid");
    input.setAttribute("aria-invalid", "true");
    const spanLoi = taoPhanTuLoi(input);
    if (spanLoi) spanLoi.textContent = message || "";
}

function xoaLoi(input) {
    if (!input) return;
    input.classList.remove("invalid");
    input.removeAttribute("aria-invalid");
    const nhom = input.closest(".form-group");
    const spanLoi = nhom && nhom.querySelector(".field-error");
    if (spanLoi) spanLoi.textContent = "";
}

// Quy tắc kiểm tra
const bieuThucEmail = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;

function kiemTraEmail() {
    const giaTri = (emailInput?.value || "").trim();
    if (!bieuThucEmail.test(giaTri)) {
        ganLoi(emailInput, "Email không hợp lệ (ví dụ: ten@domain.com)");
        return false;
    }
    xoaLoi(emailInput);
    return true;
}

function kiemTraMatKhau() {
    const giaTri = passwordInput?.value || "";
    // Với trang Login chỉ cần có mật khẩu (không rỗng).
    if (!giaTri) {
        ganLoi(passwordInput, "Vui lòng nhập mật khẩu");
        return false;
    }
    xoaLoi(passwordInput);
    return true;
}

// Realtime
emailInput?.addEventListener("blur", kiemTraEmail);
passwordInput?.addEventListener("input", kiemTraMatKhau);

// Submit
form?.addEventListener("submit", (e) => {
    if (globalError) globalError.innerText = "";

    const okEmail = kiemTraEmail();
    const okPass = kiemTraMatKhau();

    if (!okEmail || !okPass) {
        e.preventDefault();
        if (globalError) globalError.innerText = "Vui lòng kiểm tra lại các trường bị lỗi.";
        const dauTienLoi = form.querySelector(".invalid");
        dauTienLoi?.focus();
    }
});
