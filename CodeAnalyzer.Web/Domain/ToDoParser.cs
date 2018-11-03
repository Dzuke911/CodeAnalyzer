using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeAnalyzer.Web.Domain
{
    public class ToDoParser
    {
        public static async Task<List<ToDoItem>> Parse(IFormFile file)
        {
            Regex reg = new Regex("[//] TODO(.+)");

            var result = new List<ToDoItem>();
            var currentItems = new List<ToDoItem>();
            var prevStrings = new List<string>();

            string line;
            int currrentLineNum = 1;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    line = await reader.ReadLineAsync();

                    Match match = reg.Match(line);

                    if (match.Success)
                    {
                        ToDoItem item = new ToDoItem();

                        foreach (string str in prevStrings)
                        {
                            AddHintLine(item,str);
                        }

                        item.ToDo = match.Groups[1].Value.Trim();
                        item.Number = currrentLineNum;

                        result.Add(item);
                        currentItems.Add(item);
                    }

                    if (prevStrings.Count >= 5)
                    {
                        prevStrings.RemoveAt(0);
                    }

                    prevStrings.Add($"<i>{currrentLineNum.ToString()}: </i>{line}");

                    foreach (ToDoItem item in currentItems)
                    {
                        AddHintLine(item, line, currrentLineNum);
                    }

                    currentItems.RemoveAll(td => td.Number <= currrentLineNum - 5);

                    currrentLineNum++;
                }
            }

            return result;
        }

        private static void AddHintLine(ToDoItem item, string line, int num)
        {
            if(item.Number == num)
            {
                item.Hint.Append($"<span style='color:green'><i>{ num.ToString()}: </i>{line}</span><br>");
            }
            else
            {
                item.Hint.Append($"<i>{num}: </i>{line}<br>");
            }
        }

        private static void AddHintLine(ToDoItem item, string line)
        {
            item.Hint.Append(line + "<br>");
        }
    }
}
