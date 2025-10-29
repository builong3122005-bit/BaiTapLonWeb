<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="btl.Page.OrderDetail" %>

<%@ Register Src="~/UserControl/Header.ascx" TagPrefix="uc" TagName="Header" %>
<%@ Register Src="~/UserControl/Footer.ascx" TagPrefix="uc" TagName="Footer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Chi tiết Đơn hàng - Shop ABC</title>
    <link rel="stylesheet" href="~/assets/css/reset.css" />
    <link rel="stylesheet" href="~/assets/css/grid.css" />
    <link rel="stylesheet" href="~/assets/css/common.css" />
    <link rel="stylesheet" href="~/assets/css/home.css" />
    <link rel="stylesheet" href="~/assets/css/products.css" />
    <link rel="stylesheet" href="~/assets/css/icons.css" />
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <style>
        .container123 {
            max-width: 900px;
            margin: 30px auto;
            padding: 25px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }

        .page-header {
            text-align: center;
            margin-bottom: 30px;
            border-bottom: 1px solid #eee;
            padding-bottom: 15px;
        }

            .page-header h1 {
                font-size: 1.8em;
                color: #333;
                margin: 0;
            }

        .order-summary, .order-items-section {
            margin-bottom: 30px;
            padding-bottom: 20px;
            border-bottom: 1px solid #eee;
        }

            .order-summary h2, .order-items-section h2 {
                font-size: 1.4em;
                color: #555;
                margin-bottom: 15px;
                border-bottom: 1px dashed #ddd;
                padding-bottom: 8px;
            }

        .summary-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 15px;
            margin-bottom: 15px;
        }

        .summary-item label {
            font-weight: 600;
            display: block;
            color: #666;
            font-size: 0.9em;
            margin-bottom: 3px;
        }

        .summary-item span {
            font-size: 1em;
        }

        .order-status {
            padding: 3px 8px;
            border-radius: 4px;
            color: white;
            font-size: 0.9em;
            font-weight: 500;
            text-transform: capitalize;
        }

            .order-status.pending {
                background-color: #ffc107;
                color: #333;
            }

            .order-status.processing {
                background-color: #17a2b8;
            }

            .order-status.shipped {
                background-color: #007bff;
            }

            .order-status.completed {
                background-color: #28a745;
            }

            .order-status.cancelled {
                background-color: #dc3545;
            }

        .order-items-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }

            .order-items-table th, .order-items-table td {
                text-align: left;
                padding: 10px 8px;
                border-bottom: 1px solid #eee;
            }

            .order-items-table th {
                background-color: #f8f9fa;
                font-weight: 600;
                font-size: 0.9em;
                color: #555;
            }

            .order-items-table td.item-image img {
                max-width: 50px;
                height: auto;
                vertical-align: middle;
                margin-right: 10px;
                border-radius: 4px;
            }

            .order-items-table td.item-name {
                font-weight: 500;
            }

            .order-items-table td.item-meta {
                font-size: 0.85em;
                color: #777;
            }

            .order-items-table td.item-price, .order-items-table td.item-total {
                text-align: right;
            }

        .order-total-summary {
            text-align: right;
            margin-top: 20px;
        }

            .order-total-summary div {
                margin-bottom: 8px;
                font-size: 1.1em;
            }

            .order-total-summary span {
                display: inline-block;
                min-width: 120px;
                text-align: left;
            }

            .order-total-summary .total span {
                font-weight: 600;
                color: #dc3545;
            }

        .action-buttons {
            margin-top: 30px;
            text-align: center;
        }

        .btn {
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            border: none;
            cursor: pointer;
            font-family: 'Montserrat', sans-serif;
            font-size: 1em;
            transition: background-color 0.3s ease;
        }

            .btn:hover {
                background-color: #0056b3;
            }

        .btn-secondary {
            background-color: #6c757d;
        }

            .btn-secondary:hover {
                background-color: #5a6268;
            }

        .alert-danger {
            color: #721c24;
            background-color: #f8d7da;
            border: 1px solid #f5c6cb;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc:Header runat="server" ID="header1" />
        <main>
            <asp:Panel ID="pnlOrderDetail" runat="server">
                <div class="container123">
                    <div class="page-header">
                        <h1>Chi tiết Đơn hàng
                            <asp:Literal ID="litOrderIdHeader" runat="server"></asp:Literal></h1>
                    </div>

                    <asp:Literal ID="litErrorMessage" runat="server" Visible="false"></asp:Literal>

                    <asp:Panel ID="pnlContent" runat="server">
                        <div class="order-summary">
                            <h2>Thông tin đơn hàng</h2>
                            <div class="summary-grid">
                                <div class="summary-item">
                                    <label>Mã đơn hàng:</label>
                                    <span>
                                        <asp:Literal ID="litOrderId" runat="server"></asp:Literal></span>
                                </div>
                                <div class="summary-item">
                                    <label>Ngày đặt:</label>
                                    <span>
                                        <asp:Literal ID="litOrderDate" runat="server"></asp:Literal></span>
                                </div>
                                <div class="summary-item">
                                    <label>Trạng thái:</label>
                                    <span>
                                        <asp:Literal ID="litOrderStatus" runat="server"></asp:Literal></span>
                                </div>
                                <div class="summary-item">
                                    <label>Tên khách hàng:</label>
                                    <span>
                                        <asp:Literal ID="litCustomerName" runat="server"></asp:Literal></span>
                                </div>
                            </div>
                        </div>


                        <div class="order-items-section">
                            <h2>Chi tiết sản phẩm</h2>
                            <table class="order-items-table">
                                <thead>
                                    <tr>
                                        <th colspan="2">Sản phẩm</th>
                                        <th>Giá</th>
                                        <th>Số lượng</th>
                                        <th style="text-align: right">Thành tiền</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptOrderItems" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="item-image">
                                                    <img src='<%# ResolveUrl( Eval("ImageUrl") != null ? Eval("ImageUrl").ToString() : "~/Uploads/Products/default.jpg" ) %>' alt='<%# Eval("ProductName") %>' />
                                                </td>
                                                <td>
                                                    <div class="item-name"><%# Eval("ProductName") %></div>
                                                    <div class="item-meta">Size: <%# Eval("Size") %></div>
                                                </td>
                                                <td class="item-price"><%# Eval("Price", "{0:N0}đ") %></td>
                                                <td><%# Eval("Quantity") %></td>
                                                <td class="item-total"><%# Eval("TotalPrice", "{0:N0}đ") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Literal ID="litOrderItemsFallback" runat="server" Visible="false"></asp:Literal>
                                </tbody>
                            </table>
                        </div>

                        <div class="order-total-summary">
                            <div>
                                <span>Tạm tính:</span>
                                <asp:Literal ID="litSubtotal" runat="server">0đ</asp:Literal>
                            </div>
                            <div>
                                <span>Phí vận chuyển:</span>
                                <asp:Literal ID="litShippingFee" runat="server">0đ</asp:Literal>
                            </div>
                            <div class="total">
                                <span>Tổng cộng:</span>
                                <asp:Literal ID="litTotal" runat="server">0đ</asp:Literal>
                            </div>
                        </div>

                        <div class="action-buttons">
                            <asp:HyperLink NavigateUrl="~/Page/Products.aspx" Text="Tiếp tục mua sắm" CssClass="btn btn-secondary" runat="server" />
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </main>
        <uc:Footer runat="server" ID="footer1" />
    </form>
    <script src="~/assets/js/admin.js"></script>

</body>
</html>
