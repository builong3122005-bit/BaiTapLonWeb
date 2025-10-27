using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btl.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; } // 1: Áo Thun, 2: Sơ Mi, 3: Áo Khoác, 4: Jeans, 5: Shorts
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } // Đường dẫn lưu ảnh
        public string Description { get; set; }
        public Product()
        {
        }
    }
}