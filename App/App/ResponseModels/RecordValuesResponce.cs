
using App.Models;

namespace App.ResponseModels
{
    public class RecordValuesResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? StartAddress { get; set; }

        public string? EndAddress { get; set; }

        public float? Price { get; set; }
        public float? Tax { get; set; }
        public int Status { get; set; }
    }
}