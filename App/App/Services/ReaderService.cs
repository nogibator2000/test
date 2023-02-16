using App.InputModels;
using App.Models;
using App.Repository;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace App.Services
{
    public class ReaderService : IReaderService
    {
        private IUserRepository _userRepository;
        private IRecordRepository _recordRepository; 
        public ReaderService(IUserRepository userRepository, IRecordRepository recordRepository) 
        {
            _userRepository = userRepository;
            _recordRepository = recordRepository;
        }
        public void LoadAdmins(IFormFile file)
        {
            Cheker.CheckFile(file);
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var user = _userRepository.GetByEmail(line);
                    if (user == null)
                    {
                        _userRepository.AddUser(line, User.ADMIN, string.Empty);
                        _userRepository.Commit();
                    } else if (user.Rights == User.USER)
                    {
                        user.Rights = User.ADMIN;
                        _userRepository.Commit();
                    }
                }

            }
        }
        public void LoadExcel(IFormFile file, int month, int year)
        {
            Cheker.CheckDate(month, year);
            Cheker.CheckFile(file);
            var wb = new XLWorkbook(file.OpenReadStream());

            var ws2 = wb.Worksheet(1);


            foreach (var r in ws2.RangeUsed().RowsUsed())
            {
                if (r.RowNumber() == 1)
                {
                    continue;
                }
                var inputRecord = new InputRecord
                {
                    Name = r.Cell(1).GetString(),
                    Email = r.Cell(2).GetString(),
                    StartDate = DateTime.Parse(r.Cell(3).GetString(), CultureInfo.GetCultureInfo("ru-RU")),
                    StartAddress = r.Cell(4).GetString(),
                    EndDate = DateTime.Parse(r.Cell(3).GetString(), CultureInfo.GetCultureInfo("ru-RU")),
                    EndAddress = r.Cell(6).GetString(),
                    Price = float.Parse(r.Cell(7).GetString()),
                    Tax = float.Parse(r.Cell(8).GetString()),
                };
                var user = _userRepository.GetByEmail(inputRecord.Email);
                if (user == null)
                {
                    _userRepository.AddUser(inputRecord.Email, User.USER, inputRecord.Name);
                    _userRepository.Commit();
                    user = _userRepository.GetByEmail(inputRecord.Email);
                } 
                if (user.Name == string.Empty)
                {
                    user.Name = inputRecord.Name;
                    _userRepository.Commit();
                }
                else if (user.Name != inputRecord.Name)
                {
                    throw new Exception(inputRecord.Name + " не совпадает с базой");

                }
                _recordRepository.Add(new Record
                {
                    User = user,
                    Status = Record.STATUSNEW,
                    StartAddress = inputRecord.StartAddress,
                    EndAddress = inputRecord.EndAddress,
                    EndDate = inputRecord.EndDate,
                    StartDate = inputRecord.StartDate,
                    Price = inputRecord.Price,
                    Tax = inputRecord.Tax,
                    Month = month,
                    Year = year
                });
                _recordRepository.Commit();

            }

        }

    }
}
