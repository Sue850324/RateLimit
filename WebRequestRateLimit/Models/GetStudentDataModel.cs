using System;

namespace WebRequestRateLimit.Models
{
    public class GetStudentDataModel
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int? Grade { get; set; }
        public bool? Sextual { get; set; }
        public string Locale { get; set; }
        public string Phone { get; set; }
        public string PhoneDisplay
        {
            get
            {
                string result = Phone == null ? "-" : Phone;
                return result;
            }
        }
        public string SextualDisplay
        {
            get
            {
               string result = Sextual == true ? "Girl" : "Boy";
               return result;
            }
        }       
    }
}