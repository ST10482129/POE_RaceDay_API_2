namespace POE_RaceDay_API.DTOs
{
    public class CategoryRequest
    {
        public string CategoryName { get; set; } = string.Empty;
        public decimal Distance { get; set; }
        public decimal EntryFee { get; set; }
        public int EventID { get; set; }
    }
}
