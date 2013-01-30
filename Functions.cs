using Flashcard.DataModel;
using Flashcard.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard
{
    class Functions
    {
        public Functions()
        {
        }

        public static CategoryViewModel GetCategory(String name)
        {
            var _categories = FlashcardDataSource.GetCategories("");
            foreach(var _category in _categories)
                if (_category.Name == name)
                    return _category;
            return null;
        }

        public static void ClickAddCategory(String name)
        {
            CategoryViewModel _category = GetCategory(name);
            if (_category == null)
            {
                CategoryViewModel newCategory = new CategoryViewModel("",name);
                FlashcardDataSource.AddCategory(newCategory);
            }
        }

        public static void ClickEditCategory(CategoryViewModel category, String newCategoryName)
        {
            if (category != null)
            {
                category.Name = newCategoryName;
                FlashcardDataSource.UpdateCategory(category);
            }
        }

        public static void ClickDeleteCategory(CategoryViewModel category)
        {
            if (category != null)
                FlashcardDataSource.DeleteCategory(category);
        }

        public static DeckViewModel GetDeck(CategoryViewModel category,String title)
        {
            var _decks = FlashcardDataSource.GetDecks(category);
            foreach (var _deck in _decks)
            {
                if (_deck.Title == title)
                    return _deck;
            }
            return null;
        }

        public static void ClickAddDeck(String title, String author, String subject, String description, String imagePath, CategoryViewModel category)
        {
            var _deck = GetDeck(category, title);
            if (_deck == null)
            {
                _deck = new DeckViewModel("", category, title, author, subject, description, imagePath);
                FlashcardDataSource.AddDeck(_deck);
            }
        }

        public static void ClickEditDeck(DeckViewModel deck, String title, String author, String subject, String description, String imagePath)
        {
            if (deck != null)
            {
                deck.Title = title;
                deck.Author = author;
                deck.Subject = subject;
                deck.Description = description;
                deck.ImagePath = imagePath;
                FlashcardDataSource.UpdateDeck(deck);
            }
        }

        public static void ClickDeleteDeck(DeckViewModel deck)
        {
            if (deck != null)
            {
                FlashcardDataSource.DeleteDeck(deck);
            }
        }

        public static CardsViewModel GetCard(DeckViewModel deck, String frontContent)
        {
            var _cards = FlashcardDataSource.GetCards(deck);
            foreach (var _card in _cards)
            {
                if (_card.FrontContent == frontContent)
                {
                    return _card;
                }
            }
            return null;
        }

        public static void ClickAddCard(DeckViewModel deck, String frontContent, String backContent)
        {
            var _card = GetCard(deck, frontContent);
            if (_card == null)
            {
                _card = new CardsViewModel("", frontContent, backContent, deck);
                FlashcardDataSource.AddCard(_card);
            }
        }

        public static void ClickEditCard(CardsViewModel card, String frontContent, String backContent)
        {
            if (card != null)
            {
                card.FrontContent = frontContent;
                card.BackContent = backContent;
                FlashcardDataSource.UpdateCard(card);
            }
        }

        public static void ClickDeleteCard(CardsViewModel card)
        {
            if (card != null)
            {
                FlashcardDataSource.DeleteCard(card);
            }
        }
    }
}
