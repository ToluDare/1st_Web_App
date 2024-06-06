using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_App.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StockQuantity { get; set; }
        [DisplayName("Category Stock Quantity")]
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public virtual Category? Categorys { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
