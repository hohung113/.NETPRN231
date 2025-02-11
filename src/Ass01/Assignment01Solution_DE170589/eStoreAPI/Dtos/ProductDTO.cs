﻿using BusinessObject.Enitty;

namespace eStoreAPI.Dtos
{
    public class ProductDTO
    {
        public string ProductName { get; set; } = null!;

        public string Weight { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int UnitInStock { get; set; }
        
        public int CategoryId { get; set; }
    }
}
