using PagedList;
using System;
using System.Collections.Generic;

namespace WebRequestRateLimit.Models
{
    public class StudentViewModel
    {
        public bool All { get; set; }
        public bool OnlyBoys { get; set; }
        public bool OnlyGirls { get; set; }
        public IPagedList<GetStudentDataModel> GetList { get; set; }
    }
}