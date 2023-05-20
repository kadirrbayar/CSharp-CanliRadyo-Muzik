using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzikUygulamasi
{
    class Connect
    {
        private static MySqlConnection sqlConnection;

        private Connect()
        {

        }
        public static MySqlConnection baglanti()
        {
            if(sqlConnection == null)
            {
                sqlConnection = new MySqlConnection("server=localhost;user=root;password=12345;database=43stationc#;port=3306;charset=utf8");
                sqlConnection.Open();
            }
            return sqlConnection;
        }
        public static void kapat()
        {
            if(sqlConnection != null)
            {
                sqlConnection.Close();
            }
        }
    }
}
