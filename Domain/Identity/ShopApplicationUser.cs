using Domain.Domain_models;

namespace Domain.Identity
{
    public class ShopApplicationUser : BaseEntity
    {
        public string Name{ get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public virtual ShoppingCart UserShoppingCart { get; set; }
        public virtual Favourites UserFavourites { get; set; }
        public virtual Rented UserRented { get; set; }
        public int UserRatingCount { get; set; }
        public double UserRating { get; set; }
        public int UserRatingTotal { get; set; }
    }
}
