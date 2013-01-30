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
    class DecksViewModel:ViewModelBase
    {
        private ObservableCollection<DeckViewModel> _decks;
        public ObservableCollection<DeckViewModel> Decks
        {
            get 
            {
                return _decks;
            }
            set
            {
                _decks = value;
                RaisePropertyChanged("Decks");
            }
        }

        public async Task<ObservableCollection<DeckViewModel>> GetDecks(int categoryId)
        {
            _decks = new ObservableCollection<DeckViewModel>();
            var db = new SQLiteAsyncConnection(App.DBPath);

            var query = await db.Table<Deck>().OrderBy(d => d.Title).ToListAsync();
            foreach (var d in query)
            {
                var _deck = new DeckViewModel();
                _deck.Id = d.Id;
                _deck.Title = d.Title;
                _deck.Author = d.Author;
                _deck.Subject = d.Subject;
                _deck.Description = d.Description;
                _deck.CategoryId = d.CategoryId;

                _decks.Add(_deck);
            }
            return _decks;
        }
    }
}
