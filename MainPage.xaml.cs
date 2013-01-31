using System;
using System.Collections.Generic;
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
using Flashcard.Views;
using System.Collections.ObjectModel;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Flashcard.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Flashcard.Common.LayoutAwarePage
    {
        // CategoryPage = Customer Page
        // DeckPage = Project Page
        CategoriesViewModel categoriesViewModel = null;
        ObservableCollection<CategoryViewModel> categories = null;
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GetData();
            base.OnNavigatedTo(e);
        }

        private async void GetData()
        {
            categoriesViewModel = new CategoriesViewModel();
            categories = await categoriesViewModel.GetCategories();
            CategoriesViewSource.Source = categories;
            CategoriesGridView.SelectedItem = null;
        }

        private void CategoriesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(DeckPage), e.ClickedItem);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CategoryPage));
        }

        private void CategoriesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesGridView.SelectedItems.Count() > 0)
            {
                MainPageAppBar.IsOpen = true;
                MainPageAppBar.IsSticky = true;
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Visible;
            }
            else
            {
                MainPageAppBar.IsOpen = false;
                MainPageAppBar.IsSticky = false;
                AddButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CategoryPage), CategoriesGridView.SelectedItem);
        }

    }
}
