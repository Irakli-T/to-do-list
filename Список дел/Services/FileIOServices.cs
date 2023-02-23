using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Список_дел.Models;

namespace Список_дел.Services
{
    internal class FileIOServices
    {
        private readonly string PATH;
        public FileIOServices(string path)
        {
             PATH = path;
        }
        public BindingList<TodoModel> LoadDate()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<TodoModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                if (fileText=="")
                { return new BindingList<TodoModel>(); }
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }

        public void SaveDate(object todoDataList)
        {
            using(StreamWriter writer = File.CreateText(PATH)) 
            { 
                string output = JsonConvert.SerializeObject(todoDataList);
                writer.WriteLine(output);
            }
        }
    }
}
