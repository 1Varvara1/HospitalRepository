using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBLL.Models
{
     public class DrugBLL
    {
        public int IdDrugs { get; set; }
        public string DrugsName { get; set; }
        public string Path { get; set; }
        public DrugBLL(int id, string name)
        {
            this.IdDrugs =id;
            this.DrugsName=name;
        }
        public DrugBLL(int id, string name , string path)
        {
            this.IdDrugs = id;
            this.DrugsName = name;
            Path = path;
        }
    }
}
