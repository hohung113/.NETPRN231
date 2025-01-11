using BusinessObject.Enitty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Entity;

public partial class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Weight { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int UnitInStock { get; set; }
    //[JsonIgnore]
    public virtual Category Category { get; set; }  
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
