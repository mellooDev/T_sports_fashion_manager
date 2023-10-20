namespace DataModel
{
    public class Order_invoicesModel
    {
        public int order_id { get; set; }

        public DateTime? created_date { get; set; }

        public int account_id { get; set; }

        public int shippingDetail_id { get; set; }

        public int total_order_money { get; set; }

        public DateTime order_date { get; set; }

        public int status { get; set; }

        public List<Order_detailsModel> list_json_order_details { get; set; }

    }

    public class Order_detailsModel
    {
        public int detail_id { get; set; }

        public int order_id { get; set; }

        public int product_id { get; set; }

        public int voucher_id { get; set; }

        public int total_details_money { get; set; }

    }
}