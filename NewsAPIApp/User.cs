using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPIApp
{
    public class User
    {
        public int id { get; set; }
        public string pseudo { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public User(int id, string pseudo, string firstName, string lastName) 
        {
            this.id = id;
            this.pseudo = pseudo;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public User(string pseudo, string password, string firstName, string lastName)
        {
            this.pseudo = pseudo;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public User(string pseudo, string password)
        {
            this.pseudo = pseudo;
            this.password = password;
        }
    }
}
