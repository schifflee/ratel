using System;
using System.Collections.Generic;
using System.Text;

namespace Ratel.Web.Models
{
    public class ConditionModel
    {
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public bool Operator { get; set; }
    }
}
