using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        [MaxLength(255, ErrorMessage = "Title must be at most 255 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        [MaxLength(255, ErrorMessage = "Title must be at most 255 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.01, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        [MaxLength(255, ErrorMessage = "Title must be at most 255 characters")]
        public string Industry { get; set; } = string.Empty;
        
        [Range(0, 1000000)]
        public long MarketCap { get; set; }
    }
}