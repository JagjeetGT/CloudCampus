using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Data.Repositories;

namespace KRBAccounting.Web.Helpers
{
    public class StudentHelper
    {
        private IScStudentinfoRepository _scStudentinfoRepository;

        public StudentHelper(IScStudentinfoRepository studentinfoRepository)
        {
            _scStudentinfoRepository = studentinfoRepository;
        }

        //For generating the student id
        public string GenerateStudentCode(string studentName)
        {
            string studentCode = string.Empty;
            string startingAlphabet = studentName.Substring(0, 1);
            startingAlphabet = startingAlphabet.ToUpper();
            int count = _scStudentinfoRepository.GetAll().Count() + 1;
            studentCode = startingAlphabet + "0000" + count;
            return studentCode;
        }

        public string GenerateStudentRegistrationNum()
        {
            string currentDate = DateTime.UtcNow.Year.ToString();
            int count = _scStudentinfoRepository.GetAll().Count() + 1;
            string studentRegistrationNo = string.Concat(currentDate, count.ToString());
            return studentRegistrationNo;
        }
    }
}