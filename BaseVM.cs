using System.ComponentModel;

namespace Epenthesis_2.ViewModel
{
    class BaseVM : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        protected void Set<T>(ref T field, T newValue, string propertyName) where T : class
        {

            if (field != newValue)
            {
                field = newValue;
                OnPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
