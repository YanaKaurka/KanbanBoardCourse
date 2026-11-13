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

namespace KanbanBoardCourse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext _db;

        public MainWindow()
        {
            InitializeComponent();

            // 2. Создаем экземпляр контекста
            _db = new AppDbContext();

            // 3. Говорим EF Core: "Создай базу и все таблицы, если их еще нет"
            _db.Database.EnsureCreated();

            // 4. Заполняем стартовыми данными (если база пустая)
            InitializeData();
        }

        private void InitializeData()
        {

        }
    }
}