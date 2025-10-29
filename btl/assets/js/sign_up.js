// /assets/js/sign_up.js
// File này được nhúng bằng <script type="module" src="/assets/js/sign_up.js"></script>

const $ = document.querySelector.bind(document);

// Các phần tử (đã có ClientIDMode="Static")
const form = $("#form1");
const nameInput = $("#name");
const emailInput = $("#email");
const passwordInput = $("#password");
const confirmInput = $("#confirm_password");
const globalError = $("#errorMessage"); // label runat="server" (render thành <span>)

// --- Helpers hiển thị lỗi ---

// Tạo phần tử lỗi 
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
    input.setAttribute("aria-invalid", true);
    const spanLoi = taoPhanTuLoi(input);
    if (spanLoi) spanLoi.textContent = message || "";
}
function xoaLoi(input) {
    if (!input) return;
    input.classList.remove("invalid");
    input.removeAttribute("aria-invalid");
    const nhom = input.closest(".form-group");
    const spanLoi = nhom && nhom.querySelector(".field-error");
    if (spanLoi) spanLoi.textContent = ""
}

// Biểu thức kiểm tra email cơ bản phía client
const bieuThucEmail = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;

// kiểm tra tên 
function kiemTraHoTen() {
    const giaTri = (nameInput?.value || "").trim();
    if (giaTri.length < 2) {
        ganLoi(nameInput, "Họ tên phải có ít nhất 2 ký tự ");
        return false;

    }
    xoaLoi(nameInput); // hợp lệ thì xóa lỗi ;
    return true;
}

// Kiemtra email

function kiemTraEmail() {
    const giaTri = (emailInput?.value || "").trim();
    if (!bieuThucEmail.test(giaTri)) {
        ganLoi(emailInput, "Email không hợp lệ (ví dụ: ten@domain.com)");
        return false;
    }
    xoaLoi(emailInput);
    return true;
}

// kiem tra mật khẩu

function kiemTraMatKhau() {
    const giaTri = passwordInput?.value || '';
    const duDoDai = giaTri.length >= 6;
    const coChu = /[A-Za-z]/.test(giaTri);
    const coSo = /\d/.test(giaTri);
    if (!duDoDai || !coChu || !coSo) {
        ganLoi(passwordInput, "Mật khẩu ≥ 6 ký tự, gồm cả chữ và số");
        return false;
    }
    xoaLoi(passwordInput);
    return true;
}
/* kiemTraXacNhanMatKhau():
 Giá trị confirm phải trùng mật khẩu
*/
function kiemTraXacNhanMatKhau() {
    const mk = passwordInput?.value || "";                // Lấy mật khẩu
    const xnm = confirmInput?.value || "";                 // Lấy xác nhận
    if (xnm !== mk) {                                      // So sánh không khớp
        ganLoi(confirmInput, "Mật khẩu nhập lại không khớp");
        return false;                                        // Báo lỗi
    }
    xoaLoi(confirmInput);                                  // Hợp lệ -> xóa lỗi
    return true;                                           // Trả về hợp lệ
}

/* ================== GẮN SỰ KIỆN REALTIME ================== */
// Khi rời ô họ tên -> kiểm tra họ tên
nameInput?.addEventListener("blur", kiemTraHoTen);

// Khi rời ô email -> kiểm tra email
emailInput?.addEventListener("blur", kiemTraEmail);

// Khi gõ ở ô mật khẩu -> kiểm tra mật khẩu liên tục
passwordInput.addEventListener("input", () => {
    kiemTraMatKhau();
    if (confirmInput?.value) {
        kiemTraXacNhanMatKhau();
    }
});
// Khi gõ ở ô xác nhận -> kiểm tra khớp ngay
confirmInput?.addEventListener("input", kiemTraXacNhanMatKhau);

form?.addEventListener("submit", (e) => {
    if (globalError) globalError.innerText = "";

    // Gọi lần lượt các hàm kiểm tra
    const hopLeHoTen = kiemTraHoTen();
    const hopLeEmail = kiemTraEmail();
    const hopLeMatKhau = kiemTraMatKhau();
    const hopLeXacNhan = kiemTraXacNhanMatKhau();

    // Tổng hợp kết quả
    const tatCaHopLe = hopLeHoTen && hopLeEmail && hopLeMatKhau && hopLeXacNhan;

    if (!tatCaHopLe) {
        e.preventDefault();
        if (globalError) {
            globalError.innerText = "Vui lòng kiểm tra lại các trường bị lỗi.";
        }
        const dauTienLoi = form.querySelector(".invalid");
        dauTienLoi?.focus();
    }
})