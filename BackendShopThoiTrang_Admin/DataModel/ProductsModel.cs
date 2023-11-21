namespace DataModel
{
    public class ProductsModel
    {
        public int product_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int price { get; set; }

        public int discount { get; set; }

        public string image_link { get; set; }

        public int product_quantity { get; set; }

        public DateTime created_date { get; set; }

        public DateTime updated_date { get; set; }

        public int subCategory_id { get; set; }

        public int brand_id { get; set; }

    }
}