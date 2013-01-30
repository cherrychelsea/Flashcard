using SQLite;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Flashcard.ViewModels;

namespace Flashcard.DataModel
{

    public sealed class FlashcardDataSource
    {
        private static FlashcardDataSource _flashcardDataSource = new FlashcardDataSource();

        private static String databasePath = String.Empty;
        public FlashcardDataSource()
        {
            databasePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDatabase.db");
            using (var database = new SQLiteConnection(databasePath))
            {
                database.CreateTable<Category>();
                database.CreateTable<Deck>();
                database.CreateTable<Card>();
            }
            var category1 = new CategoriesViewModel("", "english");
            AddCategory(category1);
            AllCategories.Add(category1);
            var deck1 = new DeckViewModel("", category1, "1001 words need to know", "Thang", "toefl", "", "");
            AddDeck(deck1);
            AllDecks.Add(deck1);
            var card1 = new CardsViewModel("", "abc", "abc", deck1);
            AddCard(card1);
            AllCards.Add(card1);
        }

        private ObservableCollection<CategoriesViewModel> _allCategories = new ObservableCollection<CategoriesViewModel>();
        public ObservableCollection<CategoriesViewModel> AllCategories
        {
            get { return this._allCategories; }
        }

        private ObservableCollection<DeckViewModel> _allDecks = new ObservableCollection<DeckViewModel>();
        public ObservableCollection<DeckViewModel> AllDecks
        {
            get { return this._allDecks; }
        }

        private ObservableCollection<CardsViewModel> _allCards = new ObservableCollection<CardsViewModel>();
        public ObservableCollection<CardsViewModel>AllCards
        {
            get { return this._allCards; }
        }

        // function for category
        // get
        // add
        // edit
        // delete
        public static ObservableCollection<CategoriesViewModel> GetCategories(String uniqueId)
        {
            _flashcardDataSource._allCategories.Clear();

            using (var database = new SQLiteConnection(databasePath))
            {
                var _categories = database.Table<Category>().OrderBy(c1 => c1.Name);
                foreach (var _category in _categories)
                {
                    CategoriesViewModel categoriesViewModel = new CategoriesViewModel(_category.Id.ToString(), _category.Name);
                    _flashcardDataSource._allCategories.Add(categoriesViewModel);
                }
            }
            return _flashcardDataSource._allCategories;
        }

        public static CategoriesViewModel GetCategory(String uniqueId)
        {
            foreach (var _category in _flashcardDataSource._allCategories)
                if (_category.UniqueId == uniqueId)
                    return _category;
            return null;
        }

        public static void AddCategory(CategoriesViewModel category)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                
                var _category = new Category();
                _category.Id = Guid.NewGuid();
                _category.Name = category.Name;
                database.Insert(_category);
                category.UniqueId = _category.Id.ToString();
            }
        }

        public static void UpdateCategory(CategoriesViewModel category)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _category = database.Find<Category>(c1 => c1.Id.ToString() == category.UniqueId);
                if (_category != null)
                {
                    _category.Name = category.Name;
                    database.Update(_category);
                }
            }
        }

        public static void DeleteCategory(CategoriesViewModel category)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _category = database.Find<Category>(c1 => c1.Id.ToString() == category.UniqueId);
                database.Delete(category);
            }
        }

        //function for deck
        // get
        // add
        // edit
        // delete
        public static ObservableCollection<DeckViewModel> GetDecks(CategoriesViewModel category)
        {
            _flashcardDataSource._allDecks.Clear();

            using (var database = new SQLiteConnection(databasePath))
            {
                var decks = database.Table<Deck>().Where(d1 => d1.Id.ToString() == category.UniqueId).OrderBy(d2 => d2.Title);
                foreach (var deck in decks)
                {
                    DeckViewModel flashcardDataDeck = new DeckViewModel(""+deck.Id, 
                                                                                category,
                                                                                deck.Title,
                                                                                deck.Author,
                                                                                deck.Subject,
                                                                                deck.Description,
                                                                                deck.ImagePath);
                    _flashcardDataSource._allDecks.Add(flashcardDataDeck);
                }
            }
            return _flashcardDataSource.AllDecks;
        }

        public static DeckViewModel GetDeck(String uniqueId)
        {
            foreach (var _deck in _flashcardDataSource._allDecks)
                if (_deck.UniqueId == uniqueId)
                    return _deck;
            return null;
        }

        public static void AddDeck(DeckViewModel deck)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _deck = new Deck();
                _deck.Id = Guid.NewGuid();
                _deck.Title = deck.Title;
                _deck.Author = deck.Author;
                _deck.Subject = deck.Subject;
                _deck.Description = deck.Description;
                _deck.ImagePath = deck.ImagePath;
                _deck.CategoryId = new Guid(deck.Category.UniqueId);
                database.Insert(_deck);
                deck.UniqueId = _deck.Id.ToString();
            }
        }

        public static void UpdateDeck(DeckViewModel deck)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _deck = database.Find<Deck>(d1 => d1.Id.ToString() == deck.UniqueId);
                if (_deck != null)
                {
                    _deck.CategoryId = new Guid(deck.Category.UniqueId);
                    _deck.Title = deck.Title;
                    _deck.Author = deck.Author;
                    _deck.Subject = deck.Subject;
                    _deck.Description = deck.Description ;
                    _deck.ImagePath = deck.ImagePath;
                    database.Update(deck);
                }
            }
        }

        public static void DeleteDeck(DeckViewModel deck)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _deck = database.Find<Deck>(d1 => d1.Id.ToString() == deck.UniqueId);
                database.Delete(_deck);
            }
        }



        // function for card
        // get
        // add
        // edit
        // delete
        public static ObservableCollection<CardsViewModel> GetCards(DeckViewModel deck)
        {
            // Simple linear search is acceptable for small data sets
            _flashcardDataSource._allCards.Clear();
            using (var database = new SQLiteConnection(databasePath))
            {
                var _cards = database.Table<Card>().Where(c1 => c1.DeckId.ToString() == deck.UniqueId).OrderBy(c2 => c2.FrontContent);
                foreach (var _card in _cards)
                {
                    CardsViewModel cardViewModel = new CardsViewModel(_card.Id.ToString(), _card.FrontContent, _card.BackContent, deck);
                    _flashcardDataSource._allCards.Add(cardViewModel);
                }
            }
            return _flashcardDataSource._allCards;
        }

        public static CardsViewModel GetCard(String uniqueId)
        {
            foreach (var _card in _flashcardDataSource._allCards)
                if (_card.UniqueId == uniqueId)
                    return _card;
            return null;
        }

        public static void AddCard(CardsViewModel card)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _card = new Card();
                _card.Id = Guid.NewGuid();
                _card.FrontContent = card.FrontContent;
                _card.BackContent = card.BackContent;
                _card.DeckId = new Guid(card.Deck.UniqueId);
                database.Insert(_card);
                card.UniqueId = _card.Id.ToString();
            }
        }

        public static void UpdateCard(CardsViewModel card)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _card = database.Find<Card>(c1 => c1.Id.ToString() == card.UniqueId);
                if (_card != null)
                {
                    _card.FrontContent = card.FrontContent;
                    _card.BackContent = card.BackContent;
                    database.Update(_card);
                }
            }
        }

        public static void DeleteCard(CardsViewModel card)
        {
            using (var database = new SQLiteConnection(databasePath))
            {
                var _card = database.Find<Card>(c1 => c1.Id.ToString() == card.UniqueId);
                database.Delete(_card);
            }
        }
    }
}
