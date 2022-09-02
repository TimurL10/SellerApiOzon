using System;
using System.Collections.Generic;
using System.Text;

namespace SellerApiOzon.Services
{
    public interface IApiConnector
    {
        public void GetSkuFromOzon();
        public void GetAttributes();
        public void UpdateStockForOzon();
        public void GetItemForOzon();
        public void UpdatePricesForOzon();

    }
}
