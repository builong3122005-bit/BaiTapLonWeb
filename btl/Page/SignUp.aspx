<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" ClientIDMode="Static" Inherits="btl.Page.SignUp" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng ký - Shop ABC</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet" />
    <!-- Link từ root -->
    <link rel="stylesheet" href="/assets/css/reset.css" />
    <link rel="stylesheet" href="/assets/css/grid.css" />
    <link rel="stylesheet" href="/assets/css/common.css" />
    <link rel="stylesheet" href="/assets/css/forms.css" />
    <link rel="stylesheet" href="/assets/css/auth.css" />
    <link rel="stylesheet" href="/assets/css/icons.css" />
</head>
<body class="form-page">
    <form id="form1" runat="server" class="auth-page">
        <div class="auth-container">
            <div class="form-wrapper">
                <a
                    href="/Page/Index.aspx"
                    class="back-to-home"
                    aria-label="Quay về trang chủ">
                    <img src="/assets/img/icon-exit.png" alt="Quay lại" class="icon-img" />
                </a>

                <h1>Tạo tài khoản</h1>
                <label id="errorMessage" runat="server" style="color: red; padding: 10px 0;"></label>

                <div class="form-group">
                    <label for="name">Họ và tên</label>
                    <input type="text" id="name" required runat="server" />
                    <span class="field-error"></span>
                </div>

                <div class="form-group">
                    <label for="email">Email</label>
                    <input type="email" id="email" required runat="server" />
                    <span class="field-error"></span>
                </div>

                <div class="form-group">
                    <label for="password">Mật khẩu</label>
                    <input type="password" id="password" required runat="server" />
                    <span class="field-error"></span>
                </div>

                <div class="form-group">
                    <label for="confirm_password">Nhập lại mật khẩu</label>
                    <input type="password" id="confirm_password" required runat="server" />
                    <span class="field-error"></span>
                </div>

                <button type="submit" class="btn" id="btn_submit" runat="server">Đăng ký</button>

                <p class="form-footer">
                    Đã có tài khoản? <a href="/Page/Login.aspx">Đăng nhập</a>
                </p>
            </div>
        </div>
    </form>

    <!-- Script từ root -->
    <script src="../assets/js/main.js"></script>
    <script src="/assets/js/product-detail.js"></script>
    <script src="/assets/js/sign_up.js" type="module"></script>
</body>
</html>
