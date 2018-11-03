using CodeAnalyzer.Web.Attribute;
using CodeAnalyzer.Web.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeAnalyzer.Web.Models
{
    public class IndexModel
    {
        [CheckFile(500)]
        public IFormFile File { get; set; }

        public List<ToDoItem> List { get; set; }
    }
}
