using App.Models;
using App.Repository;
using App.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace App.Repository
{
    public class RecordRepository : EntityRepository<Record>, IRecordRepository
    {
        public RecordRepository(MyDBContext context) : base(context) { }

        public IEnumerable<Record> GetAllNewByPeriod(int month, int year)
        {
            IEnumerable<Record> records = FindBy(u => u.Month == month && u.Year == year && u.Status == Record.STATUSNEW);
            return records;
        }
        public IEnumerable<Record> GetNewForUser(User user)
        {
            var records = FindBy(u => u.Status == Record.STATUSNEW && u.User == user);
            return records;
        }
        public IEnumerable<Record> GetClosedByUser(User user)
        {
            var records = FindBy(u => u.User == user&&
            (u.Status == Record.STATUSCLOSED|| u.Status == Record.STATUSCLOSEDPRIVATE));
            return records;
        }
        public void CloseRecord(int id)
        {
            var record = GetSingle(u => u.Id == id);
            if (record != null)
            {
                record.Status = Record.STATUSCLOSED;
            }
            else
                throw new ArgumentException("запись не найдена");
        }
    }
}
