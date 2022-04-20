using System;
using System.Collections.Generic;
using System.Text;

namespace SellerApiOzon.Services
{
    interface IDbContext
    {
        public string Dal_GetSkuFromOzon();
        public string Dal_GetRemainsForPrices();
        public void Dal_SaveSkuFromOzon(string skuList);
        public string Dal_UpdateStockForOzon();


    }
}
