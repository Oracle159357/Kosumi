using System.ComponentModel;

namespace Kosumi.Presentation
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}