using CodeAnalyzer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAnalyzer.Web.Domain
{
    public class DBManager : IDBManager
    {
        private DatabaseContext _context;

        public DBManager(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> CreateFile(string fileName, DateTime time)
        {
            if(time == null)
            {
                throw new ArgumentNullException(nameof(time));
            }

            FileEntity entity = new FileEntity { Name = fileName, SaveTime = time };

            _context.Files.Add(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> CreateTask(ToDoItem toDoItem, int fileId)
        {
            if (toDoItem == null)
            {
                throw new ArgumentNullException(nameof(toDoItem));
            }

            TaskEntity entity = new TaskEntity { FileID = fileId, StringNumber = toDoItem.Number, Task = toDoItem.ToDo, Tooltip = toDoItem.Hint.ToString() };

            _context.Tasks.Add(entity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
