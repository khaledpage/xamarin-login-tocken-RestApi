using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kalksi.Models
{
    public class MasterMenuItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Color BackgroundColor { get; set; }
        public Type TargetType { get; set; }

        public MasterMenuItem(string title, string iconsrc, Color col, Type target)
        {
            Title = title;
            IconSource = iconsrc;
            BackgroundColor = col;
            TargetType = target;
        }
    }
}
