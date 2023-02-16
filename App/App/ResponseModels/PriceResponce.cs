
namespace App.ResponseModels
{
    public class PriceResponse
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string? UserName { get; set; }
        public int Code { get; set; }
        public float FullPrice { get; set; }
    }
}