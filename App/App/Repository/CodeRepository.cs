using App.Models;
using App.Repository;
using App.Context;

namespace App.Repository
{
    public class CodeRepository : EntityRepository<Code>, ICodeRepository
    {
        public CodeRepository(MyDBContext context) : base(context) { }

        public int GetAnyCode()
        {
            var code = GetSingle(a=>true);
            if (code != null&&code.Value != null)
                return (int)code.Value;
            else
                throw new Exception("код не найден");
        }
    }
}
