using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Flashcard.Views;
using SQLite;
using Flashcard.Models;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace Flashcard
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 

        public static string DBPath = String.Empty;
        public static int CurrentCategoryId { get; set; }

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDatabase.s3db");
                //DBPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "customers.s3db");
                using (var db = new SQLiteConnection(DBPath))
                {
                    db.CreateTable<Category>();
                    db.CreateTable<Deck>();
                    db.CreateTable<Card>();
                    
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            //
            ResetData();
            
            //

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage)))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }


        private async void ResetData()
        {
            var db = new SQLite.SQLiteAsyncConnection(DBPath);

            // Empty the Customer and Project tables 
            var categories = await db.Table<Category>().ToListAsync();
            foreach (Category c in categories)
            {
                await db.DeleteAsync(c);
            }
            var decks = await db.Table<Deck>().ToListAsync();
            foreach (Deck d in decks)
            {
                await db.DeleteAsync(d);
            }

            // Add seed customers and projects
            var newCategory1 = new Category()
            {
                Name = "abc"
            };
            await db.InsertAsync(newCategory1);
            var newCategory2 = new Category()
            {
                Name = "xyz"
            };
            await db.InsertAsync(newCategory2);
            var newCategory3 = new Category()
            {
                Name = "123"
            };
            await db.InsertAsync(newCategory3);
           
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
