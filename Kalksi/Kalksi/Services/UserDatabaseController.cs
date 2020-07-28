﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Kalksi.Models;
using SQLite;

namespace Kalksi.Services
{
    public class UserDatabaseController
    {
        static object locker = new object();
        SQLiteConnection db;
        public UserDatabaseController()
        {
            db = DependencyService.Get<ISQlite>().GetConnection();
            db.CreateTable<User>();
        }

        public User GetUser()
        {
            lock (locker)
            {
                if (db.Table<User>().Count() == 0) { return null; }
                else { return db.Table<User>().First(); }
            }
        }

        public int SaveUser(User u)
        {
            lock (locker)
            {
                if (u.Id != 0)
                {
                    db.Update(u);
                    return u.Id;
                }
                else { return db.Insert(u); }
            }
        }

        public int DeleteUser(int id)
        {
            lock (locker) { return db.Delete<User>(id); }
        }
    }
}
