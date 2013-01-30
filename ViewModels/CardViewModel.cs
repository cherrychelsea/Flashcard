using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcard;
using SQLite;
using Flashcard.Models;

namespace Flashcard.ViewModels
{
    public class CardViewModel : ViewModelBase
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

        private int _deckId = 0;
        public int DeckId
        {
            get
            {
                return this._deckId;
            }
            set
            {
                if (_deckId == value)
                { return; }
                _deckId = value;
                RaisePropertyChanged("DeckId");
            }
        }

        private string _frontContent = String.Empty;
        public string FrontContent
        {
            get 
            { 
                return this._frontContent; 
            }
            set 
            {
                if (_frontContent == value)
                { return; }

                _frontContent = value;
                isDirty = true;
                RaisePropertyChanged("FrontContent");
            }
        }

        private string _backContent = String.Empty;
        public string BackContent
        {
            get 
            { 
                return this._backContent; 
            }
            set 
            {
                if (_backContent == value)
                { return; }

                _backContent = value;
                isDirty = true;
                RaisePropertyChanged("BackContent");
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

        public async Task<CardViewModel> GetCard(int cardId)
        {
            var card = new CardViewModel();

            var db = new SQLiteAsyncConnection(App.DBPath);
            var _card = await db.FindAsync<Card>(c1 => c1.Id == cardId);

            if (_card == null)
                return null;

            card.Id = _card.Id;
            card.FrontContent = _card.FrontContent;
            card.BackContent = _card.BackContent;
            card.DeckId = _card.DeckId;

            return card;
        }

        public async Task<string> GetFrontContent(int cardId)
        {
            string frontContent = String.Empty;

            var db = new SQLiteAsyncConnection(App.DBPath);
            var _card = await db.FindAsync<Card>(c1 => c1.Id == cardId);

            if (_card == null)
                return null;

            frontContent = _card.FrontContent;

            return frontContent;
        }

        public async Task<string> GetBackContent(int cardId)
        {
            string backContent = String.Empty;

            var db = new SQLiteAsyncConnection(App.DBPath);
            var _card = await db.FindAsync<Card>(c1 => c1.Id == cardId);

            if (_card == null)
                return null;

            backContent = _card.BackContent;

            return backContent;
        }

        public async Task<string> SaveCard(CardViewModel card)
        {
            string result = String.Empty;
            var db = new SQLiteAsyncConnection(App.DBPath);

            try
            {
                var existingCard = await (db.Table<Card>().Where(c1 => c1.Id == card.Id)).FirstOrDefaultAsync();

                if (existingCard != null)
                {
                    existingCard.FrontContent = card.FrontContent;
                    existingCard.BackContent = card.BackContent;
                    int success = await db.UpdateAsync(existingCard);
                }
                else 
                {
                    var _card = new Card();
                    _card.FrontContent = card.FrontContent;
                    _card.BackContent = card.BackContent;
                    _card.DeckId = card.DeckId;
                    int success = await db.InsertAsync(_card);
                }
                result = "Success";
            }
            catch
            {
                result = "this card was not saved";
            }
            return result;
        }

        public async Task<string> DeleteCard(int cardId)
        {
            string result = string.Empty;
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);

            var existingProject = await (db.Table<Card>().Where(
                c => c.Id == cardId)).FirstAsync();

            if (await db.DeleteAsync(existingProject) > 0)
            {
                result = "Success";
            }
            else
            {
                result = "This project was not removed";
            }
            return result;
        }
    }
}
