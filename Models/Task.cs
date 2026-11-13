using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanBoardCourse.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = "Средний";

        // Сравниваем с системным временем
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int Position { get; set; }

        public int ColumnId { get; set; }
        public Column? Column { get; set; }

        public int CreatorId { get; set; }
        public User? Creator { get; set; }

        public int? AssigneeId { get; set; }
        public User? Assignee { get; set; }
    }
}
