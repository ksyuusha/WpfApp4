using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfApp
{
    public delegate bool BlogPostFilter(BlogPost post);

    public partial class MainWindow : Window
    {
        private readonly List<BlogPost> blogPosts;     // Исходный список данных
        private List<BlogPost> filteredPosts;          // Отфильтрованный список

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация списка данных с примерами
            blogPosts = new List<BlogPost>
            {
                new BlogPost("Учебник WPF", new DateTime(2023, 10, 1), "WPF, C#, Учебник"),
                new BlogPost("Лучшие практики XAML", new DateTime(2023, 9, 15), "XAML, Лучшие практики"),
                new BlogPost("Изучение C#", new DateTime(2023, 8, 20), "C#, Начинающий"),
                new BlogPost("Расширенные техники C#", new DateTime(2024, 1, 5), "C#, Продвинутый"),
                new BlogPost("Начало работы с .NET", new DateTime(2022, 12, 10), "C#, .NET, Учебник"),
                new BlogPost("Изучаем LINQ", new DateTime(2023, 6, 5), "C#, LINQ")
            };

            // Отображение всех постов по умолчанию
            filteredPosts = new List<BlogPost>(blogPosts);
            UpdateListBox();
        }

        // Обновление ListBox с отфильтрованными постами
        private void UpdateListBox()
        {
            PostsListBox.ItemsSource = null;
            PostsListBox.ItemsSource = filteredPosts;
        }

        // Обработка нажатия на кнопку "Фильтровать"
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Определяем, какой фильтр выбран в ComboBox
            if (FilterComboBox.SelectedIndex == 0) // Фильтр по дате
            {
                if (DateTime.TryParse(FilterInput.Text, out DateTime filterDate))
                {
                    // Применяем фильтр по дате: все посты с точной датой filterDate
                    ApplyFilter(post => BlogPostFilters.FilterByDate(post, filterDate));
                }
                else
                {
                    MessageBox.Show("Введите корректную дату в формате гггг-мм-дд.");
                }
            }
            else if (FilterComboBox.SelectedIndex == 1) // Фильтр по ключевым словам
            {
                string keyword = FilterInput.Text;
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    // Применяем фильтр по ключевым словам
                    ApplyFilter(post => BlogPostFilters.FilterByKeyword(post, keyword));
                }
                else
                {
                    MessageBox.Show("Введите ключевое слово для фильтрации.");
                }
            }
        }

        // Обработка нажатия на кнопку "Сбросить фильтр"
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем фильтрацию и отображаем все посты
            filteredPosts = new List<BlogPost>(blogPosts);
            FilterComboBox.SelectedIndex = -1; // Сбрасываем выбор в ComboBox
            FilterInput.Clear(); // Очищаем поле ввода
            UpdateListBox(); // Обновляем ListBox
        }

        // Применение фильтра через делегат
        private void ApplyFilter(BlogPostFilter filter)
        {
            filteredPosts = blogPosts.Where(post => filter(post)).ToList();
            UpdateListBox();
        }
    }

    // Класс для фильтрации постов
    public static class BlogPostFilters
    {
        // Фильтр по дате публикации
        public static bool FilterByDate(BlogPost post, DateTime exactDate)
        {
            return post.Date.Date == exactDate.Date; // Проверяем точное совпадение даты
        }

        // Фильтр по ключевым словам
        public static bool FilterByKeyword(BlogPost post, string keyword)
        {
            return post.Keywords.IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }

    // Модель данных для статей блога
    public class BlogPost
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Keywords { get; set; }

        public BlogPost(string title, DateTime date, string keywords)
        {
            Title = title;
            Date = date;
            Keywords = keywords;
        }

        public override string ToString()
        {
            return $"{Title} - {Date.ToShortDateString()} - Ключевые слова: {Keywords}";
        }
    }
}
