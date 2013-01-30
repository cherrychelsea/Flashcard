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
    class CategoriesViewModel:ViewModelBase
    {
        private ObservableCollection<CategoryViewModel> _categories;
        public ObservableCollection<CategoryViewModel> Categories
        {
            get 
            {
                return _categories;
            }
            set
            {
                _categories = value;
                RaisePropertyChanged("Categories");
            }
        }

        public async Task<ObservableCollection<CategoryViewModel>> GetCategories()
        {
            _categories = new ObservableCollection<CategoryViewModel>();
            var db = new SQLiteAsyncConnection(App.DBPath);

            var query = await db.Table<Category>().OrderBy(c => c.Name).ToListAsync();

            foreach (var c in query)
            {
                var _category = new CategoryViewModel();
                _category.Id = c.Id;
                _category.Name = c.Name;

                _categories.Add(_category);
            }
            return _categories;
        }
    }
}
