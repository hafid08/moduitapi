using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuitAPI
{
    public abstract class BaseModel {

        public int id { get; set; }
        public int category { get; set; }
        public DateTime createdAt { get; set; }
    }
    public class ModuitModel : BaseModel
    {
        public string title { get; set; }
        public string footer { get; set; }
        public string description { get; set; }
        public string[] tags { get; set; }
    }
    public class MainModel : BaseModel
    {
        public List<ModuitModel> items { get; set; }
    }
}
