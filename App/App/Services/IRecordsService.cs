using App.InputModels;
using App.Models;
using App.Repository;
using App.ResponseModels;

namespace App.Services
{
    public interface IRecordsService
    {
        public List<RecordValuesResponse> GenerateRecordValues(int month, int year, string email);
        public List<RecordResponse> GenerateAllRecordsByUser(string email);
        public List<RecordResponse> GenerateAllNewRecords(int month, int year);
        public List<PriceResponse> GeneratePrice(int month, int year);
        public void ChangeStatus(string email, int id);
        public void Close(string email, int month, int year);


    }
}
