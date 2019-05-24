using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    public class User
    {
        private bool _isUserCreated = false;

        private Guid _uid;
        public Guid Uid
        {
            get { return _uid; }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _firstname;

        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        private string _passsword;

        public string Password
        {
            get { return _passsword; }
            set { _passsword = value; }
        }

        private User(Guid uid, string username, string firstname, string password)
        {
            _uid = uid;
            _username = username;
            _firstname = firstname;
            _passsword = password;
        }

        public User()
        {
            _isUserCreated = true;
        }

        public static User Login(string username, string password)
        {
            User.Criteria criteria = new User.Criteria()
            {
                Username = username
            };

            List<User> users = new Repository().FindBy(criteria);

            if (users.Count < 1)
            {
                return null;
            }

            User user = users[0];

            if (user.Password == password)
            {
                return user;
            }
            
            return null;
        }

        public class Repository
        {
            private Connection _connection = new Connection();

            private bool InternalSave(User user)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "usp_AppUser_Add";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@username", SqlDbType.NVarChar, 50);
                command.Parameters["@username"].Value = user._username;

                command.Parameters.Add("@firstname", SqlDbType.NVarChar, 50);
                command.Parameters["@firstname"].Value = user._firstname;

                command.Parameters.Add("@password", SqlDbType.NVarChar, 50);
                command.Parameters["@password"].Value = user._passsword;

                return _connection.ExecuteCommand(command);
            }

            private bool InternalUpdate(User user)
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "usp_AppUser_Update";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@uid", SqlDbType.UniqueIdentifier);
                command.Parameters["@uid"].Value = user._uid;

                command.Parameters.Add("@username", SqlDbType.NVarChar, 50);
                command.Parameters["@username"].Value = user._username;

                command.Parameters.Add("@firstname", SqlDbType.NVarChar, 50);
                command.Parameters["@firstname"].Value = user._firstname;

                command.Parameters.Add("@password", SqlDbType.NVarChar, 50);
                command.Parameters["@password"].Value = user._passsword;

                return _connection.ExecuteCommand(command);
            }

            public bool Save(User user)
            {
                bool result = false;

                try
                {
                    if (user._isUserCreated)
                        result = InternalSave(user);
                    else if (!user._isUserCreated && !(user._uid == null))
                        result = InternalUpdate(user);
                    else
                        throw new DatabaseException("Invalid User object");

                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error saving User object", e);
                }

                return result;
            }

            public User Find(Guid uid)
            {
                User user = null;
                DataTable table = null;
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_AppUser_Find";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@uid", SqlDbType.UniqueIdentifier);
                    command.Parameters["@uid"].Value = uid;

                    table = _connection.ExecuteQuery(command);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error populating table Use", e);
                }

                foreach (DataRow row in table.Rows)
                {
                    string username = row["username"].ToString();
                    string firstname = row["firstname"].ToString();
                    string password = row["password"].ToString();
                    user = new User(uid, username, firstname, password);
                }

                return user;
            }

            public List<User> FindAll()
            {
                List<User> users = null;
                DataTable table = null;
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_AppUser_Search";
                    command.CommandType = CommandType.StoredProcedure;

                    table = _connection.ExecuteQuery(command);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error populating table Use", e);
                }

                users = new List<User>(table.Rows.Count);

                foreach (DataRow row in table.Rows)
                {
                    Guid uid = new Guid(row["uid"].ToString());
                    string username = row["username"].ToString();
                    string firstname = row["firstname"].ToString();
                    string password = row["password"].ToString();
                    User user = new User(uid, username, firstname, password);
                    users.Add(user);
                }

                return users;
            }

            public List<User> FindBy(Criteria criteria)
            {
                List<User> users = null;
                DataTable table = null;
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_AppUser_Search";
                    command.CommandType = CommandType.StoredProcedure;

                    InjectParameters(command, criteria);

                    table = _connection.ExecuteQuery(command);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error populating table Use", e);
                }

                users = new List<User>(table.Rows.Count);

                foreach (DataRow row in table.Rows)
                {
                    Guid uid = new Guid(row["uid"].ToString());
                    string username = row["username"].ToString();
                    string firstname = row["firstname"].ToString();
                    string password = row["password"].ToString();
                    User user = new User(uid, username, firstname, password);
                    users.Add(user);
                }

                return users;
            }

            private void InjectParameters(SqlCommand command, Criteria criteria)
            {
                if (!string.IsNullOrWhiteSpace(criteria.Username))
                {
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 50);
                    command.Parameters["@username"].Value = criteria.Username;
                }
                if (!string.IsNullOrWhiteSpace(criteria.Firstname))
                {
                    command.Parameters.Add("@firstname", SqlDbType.NVarChar, 50);
                    command.Parameters["@firstname"].Value = criteria.Username;
                }
            }
        }
        public class Criteria
        {
            public string Username { get; set; }
            public string Firstname { get; set; }
        }
    }
}