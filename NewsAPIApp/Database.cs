using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPIApp
{
    public class Database
    {
        private string url = "localhost";
        private string bdd = "newsAPI";
        private string user = "UserAPI";
        private string password = "UserPassword";

        public static MySqlConnection getConnection()
        {
            var conn = new Database();
            MySqlConnection connexion = new MySqlConnection("Server=" + conn.url + ";Database=" + conn.bdd + ";userid=" + conn.user + ";password=" + conn.password + ";");
            return connexion;
        }
    }
}
