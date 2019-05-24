using CodorniX.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodorniX.VistaDelModelo
{
    public class Login
    {
        private User.Repository userRepository = new User.Repository();
        public User User { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool LoginUser()
        {
            User = User.Login(Username, Password);

            if (User != null)
            {
                Firstname = User.Firstname;
                return true;
            }

            ErrorMessage = "Invalid User/Password";
            return false;
        }
    }
}