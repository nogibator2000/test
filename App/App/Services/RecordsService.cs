using App.InputModels;
using App.Models;
using App.Repository;
using App.ResponseModels;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace App.Services
{
    public class RecordsService: IRecordsService
    {
        private IUserRepository _userRepository;
        private IRecordRepository _recordRepository;
        private ICodeRepository _codeRepository;
        public RecordsService(IUserRepository userRepository, IRecordRepository recordRepository, ICodeRepository codeRepository) 
        {
            _userRepository = userRepository;
            _recordRepository = recordRepository;
            _codeRepository = codeRepository;
        }
        public List<RecordValuesResponse> GenerateRecordValues(int month, int year, string email)
        {
            Cheker.CheckDate(month, year);
            var res = new List<RecordValuesResponse>();
            var query = _recordRepository.FindBy(a=>a.Month==month&& a.Year == year&&a.User!=null&&a.User.Email==email);
            foreach (var record in query)
            {
                res.Add(new RecordValuesResponse
                {
                    Month = record.Month,
                    Year = record.Year,
                    Status = record.Status,
                    Id = record.Id,
                    EndAddress = record.EndAddress,
                    EndDate = record.EndDate,
                    Price = record.Price,
                    StartAddress = record.StartAddress,
                    StartDate = record.StartDate,
                    Tax = record.Tax,
                    UserEmail = record.User.Email,
                    UserName = record.User.Name

                });
            }
            return res;
        }
        public List<RecordResponse> GenerateAllRecordsByUser(string email)
        {
            var model = _userRepository.GetByEmail(email);
            var res = new List<RecordResponse>();
            var query = _recordRepository.GetNewForUser(model).ToList().GroupBy(n => new { n.Month, n.Year }).Select(grp => grp.Last()).ToList();
            query.AddRange(_recordRepository.GetClosedByUser(model).ToList().GroupBy(n => new { n.Month, n.Year }).Select(grp => grp.Last()).ToList());
            foreach (var record in query)
            {
                res.Add(new RecordResponse
                {
                    Month = record.Month,
                    Year = record.Year,
                    Status = record.Status,
                    UserEmail = model.Email,
                    UserName = model.Name
                });
            }
            return res;
        }
        public List<RecordResponse> GenerateAllNewRecords(int month, int year)
        {
            Cheker.CheckDate(month, year);
            var res = new List<RecordResponse>();
           
            var query = _recordRepository.GetAllNewByPeriod(month, year).ToList().GroupBy(n => new { n.Month, n.Year, n.User}).Select(grp => grp.Last()).ToList(); ;
            foreach (var record in query)
            {
                if (record.User == null)
                {
                    throw new Exception("ошибка пользователя");
                }
                res.Add(new RecordResponse
                {
                    Month = record.Month,
                    Year = record.Year,
                    Status = record.Status,
                    UserEmail = record.User.Email,
                    UserName = record.User.Name
                });
            }
            return res;
        }
        public List<PriceResponse> GeneratePrice(int month, int year)
        {
            Cheker.CheckDate(month, year);
            var res = _recordRepository.FindBy(a => a.User != null && a.Month == month && a.Year == year &&
            (a.Status == Record.STATUSPRIVATE || a.Status == Record.STATUSCLOSEDPRIVATE))
                .ToList().GroupBy(a => a.User).Select(a => new PriceResponse
                {
                    FullPrice = a.Sum(b => b.Price + b.Tax),
                    Code = _codeRepository.GetAnyCode(),
                    Month = a.FirstOrDefault().Month,
                    Year = a.FirstOrDefault().Year,
                    UserName = a.FirstOrDefault().User.Name
                }).ToList();
            return res;
        }
        public void ChangeStatus(string email, int id)
        {
            var record = _recordRepository.GetSingle(u => u.Id == id);
            if (record == null)
            {
                throw new ArgumentException("запись не найдена");
            } else if (record.User == null)
            {
                throw new ArgumentException("запись повреждена");
                            }
            else if ( record.User.Email != email)
            {
                throw new ArgumentException("емаил не совпадает");
                            }
            if (record.Status == Record.STATUSPRIVATE)
            {
                if (DateTime.Now.AddHours(-5) < record.ChangeDate)
                {
                    record.Status = Record.STATUSNEW;
                    _recordRepository.Commit();
                }
                else
                {
                    throw new ArgumentException("время прошло");
                }
            }
            else if (record.Status == Record.STATUSNEW)
            {

                if (record.ChangeDate == null)
                {
                    record.Status = Record.STATUSPRIVATE;
                    record.ChangeDate = DateTime.Now;
                    _recordRepository.Commit();
                }
                else if (DateTime.Now.AddHours(-5) < record.ChangeDate)
                {
                    record.Status = Record.STATUSPRIVATE;
                    _recordRepository.Commit();
                }
                else
                {
                    throw new ArgumentException("время прошло");
                }

            }
        }
        public void Close(string email, int month, int year)
        {
            Cheker.CheckDate(month, year);
            var query = _recordRepository.GetAllNewByPeriod(month, year);
                foreach (var item in query)
            {
                if (item.Status == Record.STATUSPRIVATE)
                {
                    item.Status = Record.STATUSCLOSEDPRIVATE;
                    _recordRepository.Commit();
                }
                else if (item.Status == Record.STATUSNEW)
                {
                    item.Status = Record.STATUSCLOSED;
                    _recordRepository.Commit();
                }
            }
        }


    }
}
