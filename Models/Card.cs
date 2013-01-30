using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Flashcard.DataModel
{
    [Table("Cards")]
    public class Card
    {
        [PrimaryKey, Unique]
        public Guid Id { get; set; }
        public String FrontContent { get; set; }
        public String BackContent { get; set; }
        public Guid DeckId { get; set; }
    }
}
