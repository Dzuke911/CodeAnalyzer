using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeAnalyzer.Web.Models;
using CodeAnalyzer.Web.Domain;
using Microsoft.AspNetCore.Http;

namespace CodeAnalyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDBManager _manager;

        public HomeController(IDBManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Upload(IndexModel model)
        {
            if (model.File != null && ModelState.IsValid)
            {
                List<ToDoItem>  list = await ToDoParser.Parse(model.File);
                model.List = list;

                HttpContext.Session.Set<List<ToDoItem>>("list", list);
                HttpContext.Session.SetString("fname", model.File.FileName);
            }
            else
            {
                model.List = new List<ToDoItem>();
            }

            return View(nameof(HomeController.Index), model);
        }

        public async Task<IActionResult> Save()
        {
            List<ToDoItem> list = HttpContext.Session.Get<List<ToDoItem>>("list");
            string fileName = HttpContext.Session.GetString("fname");

            int fileId = await _manager.CreateFile(fileName, DateTime.Now);

            foreach(ToDoItem item in list)
            {
                await _manager.CreateTask(item, fileId);
            }

            return Redirect(nameof(HomeController.Index));
        }

        public async Task<IActionResult> Index()
        {
            Class1 x = new Class1();
            await x.Xdd2();

            IndexModel model = new IndexModel { List = new List<ToDoItem>() };

            return View(nameof(HomeController.Index), model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
