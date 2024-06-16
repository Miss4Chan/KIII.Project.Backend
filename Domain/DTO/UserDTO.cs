using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int RatingCount { get; set; }
        public double Rating { get; set; }
        public virtual List<ProductDTO> Products { get; set; }
        public virtual List<CommentDTO> Comments { get; set; }

    }
}
