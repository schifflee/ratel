using System;
using System.Collections.Generic;
using System.Text;

namespace Ratel.Web.Attributrs
{
    public class PageUrlAttribute : Attribute
    {
        public string Path { get; }
        public PageUrlAttribute(string path)
        {
            Path = path;
        }
    }
}
