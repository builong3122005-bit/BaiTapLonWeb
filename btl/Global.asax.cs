using System;
using System.Collections.Generic;
using System.Linq;
using btl.Models;

namespace btl
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            List<User> users = new List<User>
            {
                new User { id = 1, fullname = "Admin User", email = "admin@gmail.com", password = "admin", role = "ADMIN" },
                new User { id = 2, fullname = "Khách Hàng A", email = "khachhangA@gmail.com", password = "123", role = "USER" }
            };
            Application["users"] = users;

            List<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Áo Thun & T-shirt", PageData = "products-tshirt" },
                new Category { Id = 2, Name = "Áo Sơ Mi", PageData = "products-somi" },
                new Category { Id = 3, Name = "Áo Khoác", PageData = "products-khoac" },
                new Category { Id = 4, Name = "Quần Jeans", PageData = "products-jeans" },
                new Category { Id = 5, Name = "Quần Shorts", PageData = "products-shorts" }
            };
            Application["categories"] = categories;

            List<Product> products = GenerateFakeProducts(30, categories);
            Application["products"] = products;


            List<Order> orders = new List<Order>
            {
                new Order { Id = 1001, UserId = 2, CustomerName = "Khách Hàng A", TotalAmount = products[random.Next(products.Count)].Price * random.Next(1, 3), Status = "Pending", OrderDate = DateTime.Now.AddDays(-random.Next(1, 5)) },
                new Order { Id = 1002, UserId = 2, CustomerName = "Khách Hàng A", TotalAmount = products[random.Next(products.Count)].Price * random.Next(1, 2), Status = "Shipped", OrderDate = DateTime.Now.AddDays(-random.Next(6, 10)) },
                 new Order { Id = 1003, UserId = 1, CustomerName = "Admin User", TotalAmount = products[random.Next(products.Count)].Price, Status = "Completed", OrderDate = DateTime.Now.AddDays(-random.Next(11, 20)) }
            };
            Application["orders"] = orders;


            List<Payment> payments = new List<Payment>();
            foreach (var order in orders)
            {
                payments.Add(new Payment
                {
                    TransactionId = $"PM{order.Id}",
                    OrderId = order.Id,
                    Amount = order.TotalAmount,
                    Method = (random.Next(0, 2) == 0) ? "COD" : "VNPAY",
                    Status = (order.Status == "Completed" || order.Status == "Shipped") ? "Success" : "Pending",
                    PaymentDate = order.OrderDate.AddHours(random.Next(1, 5))
                });
            }
            Application["payments"] = payments;
        }

        private static Random random = new Random();

        private List<Product> GenerateFakeProducts(int count, List<Category> categories)
        {
            var products = new List<Product>();

            string[] prefixes = { "Basic", "Premium", "Classic", "Modern", "Vintage", "Sporty", "Casual", "Streetwear", "Elegant", "Minimal" };
            string[] colors = { "Trắng", "Đen", "Xanh Navy", "Xám", "Beige", "Xanh Rêu", "Đỏ Đô", "Vàng Mustard", "Hồng Pastel", "Cam Đất", "Tím Than" };
            string[] materials = { "Cotton", "Kaki", "Jean", "Dù", "Nỉ", "Linen", "Kate", "Poly", "Lụa", "Len" };
            string[] patterns = { "Trơn", "Kẻ Sọc", "Kẻ Caro", "In Hình", "Wash", "Rách Gối", "Thêu Logo", "Hoa Văn", "Chấm Bi" };

            Dictionary<int, string> categoryNames = categories.ToDictionary(c => c.Id, c => c.Name.Split('&')[0].Trim());

            int currentMaxId = products.Any() ? products.Max(p => p.Id) : 0;

            for (int i = 1; i <= count; i++)
            {
                int id = currentMaxId + i;
                int categoryId = random.Next(1, categories.Count + 1);
                string categoryBaseName = categoryNames.ContainsKey(categoryId) ? categoryNames[categoryId] : "Sản phẩm";
                string prefix = prefixes[random.Next(prefixes.Length)];
                string color = colors[random.Next(colors.Length)];
                string material = materials[random.Next(materials.Length)];
                string pattern = patterns[random.Next(patterns.Length)];

                string name = $"{categoryBaseName} {prefix} {pattern} {material} {color}";
                if (pattern == "Trơn") name = $"{categoryBaseName} {prefix} {material} {color}";

                double price = 0;
                switch (categoryId)
                {
                    case 1: price = random.Next(10, 35) * 10000; break;
                    case 2: price = random.Next(25, 55) * 10000; break;
                    case 3: price = random.Next(40, 120) * 10000; break;
                    case 4: price = random.Next(35, 90) * 10000; break;
                    case 5: price = random.Next(20, 50) * 10000; break;
                    default: price = random.Next(15, 70) * 10000; break;
                }
                // Round price
                price = Math.Round(price / 10000) * 10000;


                int stock = random.Next(5, 101);
                string description = $"Mô tả chi tiết cho {name}. Sản phẩm làm từ chất liệu {material} cao cấp, màu {color}. {pattern}. Phù hợp cho nhiều dịp, mang lại cảm giác thoải mái khi mặc.";

                // Generate random image URL from 1.jpg to 16.jpg
                int imageNumber = random.Next(1, 17);
                string imageUrl = $"/Uploads/Products/{imageNumber}.jpg";


                products.Add(new Product
                {
                    Id = id,
                    Name = name,
                    CategoryId = categoryId,
                    Price = price,
                    Stock = stock,
                    ImageUrl = imageUrl,
                    Description = description
                });
            }

            return products;
        }
    }
}