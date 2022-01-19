using WebRequestRateLimit.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace WebRequestRateLimit.Repository
{
    public class StudentRepository
    {
        public List<GetStudentDataModel> GetListAll()
        {
            string strConnString = ConfigurationManager.ConnectionStrings["StudentDatabase"].ConnectionString;
            List<GetStudentDataModel> result = new List<GetStudentDataModel>();

            using (SqlConnection conn = new SqlConnection(strConnString))
             {
                string strSql = @"Select * From [dbo].[Table] ORDER BY Grade";
                result = conn.Query<GetStudentDataModel>(strSql).ToList();           
            }
    
            return result;
        }

        public List<GetStudentDataModel> GetBoys()
        {
            string strConnString = ConfigurationManager.ConnectionStrings["StudentDatabase"].ConnectionString;
            List<GetStudentDataModel> result = new List<GetStudentDataModel>();

            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                string strSql = @"SELECT * FROM [dbo].[Table] WHERE Sextual = 0 ORDER BY Grade";
                result = conn.Query<GetStudentDataModel>(strSql).ToList();
            }

            return result;
        }

        public List<GetStudentDataModel> GetGirls()
        {
            string strConnString = ConfigurationManager.ConnectionStrings["StudentDatabase"].ConnectionString;
            List<GetStudentDataModel> result = new List<GetStudentDataModel>();

            using (SqlConnection conn = new SqlConnection(strConnString))
            {
                string strSql = @"SELECT * FROM [dbo].[Table] WHERE Sextual = 1 ORDER BY Grade";
                result = conn.Query<GetStudentDataModel>(strSql).ToList();
            }

            return result;
        }
    }
}