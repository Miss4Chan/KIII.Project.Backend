using Domain.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain_models
{
    public class ProductInShoppingCart : BaseEntity
    {
        public int ProductId{ get; set; }
        public int ShoppingCartId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ShoppingCartId")]
        public virtual ShoppingCart ShoppingCart { get; set; }

    }
}
