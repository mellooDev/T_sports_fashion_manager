namespace DataModel
{
    public class SubCategoriesModel
    {
        public int subCategory_id { get; set; }

        public string subCategory_name { get; set; }

        public int categoryMain_id { get; set; }

        public List<ProductsModel> list_json_product_by_subcate { get; set; }

    }
}