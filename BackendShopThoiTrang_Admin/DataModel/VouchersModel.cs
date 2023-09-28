namespace DataModel
{
    public class VouchersModel
    {
        public int voucher_id { get; set; }

        public string voucher_name { get; set; }

        public DateTime time_start { get; set; }

        public DateTime time_end { get; set; }

        public int quantity { get; set; }

        public int voucher_value { get; set; }

        public string voucher_code { get; set; }

        public bool status { get; set; }

    }
}