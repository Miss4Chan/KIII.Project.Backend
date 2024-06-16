using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domain_models
{
    public class ProductInFavourites : BaseEntity
    {
        public int ProductId { get; set; }
        public int FavouritesId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("FavouritesId")]
        public virtual Favourites Favourites { get; set; }

    }
}
