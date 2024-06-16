using Domain.Domain_models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IRentedService
    {
        List<ProductInRented> getRentedInfo(string userId);
    }
}
