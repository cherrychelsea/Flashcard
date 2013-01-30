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
    }
}
