using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Flashcard.Models;
namespace Flashcard.ViewModels
{
    public class CategoryViewModel:ViewModelBase
    {
        #region Properties
        private int _id;
        public int Id
        {
            get 
            {
                return _id;
            }
            set 
            { 
                if (_id == value)
                { return ;}

                _id = value;
                RaisePropertyChanged("Id");
            }
        }

        private String _name;
        public String Name
        {
            get 
            {
                return this._name; 
            }
            set 
            {
                if (_name == value)
                { return; }

                _name = value;
                isDirty = true;
                RaisePropertyChanged("Name");
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

        public async Task<CategoryViewModel> GetCategory(int categoryId)
        {
            var category = new CategoryViewModel();
            var db = new SQLiteAsyncConnection(App.DBPath);

            var _category = await db.FindAsync<Category>(c => c.Id == categoryId);

            if (_category == null)
                return null;

            category.Id = _category.Id;
            category.Name = _category.Name;

            return category;
        }

        public async Task<string> GetCategoryName(int categoryId)
        {
            var name = String.Empty;
            var db = new SQLiteAsyncConnection(App.DBPath);

            var _category = await db.FindAsync<Category>(c => c.Id == categoryId);

            if (_category != null)
                name = _category.Name;

            return name;
        }

        public async Task<string> SaveCategory(CategoryViewModel category)
        {
            string result = String.Empty;
            var db = new SQLiteAsyncConnection(App.DBPath);

            try
            {
                var existingCategory = await (db.Table<Category>().Where(c1 => c1.Id == category.Id)).FirstOrDefaultAsync();

                if (existingCategory != null)
                {
                    existingCategory.Name = category.Name;
                    int success = await db.UpdateAsync(existingCategory);
                }
                else
                {
                    var _category = new Category();
                    _category.Name = category.Name;
                    int success = await db.InsertAsync(_category);
                }

                result = "success";
            }
            catch
            {
                result = "this category was not saved";
            }

            return result;
        }

        public async Task<string> DeleteCategory(int categoryId)
        {
            string result = string.Empty;
            var db = new SQLite.SQLiteAsyncConnection(App.DBPath);

            var existingCategory = await (db.Table<Category>().Where(
                d => d.Id == categoryId)).FirstAsync();

            try
            {
                var _decks = await db.Table<Deck>().Where(d1 => d1.CategoryId == existingCategory.Id).ToListAsync();

                foreach (var _deck in _decks)
                {
                    var _cards = await db.Table<Card>().Where(c1 => c1.DeckId == _deck.Id).ToListAsync();
                    foreach (var _card in _cards)
                    {
                        int s = await db.DeleteAsync(_card);
                    }
                    int suc = await db.DeleteAsync(_deck);
                }
                int succcess = await db.DeleteAsync(existingCategory);
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
