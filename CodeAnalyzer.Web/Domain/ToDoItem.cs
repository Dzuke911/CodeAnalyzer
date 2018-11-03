using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalyzer.Web.Domain
{
    public class ToDoItem
    {
        public ToDoItem()
        {
            Hint = new StringBuilder();
        }

        public StringBuilder Hint { get; set; }
        public string ToDo { get; set; }
        public int Number { get; set; }
    }
}
