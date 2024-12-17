using labat3.Models;
using labat3.Services;
using labat3.Views;

namespace labat3.Views
{
    public partial class MainPage : ContentPage
    {
        public List<Book> Books { get; set; }
        private readonly JsonManager jsonManager;

        public MainPage()
        {
            InitializeComponent();

            jsonManager = new JsonManager();
            Books = jsonManager.LoadBooks();
            BindingContext = this;
        }

        // Додавання нової книги
        private async void OnEditBookClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Book bookToEdit)
            {
                await Navigation.PushModalAsync(new BookEditorPage(bookToEdit));
                RefreshBooksList();
            }
        }
        private async void OnAddBookClicked(object sender, EventArgs e)
        {
            var newBook = new Book
            {
                Id = Books.Count + 1,
                Title = "Нова книга",
                Author = "Автор",
                Year = DateTime.Now.Year,
                Genre = "Жанр"
            };

            await Navigation.PushModalAsync(new BookEditorPage(newBook));
            Books.Add(newBook);
            RefreshBooksList();
        }







        // Видалення книги
        private async void OnDeleteBookClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Book bookToDelete)
            {
                bool confirm = await DisplayAlert("Підтвердження", $"Видалити книгу \"{bookToDelete.Title}\"?", "Так", "Скасувати");
                if (confirm)
                {
                    Books.Remove(bookToDelete);
                    RefreshBooksList();
                }
            }
        }

        // Збереження книг
        private void OnSaveBooksClicked(object sender, EventArgs e)
        {
            jsonManager.SaveBooks(Books);
            DisplayAlert("Збережено", "Список книг збережено успішно!", "OK");
        }

        // Вихід з програми
        private async void OnExitAppClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Підтвердження", "Ви дійсно бажаєте вийти з програми?", "Так", "Скасувати");
            if (confirm)
            {
                Application.Current.Quit();
            }
        }

        // Оновлення списку книг
        private void RefreshBooksList()
        {
            BooksListView.ItemsSource = null; // Скидаємо прив'язку
            BooksListView.ItemsSource = Books; // Задаємо знову список книг
        }

    }
}
