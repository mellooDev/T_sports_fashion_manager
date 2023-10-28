namespace DataModel
{
    public class ImportModel
    {
        public int import_id { get; set; }

        public int category_id { get; set; }

        public DateTime? import_date { get; set; }

        public int total_money { get; set; }

        public List<Import_detailsModel> list_json_import_details { get; set; }

    }

    public class Import_detailsModel
    {
        public int detail_id { get; set; }

        public int import_id { get; set; }

        public int brand_id { get; set; }

        public int product_id { get; set; }

        public int quantity { get; set; }

        public int import_price { get; set; }

        public int total_money { get; set; }

    }
}