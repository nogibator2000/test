
namespace App.InputModels
{
    public class InputRecord
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? StartAddress { get; set; }

        public string? EndAddress { get; set; }

        public float Price { get; set; }
        public float Tax { get; set; }
    }
}