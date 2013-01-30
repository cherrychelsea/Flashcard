using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flashcard.DataModel
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, Unique]
        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}
