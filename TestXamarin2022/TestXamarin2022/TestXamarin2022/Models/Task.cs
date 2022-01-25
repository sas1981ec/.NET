using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestXamarin2022.Models
{
    [Table("Tareas")]
    public class Task
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
