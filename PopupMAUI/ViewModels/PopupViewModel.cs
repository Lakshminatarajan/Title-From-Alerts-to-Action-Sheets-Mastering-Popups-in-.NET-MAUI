using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.Maui.Popup;

namespace PopupMAUI
{
    public class PopupViewModel : INotifyPropertyChanged
    {
        private bool _isModalOpen;
        private bool _isToastOpen;
        private bool _isActionSheetOpen;
        private bool _isFormOpen;
        private bool _isTeachingOpen;
        private bool _isFullScreenOpen;
        private bool _isLoadingOpen;
        private bool _isIllustratedOpen;

        public bool IsModalOpen { get => _isModalOpen; set => Set(ref _isModalOpen, value); }
        public bool IsToastOpen { get => _isToastOpen; set => Set(ref _isToastOpen, value); }
        public bool IsActionSheetOpen { get => _isActionSheetOpen; set => Set(ref _isActionSheetOpen, value); }
        public bool IsFormOpen { get => _isFormOpen; set => Set(ref _isFormOpen, value); }
        public bool IsTeachingOpen { get => _isTeachingOpen; set => Set(ref _isTeachingOpen, value); }
        public bool IsFullScreenOpen { get => _isFullScreenOpen; set => Set(ref _isFullScreenOpen, value); }
        public bool IsLoadingOpen { get => _isLoadingOpen; set => Set(ref _isLoadingOpen, value); }
        public bool IsIllustratedOpen { get => _isIllustratedOpen; set => Set(ref _isIllustratedOpen, value); }

        private string _name = string.Empty;
        private string _notes = string.Empty;
        private string _toastText = string.Empty;
        private DateTime _date = DateTime.Today;
        private bool isToastVisible;
        public string Name { get => _name; set => Set(ref _name, value); }
        public string Notes { get => _notes; set => Set(ref _notes, value); }
        public string ToastText { get => _toastText; set => Set(ref _toastText, value); }
        public DateTime Date { get => _date; set => Set(ref _date, value); }

        public bool IsToastVisible
        {
            get { return isToastVisible; }
            set
            {
                isToastVisible = value;
                OnPropertyChanged("IsToastVisible");
            }
        }

        public ICommand OpenModalCommand { get; }
        public ICommand CloseModalCommand { get; }

        public ICommand OpenToastCommand { get; }
        public ICommand OpenActionSheetCommand { get; }
        public ICommand CloseActionSheetCommand { get; }

        // Action sheet item commands
        public ICommand ShareCommand { get; }
        public ICommand UploadCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand OpenFormCommand { get; }
        public ICommand CancelFormCommand { get; }
        public ICommand SaveFormCommand { get; }

        public ICommand OpenTeachingCommand { get; }
        public ICommand OpenFullScreenCommand { get; }
        public ICommand DismissFullScreenCommand { get; }

        public ICommand OpenLoadingCommand { get; }

        public ICommand OpenIllustratedCommand { get; }
        public ICommand CancelIllustratedCommand { get; }
        public ICommand ConfirmIllustratedCommand { get; }

        public PopupViewModel()
        {
            OpenModalCommand = new Command(() => IsModalOpen = true);
            CloseModalCommand = new Command(() => IsModalOpen = false);

            OpenToastCommand = new Command(async () =>
            {
                IsToastOpen = true;
                await Task.Delay(2000);
                IsToastOpen = false;
            });

            OpenActionSheetCommand = new Command(() => IsActionSheetOpen = true);
            CloseActionSheetCommand = new Command(() => IsActionSheetOpen = false);

            // Initialize action sheet item commands
            ShareCommand = new Command<string>(ExecuteAction);
            UploadCommand = new Command<string>(ExecuteAction);
            CopyCommand = new Command<string>(ExecuteAction);
            PrintCommand = new Command<string>(ExecuteAction);
            DeleteCommand = new Command<string>(ExecuteAction);

            OpenFormCommand = new Command(() => IsFormOpen = true);
            CancelFormCommand = new Command(() => IsFormOpen = false);
            SaveFormCommand = new Command(() =>
            {
                // validate & persist as needed
                IsFormOpen = false;
            });

            OpenTeachingCommand = new Command(() => IsTeachingOpen = true);

            OpenFullScreenCommand = new Command(() => IsFullScreenOpen = true);
            DismissFullScreenCommand = new Command(() => IsFullScreenOpen = false);

            OpenLoadingCommand = new Command(async () =>
            {
                IsLoadingOpen = true;
                try { await Task.Delay(2000); }
                finally { IsLoadingOpen = false; }
            });

            OpenIllustratedCommand = new Command(() => IsIllustratedOpen = true);
            CancelIllustratedCommand = new Command(() => IsIllustratedOpen = false);
            ConfirmIllustratedCommand = new Command(() => IsIllustratedOpen = false);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private async void ExecuteAction(string action)
        {
            // You can handle each action here.
            // For now, simply close the action sheet.
            IsActionSheetOpen = false;
            OnFileAction(action);
        }

        private async void OnFileAction(string action)
        {
            if (string.IsNullOrEmpty(action))
                return;

            ToastText = $"{action} action executed.";

            this.IsToastVisible = true;
            await Task.Delay(1000);
            this.IsToastVisible = false;
        }
        private bool Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(name);
            return true;
        }
    }
}
