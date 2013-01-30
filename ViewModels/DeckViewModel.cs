using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using SQLite;
using Flashcard.Models;

namespace Flashcard.ViewModels
{
    public class DeckViewModel : ViewModelBase
    {
        #region Properties

        private int _id = 0;
        public int Id
        {
            get 
            { 
                return this._id; 
            }
            set 
            {
                if (_id == value)
                { return; }

                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        private int _categoryId = 0;
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                if (_categoryId == value)
                { return; }

                _categoryId = value;
                RaisePropertyChanged("CategoryId");
            }
        }

        private String _title = String.Empty;
        public String Title
        {
            get 
            { 
                return this._title; 
            }
            set 
            {
                if (_title == value)
                { return; }

                _title = value;
                isDirty = true;
                RaisePropertyChanged("Title");
            }
        }

        private String _author = String.Empty;
        public String Author
        {
            get 
            {
                return this._author; 
            }
            set 
            {
                if (_author == value)
                { return; }

                _author = value;
                isDirty = true;
                RaisePropertyChanged("Author");
            }
        }

        private String _subject = String.Empty;
        public String Subject
        {
            get 
            {
                return _subject; 
            }
            set 
            {
                if (_subject == value)
                { return; }

                _subject = value;
                isDirty = true;
                RaisePropertyChanged("Subject");
            }
        }

        private String _description = String.Empty;
        public String Description
        {
            get 
            {
                return _description; 
            }
            set 
            {
                if (_description == value)
                { return; }

                _description = value;
                isDirty = true;
                RaisePropertyChanged("Description");
            }
        }

        private bool isDirty = false;
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }


            set
            {
                isDirty = value;
                RaisePropertyChanged("IsDirty");
            }
        }

        #endregion Properties

        public async Task<DeckViewModel> GetDeck(int deckId)
        {
            var deck = new DeckViewModel();
            var db = new SQLiteAsyncConnection(App.DBPath);

            var _deck = await db.FindAsync<Deck>(d1 => d1.Id == deckId);
            if (_deck == null)
                return null;

            deck.Id = _deck.Id;
            deck.Title = _deck.Title;
            deck.Author = _deck.Author;
            deck.Subject = _deck.Subject;
            deck.Description = _deck.Description;
            deck.CategoryId = _deck.CategoryId;

            return deck;
        }

        public async Task<string> GetDeckTitle(int deckId)
        {
            var title = String.Empty;

            var db = new SQLiteAsyncConnection(App.DBPath);
            var _deck = await db.FindAsync<Deck>(d1 => d1.Id == deckId);

            if (_deck != null)
            {
                title = _deck.Title;
            }
            return title;
        }

        public async Task<string> GetDeckDescription(int deckId)
        {
            var description = String.Empty;

            var db = new SQLiteAsyncConnection(App.DBPath);
            var _deck = await db.FindAsync<Deck>(d1 => d1.Id == deckId);

            if (_deck != null)
            {
                description = _deck.Description;
            }
            return description;
        }

        public async Task<string> SaveDeck(DeckViewModel deck)
        {
            string result = String.Empty;
            var db = new SQLiteAsyncConnection(App.DBPath);

            try
            {
                var existingDeck = await (db.Table<Deck>().Where(d1 => d1.Id == deck.Id)).FirstOrDefaultAsync();

                if (existingDeck != null)
                {
                    existingDeck.Title = deck.Title;
                    existingDeck.Author = deck.Author;
                    existingDeck.Description = deck.Description;
                    existingDeck.Subject = deck.Subject;

                    int success = await db.UpdateAsync(existingDeck);
                }
                else
                {
                    var _deck = new Deck();
                    _deck.Title = deck.Title;
                    _deck.Author = deck.Author;
                    _deck.Description = deck.Description;
                    _deck.Subject = deck.Subject;

                    int success = await db.InsertAsync(_deck);
                }
                result = "Success";
            }
            catch
            {
                result = "this deck was not saved";
            }
            return result;
        }

        public async Task<string> DeleteDeck(int deckId)
        {
            string result = string.Empty;
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);

            var existingDeck = await (db.Table<Deck>().Where(
                d => d.Id == deckId)).FirstAsync();

            try
            {
                var _cards = await db.Table<Card>().Where(c1 => c1.DeckId == existingDeck.Id).ToListAsync();
                foreach (var _card in _cards)
                {
                    int s = await db.DeleteAsync(_card);
                }
                int succcess = await db.DeleteAsync(existingDeck);
                result = "Success";
            }
            catch
            {
                result = "This project was not removed";
            }
            return result;
        }
    }
}
