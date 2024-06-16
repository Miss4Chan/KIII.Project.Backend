using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domain_models
{
    public class Favourites : BaseEntity
    {
        [ForeignKey("ShopApplicationUserId")]
        public int UserId { get; set; }
        public virtual List<ProductInFavourites> ProductsInFavourites { get; set; }
    }
}
