using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flashcard.Models
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey,AutoIncrement, Unique]
        public int Id { get; set; }
        public String Name { get; set; }
    }
}
