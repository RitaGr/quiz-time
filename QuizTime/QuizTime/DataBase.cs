using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using SQLite;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLiteConnection = SQLite.SQLiteConnection;

namespace QuizTime
{
    class DataBase
    {
        private static readonly string dbname = "userregDb";
        private static readonly SQLiteConnection connection = new SQLiteConnection(Path());

        private static string Path() // יוצרת את המסלול למסד נתונים
        {
            string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbname);//  מביא את התיקייה של המסד נתונים
            return path; // ניגש לתיקייה פנימית בטלפון ומחזיר את הנתונים משם
        }
        public static void CreateTables()
        {
            //if (!File.Exists(DataBase.Path()))
            //{
                DataBase.Connection.CreateTable<User>();
            //}           
        }
        public static SQLiteConnection Connection
        {
            get
            {
                return DataBase.connection;
            }
        }
    }
}