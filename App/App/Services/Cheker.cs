namespace App.Services
{
    public static class Cheker
    {
        public static void CheckDate(int month, int year)
        {
            if ((month < 1 || month > 12) && (year < 1970 || year > 2050))
            {
                throw new Exception("неправильный формат введенной даты");
            }

        }
        public static void CheckFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("файл пустой");
            }

        }
    }
}
