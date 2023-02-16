using App.InputModels;
using App.Models;
using App.Repository;

namespace App.Services
{
    public interface IReaderService
    {
        public void LoadExcel(IFormFile file, int month, int year);
        public void LoadAdmins(IFormFile file);

    }
}
