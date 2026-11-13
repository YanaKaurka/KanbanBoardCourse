using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace KanbanBoardCourse.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "Сотрудник";
        public bool IsActive { get; set; } = true;

        public List<Board> Boards { get; set; } = new();
        public List<Task> CreatedTasks { get; set; } = new();
        public List<Task> AssignedTasks { get; set; } = new();

    }
}
