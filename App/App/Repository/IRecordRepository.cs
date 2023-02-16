using App.Models;
using System.Linq.Expressions;

namespace App.Repository
{
    public interface IRecordRepository
    {
        public IEnumerable<Record> GetAllNewByPeriod(int month, int year);
        public IEnumerable<Record> GetNewForUser(User user);
        public IEnumerable<Record> GetClosedByUser(User user);
        IEnumerable<Record> FindBy(Expression<Func<Record, bool>> predicate);
        public Record? GetSingle(int id);
        public Record? GetSingle(Expression<Func<Record, bool>> predicate);
        public Record? GetSingle(Expression<Func<Record, bool>> predicate, params Expression<Func<Record, object>>[] includeProperties);

        public void Add(Record record);
        public void Commit();
    }
}
