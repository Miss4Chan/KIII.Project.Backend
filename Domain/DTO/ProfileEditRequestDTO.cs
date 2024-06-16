using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class ProfileEditRequestDTO
    {
        public UserDTO UserDTO { get; set; }
        public string Password { get; set; }
    }
}
