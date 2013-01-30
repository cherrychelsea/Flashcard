using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Flashcard.Models;

namespace Flashcard.ViewModels
{
    class CardsViewModel:ViewModelBase
    {
        private ObservableCollection<CardViewModel> _cards;
        public ObservableCollection<CardViewModel> Card
        {
            get
            {
                return _cards;
            }
            set
            {
                _cards = value;
                RaisePropertyChanged("Cards");
            }   
        }

        public async Task<ObservableCollection<CardViewModel>> GetCards(int deckId)
        {
            _cards = new ObservableCollection<CardViewModel>();
            var db = new SQLiteAsyncConnection(App.DBPath);

            var query = await db.Table<Card>().OrderBy(c => c.FrontContent).ToListAsync();

            foreach (var c in query)
            {
                var _card = new CardViewModel();
                _card.Id = c.Id;
                _card.FrontContent = c.FrontContent;
                _card.BackContent = c.BackContent;
                _card.DeckId = c.DeckId;
                _cards.Add(_card);
            }
            return _cards;
        }
    }
}
