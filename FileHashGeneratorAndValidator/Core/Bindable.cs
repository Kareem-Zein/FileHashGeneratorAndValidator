using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileHashGeneratorAndValidator.Core
{
    public class Bindable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Set<T>(ref T propert, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(propert, value))
                return;

            propert = value;
            OnPropertyChanged(propertyName);
        }
    }
}
