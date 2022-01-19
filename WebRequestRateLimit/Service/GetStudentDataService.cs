using WebRequestRateLimit.Repository;
using System;
using System.Collections.Generic;
using WebRequestRateLimit.Models;

namespace WebRequestRateLimit.Service
{
    public class GetStudentDataService
    {
        private static StudentRepository _repository = new StudentRepository();

        public List<GetStudentDataModel> GetAllStudent()
        {
            lock (CacheHelper.GetCacheObject("AllStudent"))
            {
                if ((CacheHelper._cache.Get("AllStudent") as List<GetStudentDataModel>) == null)
                {
                    var model = RefreshModelData("AllStudent");
                    CacheHelper.RefreshCache("AllStudent", model);
                }
            }

            return CacheHelper._cache.Get("AllStudent") as List<GetStudentDataModel>;
        }

        public List<GetStudentDataModel> BoysStudent()
        {
            lock (CacheHelper.GetCacheObject("BoysStudent"))
            {
                if ((CacheHelper._cache.Get("BoysStudent") as List<GetStudentDataModel>) == null)
                {
                    var model = RefreshModelData("BoysStudent");
                    CacheHelper.RefreshCache("BoysStudent", model);
                }
            }

            return CacheHelper._cache.Get("BoysStudent") as List<GetStudentDataModel>;
        }

        public List<GetStudentDataModel> GirlsStudent()
        {
            lock (CacheHelper.GetCacheObject("GirlsStudent"))
            {
                if ((CacheHelper._cache.Get("GirlsStudent") as List<GetStudentDataModel>) == null)
                {
                    var model = RefreshModelData("GirlsStudent");
                    CacheHelper.RefreshCache("GirlsStudent", model);
                }
            }

            return CacheHelper._cache.Get("GirlsStudent") as List<GetStudentDataModel>;
        }

        protected List<GetStudentDataModel> RefreshModelData(string cacheName)
        {
            List<GetStudentDataModel> model = new List<GetStudentDataModel>();

            if (cacheName == "AllStudent")
            {
                model = _repository.GetListAll();
            }
            else if (cacheName == "GirlsStudent")
            {
                model = _repository.GetGirls();
            }
            else
            {
                model = _repository.GetBoys();
            }

            foreach (var item in model)
            {
                item.CacheID = Guid.NewGuid();
                item.CacheName = $"{Guid.NewGuid()}";
            }

            return model;
        }
    }
}