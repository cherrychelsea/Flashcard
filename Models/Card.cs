using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flashcard.Models
{
    [Table("Cards")]
    public class Card
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        public String FrontContent { get; set; }
        public String BackContent { get; set; }
        public int DeckId { get; set; }
    }
}
