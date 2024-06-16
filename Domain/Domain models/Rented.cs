using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domain_models
{
    public class Rented : BaseEntity
    {

        [ForeignKey("ShopApplicationUserId")]
        public int UserId { get; set; }
        public virtual List<ProductInRented> ProductsInRented { get; set; }
    }
}
