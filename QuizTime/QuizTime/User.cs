using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuizTime
{
    [Table("tblUsers")]
    class User
    { 
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public User()
        {

        }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public static bool Exist(string userName)
        {
            string strSql = string.Format("SELECT * FROM tblUsers Where ( UserName = '{0}' )  ", userName);
            List<User> userList = DataBase.Connection.Query<User>(strSql);
            if (userList.Count >= 1)
            {
                return true;
            }
            return false;
        }
        public static bool Exist(string userName, string password)
        {
            string strSql = string.Format("SELECT * FROM tblUsers Where ( UserName = '{0}' and Password = '{1}' )  ", userName, password);
            List<User> userList = DataBase.Connection.Query<User>(strSql);
            if (userList.Count >= 1)
            {
                return true;
            }
            return false;
        }
        public static int GetId(string userName, string password)
        {
            string strSql = string.Format("SELECT * FROM tblUsers Where ( UserName = '{0}' and Password = '{1}' )  ", userName, password);
            List<User> userList = DataBase.Connection.Query<User>(strSql);
            if (userList.Count == 0)
            {
                return -1;
            }
            return userList[0].UserId;
        }
        public static string GetUserName(int id)
        {
            string strSql = string.Format("SELECT * FROM tblUsers Where ( UserId = {0} )  ", id);
            List<User> userList = DataBase.Connection.Query<User>(strSql);
            if (userList.Count == 0)
            {
                return "-1";
            }
            return userList[0].UserName;
        }
        public static bool Save(string userName, string password)
        {
            User user = new User(userName, password);
            try
            {
                DataBase.Connection.Insert(user);
                List<User> usersList = DataBase.Connection.Table<User>().ToList();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}