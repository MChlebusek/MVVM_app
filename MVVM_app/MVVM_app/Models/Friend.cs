using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM_app.Models
{
    [Table("Friends")]
    public class Friend
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public byte[] Photo { get; set; }

        public int Age { get; set; }






    }
}
