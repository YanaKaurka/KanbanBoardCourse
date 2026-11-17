using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KanbanBoardCourse.Data;
using KanbanBoardCourse.Models;
using System.Windows.Controls; 
using System.Windows.Media;

namespace KanbanBoardCourse
{

    public partial class MainWindow : Window
    {
        private AppDbContext _db;
        private int? _selectedBoardId = null;

        public MainWindow()
        {
            InitializeComponent();

            _db = new AppDbContext();

            //сздание БД
            _db.Database.EnsureCreated();

            // заполнение БД данными
            
            var admin = new User
            {
                UserName = "admin",
                Password = "123",
                Role = "Руководитель",
                IsActive = true
            };

            
            var employee = new User
            {
                UserName = "user",
                Password = "123",
                Role = "Сотрудник",
                IsActive = true
            };

            _db.Users.AddRange(admin, employee);
            _db.SaveChanges();
            LoadBoards();
        }

        // загрузка досок
        private void LoadBoards()
        {
            BoardsPanel.Children.Clear();
            _selectedBoardId = null; 

            var boards = _db.Boards.ToList();

            foreach (var board in boards)
            {
                // Создаем контейнер
                Border card = new Border
                {
                    Width = 200,
                    Height = 200,
                    Background = new SolidColorBrush(Color.FromRgb(160, 160, 160)), 
                    Margin = new Thickness(10),
                    CornerRadius = new CornerRadius(5),
                    Cursor = Cursors.Hand,
                    BorderBrush = Brushes.Transparent, 
                    BorderThickness = new Thickness(3)
                };

                // Сохраняем ID доски в свойство 
                card.Tag = board.Id;

               
                card.MouseLeftButtonDown += Card_MouseLeftButtonDown;

               
                StackPanel stack = new StackPanel { Margin = new Thickness(15) };

                TextBlock title = new TextBlock
                {
                    Text = board.Title,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 0, 0, 10)
                };

                TextBlock desc = new TextBlock
                {
                    Text = board.Description,
                    FontSize = 14,
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = new SolidColorBrush(Color.FromRgb(50, 50, 50))
                };

                stack.Children.Add(title);
                stack.Children.Add(desc);
                card.Child = stack;

                BoardsPanel.Children.Add(card);
            }
        }

        // обработка клика по картточке и получение ID
        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border clickedCard = sender as Border;
            if (clickedCard != null)
            {
                // Снимаем выделение со всех карточек
                foreach (var child in BoardsPanel.Children)
                {
                    if (child is Border b)
                    {
                        b.BorderBrush = Brushes.Transparent;
                    }
                }

                // Выделяем текущую карточку (синяя рамка)
                clickedCard.BorderBrush = Brushes.DodgerBlue;

                // Запоминаем ID выбранной доски
                _selectedBoardId = (int)clickedCard.Tag;
            }
        }

        // кнопка добавления 
        private void BtnAddBoard_Click(object sender, RoutedEventArgs e)
        {
            AddBoardWindow addWindow = new AddBoardWindow();
            addWindow.Owner = this;

            if (addWindow.ShowDialog() == true)
            {
                LoadBoards();
            }
            
        }

        // кнопка удаления
        private void BtnDeleteBoard_Click(object sender, RoutedEventArgs e)
        {
            // проверяем, выбрана ли доска
            if (_selectedBoardId == null)
            {
                MessageBox.Show("Сначала выберите доску (кликните по ней левой кнопкой мыши).",
                                "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // спрашиваем подтверждение перед удалением
            var result = MessageBox.Show("Вы уверены, что хотите удалить эту доску?\nВсе колонки и задачи внутри неё будут безвозвратно удалены!",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Ищем доску в БД и удаляем
                var board = _db.Boards.FirstOrDefault(b => b.Id == _selectedBoardId);

                if (board != null)
                {
                    // удаление всего связанного с доской 
                    _db.Boards.Remove(board);
                    _db.SaveChanges();
                }

                //обновление главного экрана
                _selectedBoardId = null;
                LoadBoards();
                MessageBox.Show("Доска удалена.");
            }
        }
    }
}