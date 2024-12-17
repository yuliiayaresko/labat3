using System.Text.Json;
using labat3.Models;

namespace labat3.Services
{
    public class JsonManager
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "C:\\Users\\yulia\\source\\repos\\labat3\\labat3\\books.json");

        public List<Book> LoadBooks()
        {
            if (!File.Exists(filePath)) return new List<Book>();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        public void SaveBooks(List<Book> books)
        {
            string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
