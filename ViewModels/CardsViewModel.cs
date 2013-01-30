using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcard;
namespace Flashcard.ViewModels
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public class CardsViewModel : Flashcard.Common.BindableBase
    {
        public CardsViewModel(String uniqueid, String frontContent, String backContent, DecksViewModel deck)
        {
            this._uniqueid = uniqueid;
            this._frontContent = frontContent;
            this._backContent = backContent;
            this._deck = deck;
        }

        private String _uniqueid = String.Empty;
        public String UniqueId
        {
            get { return this._uniqueid; }
            set { this.SetProperty(ref this._uniqueid, value); }
        }

        private String _frontContent = String.Empty;
        public String FrontContent
        {
            get { return this._frontContent; }
            set { this.SetProperty(ref this._frontContent, value); }
        }

        private String _backContent = String.Empty;
        public String BackContent
        {
            get { return this._backContent; }
            set { this.SetProperty(ref this._backContent, value); }
        }

        private DecksViewModel _deck = null;
        public DecksViewModel Deck
        {
            get { return this._deck; }
            set { this._deck = value; }
        }
    }
}
