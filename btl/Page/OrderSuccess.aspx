<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSuccess.aspx.cs" Inherits="btl.Page.OrderSuccess" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Đặt hàng thành công - Shop ABC</title>
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Montserrat', sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 80vh; /* Giữ nội dung ở giữa trang */
            text-align: center;
        }

        .success-container {
            background-color: #ffffff;
            padding: 40px 50px;
            border-radius: 8px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            max-width: 500px;
            width: 90%;
        }

        .success-icon {
            font-size: 50px;
            color: #28a745; /* Màu xanh lá cây */
            margin-bottom: 20px;
        }

        h1 {
            color: #333;
            font-size: 1.8em;
            margin-bottom: 15px;
        }

        p {
            color: #555;
            font-size: 1.1em;
            line-height: 1.6;
            margin-bottom: 25px;
        }

        .order-id {
            font-weight: 600;
            color: #007bff; /* Màu xanh dương */
        }

        .action-links a {
            display: inline-block;
            margin: 10px;
            padding: 12px 25px;
            border-radius: 5px;
            text-decoration: none;
            font-weight: 500;
            transition: background-color 0.3s ease;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
            border: 1px solid #007bff;
        }

            .btn-primary:hover {
                background-color: #0056b3;
                border-color: #0056b3;
            }

        .btn-secondary {
            background-color: #6c757d;
            color: white;
            border: 1px solid #6c757d;
        }

            .btn-secondary:hover {
                background-color: #5a6268;
                border-color: #545b62;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="success-container">
            <div class="success-icon">
                &#10004; 
            </div>
            <h1>Đặt hàng thành công!</h1>
            <p>
                Cảm ơn bạn đã mua hàng tại Shop ABC. Đơn hàng của bạn
                <asp:Label ID="lblOrderId" runat="server" CssClass="order-id" Text="#XXXXXX"></asp:Label>
                đã được ghi nhận và đang chờ xử lý.
            </p>
            <div class="action-links">
                <asp:HyperLink ID="lnkContinueShopping" NavigateUrl="~/Page/Products.aspx" Text="Tiếp tục mua sắm" CssClass="btn-secondary" runat="server" />
            </div>
        </div>
    </form>
    <script src="~/assets/js/admin.js"></script>

</body>
</html>
