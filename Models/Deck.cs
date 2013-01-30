using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flashcard.DataModel
{
    [Table("Decks")]
    public class Deck
    {
        [PrimaryKey, Unique]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Author { get; set; }
        public String Subject { get; set; }
        public String Description { get; set; }
        public String ImagePath { get; set; }
        public Guid CategoryId { get; set; }
    }
}
