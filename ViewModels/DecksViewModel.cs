using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Flashcard.ViewModels
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public class DecksViewModel : Flashcard.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public DecksViewModel(String uniqueid, CategoriesViewModel category, String title, String author, String subject, String description, String imagePath)
        {
            this._uniqueid = uniqueid;
            this._category = category;
            this._title = title;
            this._author = author;
            this._subject = subject;
            this._description = description;
            this._imagePath = imagePath;
            this.Items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<CardsViewModel> _items = new ObservableCollection<CardsViewModel>();
        public ObservableCollection<CardsViewModel> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<CardsViewModel> _topItem = new ObservableCollection<CardsViewModel>();
        public ObservableCollection<CardsViewModel> TopItems
        {
            get { return this._topItem; }
        }

        private String _uniqueid;
        public String UniqueId
        {
            get { return this._uniqueid; }
            set { this.SetProperty(ref this._uniqueid, value); }
        }

        private CategoriesViewModel _category;
        public CategoriesViewModel Category
        {
            get { return this._category; }
            set { this._category = value; }
        }

        private String _title;
        public String Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private String _author;
        public String Author
        {
            get { return this._author; }
            set { this.SetProperty(ref this._author, value); }
        }

        private String _subject;
        public String Subject
        {
            get { return _subject; }
            set { this.SetProperty(ref this._subject, value); }
        }

        private String _description;
        public String Description
        {
            get { return _description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private String _imagePath;
        public String ImagePath
        {
            get { return _imagePath; }
            set { this.SetProperty(ref this._imagePath, value); }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(_baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
