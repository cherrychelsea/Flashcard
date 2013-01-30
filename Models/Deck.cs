using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flashcard.Models
{
    [Table("Decks")]
    public class Deck
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public String Subject { get; set; }
        public String Description { get; set; }
        public int CategoryId { get; set; }
    }
}
