using Domain.Enums;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Domain_models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ShopApplicationUser User { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostalCode { get; set; }
        public float Subtotal { get; set; }
        public float Total { get; set; }
        public string FormattedDate { get; set; }
        public string FormattedTime { get; set; }
        public virtual ICollection<ProductInOrder> ProductsInOrder { get; set; }
    }
}
