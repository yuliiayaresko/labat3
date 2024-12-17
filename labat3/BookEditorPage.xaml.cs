using labat3.Models;

namespace labat3.Views
{
    public partial class BookEditorPage : ContentPage
    {
        public Book EditableBook { get; set; }

        public BookEditorPage(Book book)
        {
            InitializeComponent();
            EditableBook = book;
            BindingContext = EditableBook;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
