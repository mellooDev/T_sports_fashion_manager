namespace DataModel
{
    public class Shipping_detailsModel
    {
        public int shippingDetail_id { get; set; }

        public string consignee_name { get; set; }

        public string delivery_address { get; set; }

        public string phone_number { get; set; }

        public string shipping_note { get; set; }

        public int shipping_method { get; set; }

    }
}