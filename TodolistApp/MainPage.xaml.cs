using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace TodolistApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new TodoListViewModel();
        }
    }



    public class TodoListViewModel : INotifyPropertyChanged
    {
        public TodoListViewModel()
        {
            AddCommand = new Command<Entry>(
                entry =>
                {
                    if (!string.IsNullOrWhiteSpace(entry.Text))
                    {
                        Items.Add(new TodoItem { Title = entry.Text, Done = false });
                        entry.Text = string.Empty;
                    }
                });

            DeleteCommand = new Command<TodoItem>(
                x => Items.Remove(x),
                x => Items.Contains(x));
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ObservableCollection<TodoItem> Items { get; } = new ObservableCollection<TodoItem>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class TodoItem : INotifyPropertyChanged
    {
        private string _title;
        private bool _done;

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Done
        {
            get => _done;
            set
            {
                if (_done != value)
                {
                    _done = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
