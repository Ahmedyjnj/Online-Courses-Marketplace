using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.PaymentDto
{
    public class PaymobCallbackData
    {
        public string merchant_order_id { get; set; }
        public string amount_cents { get; set; }
        public string created_at { get; set; }
        public string currency { get; set; }
        public string error_occured { get; set; }
        public string has_parent_transaction { get; set; }
        public string id { get; set; }
        public string integration_id { get; set; }
        public string is_3d_secure { get; set; }
        public string is_auth { get; set; }
        public string is_capture { get; set; }
        public string is_refunded { get; set; }
        public string is_standalone_payment { get; set; }
        public string is_voided { get; set; }
        public string owner { get; set; }
        public string pending { get; set; }
        public string success { get; set; }
        public Order order { get; set; }
        public SourceData source_data { get; set; }
    }

    public class Order
    {
        public string id { get; set; }
    }

    public class SourceData
    {
        public string pan { get; set; }
        public string sub_type { get; set; }
        public string type { get; set; }
    }
}
