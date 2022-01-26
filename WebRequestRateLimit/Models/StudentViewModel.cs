using PagedList;
using System;
using System.Collections.Generic;

namespace WebRequestRateLimit.Models
{
    public class StudentViewModel
    {
        public IPagedList<GetStudentDataModel> GetList { get; set; }
    }
}