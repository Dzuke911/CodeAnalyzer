using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAnalyzer.Web.Domain
{
    public interface IDBManager
    {
        Task<int> CreateFile(string fileName, DateTime time);
        Task CreateTask(ToDoItem toDoItem, int fileId);
        Task CreateTasks(IEnumerable<ToDoItem> toDoList, int fileId);
    }
}
