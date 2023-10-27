namespace DataModel
{
    public class CategoriesModel
    {
        public int category_id { get; set; }

        public string category_name { get; set; }

        public List<ProductsModel> list_json_product_by_cate { get; set; }

    }
}