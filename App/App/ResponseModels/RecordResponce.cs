
using App.Models;

namespace App.ResponseModels
{
    public class RecordResponse
    {
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Status { get; set; }
    }
}