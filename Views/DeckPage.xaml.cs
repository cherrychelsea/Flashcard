using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Flashcard.ViewModels;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Flashcard.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeckPage : Page
    {
        private DeckViewModel deck;

        public DeckPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var categories = new CategoriesViewModel();
            if (e.Parameter == null)
            {
                deck = new DeckViewModel();
                PageTitle.Text = "Add New Deck";
                ObservableCollection<CategoryViewModel> cs = await categories.GetCategories();
                CategoryComboBox.ItemsSource = cs.ToList();
                CategoryComboBox.SelectionChanged += ComboBox_SelectionChanged;
            }
            else
            {
                deck = (DeckViewModel)e.Parameter;
                PageTitle.Text = "Edit Deck";
                
                CategoryComboBox.ItemsSource = categories.GetCategories();
                CategoryComboBox.SelectionChanged += ComboBox_SelectionChanged;

                TitleTextBox.Text = deck.Title;
                AuthorTextBox.Text = deck.Author;
                SubjectTextBox.Text = deck.Subject;
                DescriptionTextBox.Text = deck.Description;
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void SaveButton_Click(Object sender, RoutedEventArgs e)
        {
            string result = await deck.SaveDeck(deck);
            if (result.Contains("Success"))
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
