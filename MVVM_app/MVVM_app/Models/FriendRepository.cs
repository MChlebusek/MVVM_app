using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MVVM_app.Models
{
    public class FriendRepository
    {
        SQLiteConnection database;
        public FriendRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Friend>();
        }
        public IEnumerable<Friend> GetItems()
        {
            return database.Table<Friend>().ToList();

        }
        public int DeleteItem(int id)
        {
            return database.Delete<Friend>(id);
        }
        public Friend GetItem(int id)
        {
            return database.Get<Friend>(id);
        }
        public int SaveItem(Friend item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);

            }

        }
        public IEnumerable<Friend> SearchByName(string name)
        {
            return database.Table<Friend>().Where(friend => friend.Name.Contains(name)).ToList();
        }

    }
}
