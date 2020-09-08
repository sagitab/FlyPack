using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FlyPack
{
   public class DalUser
    {
        public static DataTable IsExsist(string pass)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER,Constants.PATH);
            string sql = $"SELECT * FROM Ussers WHERE [Password]={pass}";
            DataTable t = null;
            try
            {
                 t = helper.GetDataTable(sql);
            }
            catch
            {
                throw new Exception("sql ex");
            }
            return t;
        }
        public static int AddUser(string email, string phone, string fname, string lname, string adress, string password, int type,int numOfFloor)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            string sql = $"INSERT INTO Users(Email,[Password],PhoneNum,FirstName,LastName,Adress,NumOfFloor,Type) VALUES ({email},{password},{phone},{fname},{lname},{adress},{numOfFloor},{type})";
            int id = -1;
            try
            {
                id = helper.WriteData(sql);
            }
            catch
            {
                throw new Exception("sql ex");
            }
            return id;
        }

    }
}
