using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain_models
{
    public class ShoppingCart : BaseEntity
    {
       [ForeignKey("ShopApplicationUserId")]
        public int UserId { get; set; }
        public virtual List<ProductInShoppingCart> ProductsInShoppingCart { get; set; }
    }
}
