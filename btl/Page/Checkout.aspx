<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="btl.Page.Checkout" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thanh toán - Shop ABC</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/forms.css" />
    <link rel="stylesheet" href="~/assets/css/checkout.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <main>
            <div class="page-header">
                <h1>Thanh toán</h1>
            </div>
            <div class="container py-5">
                <asp:Panel ID="pnlCheckoutError" runat="server" Visible="false" CssClass="alert alert-danger">
                    <%-- Thông báo lỗi (ví dụ giỏ hàng trống) --%>
                </asp:Panel>

                <asp:Panel ID="pnlCheckoutContent" runat="server">
                    <div class="layout-60-40">
                        <div class="checkout-section">
                            <div class="checkout-form">
                                <h2 class="form-section-title">Thông tin giao hàng</h2>
                                <div class="form-row">
                                    <div class="form-col">
                                        <div class="form-group">
                                            <label for="txtFullName">Họ và Tên*</label>
                                            <asp:TextBox ID="txtFullName" runat="server" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                                                ErrorMessage="Vui lòng nhập họ tên" CssClass="error-message" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtAddress">Địa chỉ*</label>
                                    <asp:TextBox ID="txtAddress" runat="server" required="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                        ErrorMessage="Vui lòng nhập địa chỉ" CssClass="error-message" Display="Dynamic">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtPhone">Số điện thoại*</label>
                                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" required="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                                        ErrorMessage="Vui lòng nhập số điện thoại" CssClass="error-message" Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone"
                                        ValidationExpression="^\d{10,11}$" ErrorMessage="Số điện thoại không hợp lệ" CssClass="error-message" Display="Dynamic">*</asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtEmail">Email*</label>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" required="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Vui lòng nhập Email" CssClass="error-message" Display="Dynamic">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Email không hợp lệ" CssClass="error-message" Display="Dynamic">*</asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtNotes">Ghi chú đơn hàng (tùy chọn)</label>
                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="4" placeholder="Ghi chú về đơn hàng, ví dụ: thời gian hay chỉ dẫn địa điểm giao hàng chi tiết."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="summary-section">
                            <div class="order-summary">
                                <h2 class="form-section-title">Đơn hàng của bạn</h2>
                                <div class="order-items">
                                    <%-- Hiển thị các item trong đơn hàng --%>
                                    <asp:Literal ID="litOrderItems" runat="server"></asp:Literal>
                                </div>
                                <div class="summary-line">
                                    <span>Tạm tính</span>
                                    <asp:Label ID="lblSubtotal" runat="server">0đ</asp:Label>
                                </div>
                                <div class="summary-line">
                                    <span>Phí giao hàng</span>
                                    <asp:Label ID="lblShipping" runat="server">Miễn phí</asp:Label>
                                </div>
                                <div class="summary-line total">
                                    <span>Tổng cộng</span>
                                    <asp:Label ID="lblTotal" runat="server">0đ</asp:Label>
                                </div>

                                <div class="payment-methods">
                                    <h3 class="payment-title">Phương thức thanh toán</h3>
                                    <%-- Dùng RadioButtonList cho phương thức thanh toán --%>
                                    <asp:RadioButtonList ID="rblPaymentMethod" runat="server" RepeatLayout="Flow" CssClass="payment-options-list">
                                        <asp:ListItem Value="COD" Selected="True">
                                            <img src="../assets/img/money.png" alt="COD" class="payment-icon-img" />
                                            Trả tiền mặt khi nhận hàng (COD)
                                        </asp:ListItem>
                                        <asp:ListItem Value="VNPAY"> <%-- Hoặc Momo, ZaloPay --%>
                                            <img src='https://upload.wikimedia.org/wikipedia/vi/f/fe/MoMo_Logo.png' alt="Online" class="payment-icon-img" />
                                            Thanh toán Online (VNPAY, Momo...) <%-- Tạm thời gộp chung --%>
                                        </asp:ListItem>
                                        <%-- Thêm các phương thức khác nếu cần --%>
                                    </asp:RadioButtonList>
                                </div>
                                <asp:Button ID="btnPlaceOrder" runat="server" Text="Đặt hàng" CssClass="btn btn-order" OnClick="btnPlaceOrder_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error-summary" HeaderText="Vui lòng kiểm tra lại thông tin:" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </main>
        <uc:Footer runat="server" ID="footer1" />
    </form>
    <script src="~/assets/js/admin.js"></script>

</body>
</html>
