namespace DataModel
{
    public class CategoryMainModel
    {
        public int categoryMain_id { get; set; }

        public string categoryMain_name { get; set; }

        public List<SubCategoriesModel> list_json_sub_category { get; set; }

    }
}