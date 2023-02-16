using App.Models;

namespace App.Repository
{
    public interface ICodeRepository
    {
        public int GetAnyCode(); 
        public void Add(Code code);
        public void Commit();

    }
}
