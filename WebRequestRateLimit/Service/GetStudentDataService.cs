using WebRequestRateLimit.Repository;
using System;
using System.Collections.Generic;
using WebRequestRateLimit.Models;

namespace WebRequestRateLimit.Service
{
    public class GetStudentDataService
    {
        private static StudentRepository _repository = new StudentRepository();

        public List<GetStudentDataModel> GetData(bool? sexual)
        {
            StudentRepository studentRepository = new StudentRepository();

            if (sexual == null)
            {
                var data = CacheHelper.Get("AllStudent", () => { return studentRepository.GetListAll(); }, new TimeSpan(10));
                return data;
            }
            else if (sexual == true)
            {
                var data = CacheHelper.Get("FemaleStudent", () => { return studentRepository.GetGirls(); }, new TimeSpan(10));
                return data;
            }
            else
            {
                var data = CacheHelper.Get("MaleStudent", () => { return studentRepository.GetBoys(); }, new TimeSpan(10));
                return data;
            }
        }
    }
}