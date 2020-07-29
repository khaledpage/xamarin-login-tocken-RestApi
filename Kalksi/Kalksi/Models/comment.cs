using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Kalksi.Models
{
    public class comment
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }

        public comment()
        {

        }

        public static implicit operator ObservableCollection<object>(comment v)
        {
            throw new NotImplementedException();
        }
    }

}
