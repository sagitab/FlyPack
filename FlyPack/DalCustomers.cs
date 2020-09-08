using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FlyPack
{
    public class DalCustomers
    {
        public static bool IsExsist(string pass, string user)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT * FROM Customers WHERE FirstName = '" + user + "'" + "AND Password = '" + pass + "'";
            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();
            if (tb.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
     
        public static int AddCustomer(string FirstName,string LastName, string pass, string phoneNum,  string email ,int NUmOfFloors,string Adress)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"INSERT INTO Customers (FirstName,LastName,[Password],PhoneNamber,Gmail,NUmOfFloors,Adress) VALUES('{FirstName}','{LastName}','{pass}','{phoneNum}','{email}','{NUmOfFloors}','{Adress}')";
            int a = helper.InsertWithAutoNumKey(sql);
            helper.CloseConnection();

            if (a == -1) throw new Exception("sql ex");

            return a;
        }
    }
}
