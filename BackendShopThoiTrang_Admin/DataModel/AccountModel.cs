namespace DataModel
{
    public class AccountModel
    {
        public int account_id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public int role_id { get; set; }

        public string token { get; set;}

        public List<AccountDetailsModel> list_json_account_details { get; set; }

    }
}