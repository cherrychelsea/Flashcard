using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.ViewModels
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public class CategoriesViewModel : Flashcard.Common.BindableBase
    {
        private String _uniqueId;
        public String UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private String _name;
        public String Name
        {
            get { return this._name; }
            set { this.SetProperty(ref this._name, value); }
        }

        public CategoriesViewModel(String uniqueId, String name)
        {
            this._uniqueId = uniqueId;
            this._name = name;
        }
    }
}
