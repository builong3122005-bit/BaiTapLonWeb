<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    ClientIDMode="Static" Inherits="btl.Page.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập - Shop ABC</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet" />

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
                <a href="/Page/Index.aspx" class="back-to-home" aria-label="Quay về trang chủ">
                    <img src="/assets/img/icon-exit.png" alt="Quay lại" class="icon-img" />
                </a>

                <h1>Đăng nhập</h1>

                <asp:Label ID="errorMessage" runat="server" ForeColor="Red" />

                <div class="form-group">
                    <label for="email">Email</label>
                    <input type="email" id="email" runat="server" required />
                    <span class="field-error"></span>
                </div>

                <div class="form-group">
                    <label for="password">Mật khẩu</label>
                    <input type="password" id="password" runat="server" required />
                    <span class="field-error"></span>
                </div>

                <button type="submit" class="btn" id="btn_login" runat="server">Đăng nhập</button>

                <p class="form-footer">
                    Chưa có tài khoản? <a href="/Page/SignUp.aspx">Đăng ký ngay</a>
                </p>
            </div>
        </div>
    </form>

    <script type="module" src="/assets/js/login.js"></script>
    <script src="~/assets/js/admin.js"></script>

</body>
</html>
