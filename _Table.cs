using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class _Table
    {

        public _Table(int Id, string Name, string Photo, string Info)
        {
            this.Id = Id;
            this.Name = Name;
            this.Photo = Photo;
            this.Info = Info;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Info { get; set; }
    }
}
