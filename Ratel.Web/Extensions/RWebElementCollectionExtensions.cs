using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratel.Web.Extensions
{
    public static class RWebElementCollectionExtensions
    {
        public static List<string> SelectText(this RWebElementCollection collection)
        {
            return collection.Select(x => x.Text).ToList();
        }

        public static List<string> SelectTagNames(this RWebElementCollection collection)
        {
            return collection.Select(x => x.TagName).ToList();
        }

        public static List<string> SelectValue(this RWebElementCollection collection)
        {
            return collection.Select(x => x.Value).ToList();
        }

        public static List<string> SelectStyles(this RWebElementCollection collection)
        {
            return collection.Select(x => x.Style).ToList();
        }

        public static List<string> SelectAttributes(this RWebElementCollection collection, string attributeName)
        {
            return collection.Select(x => x.GetAttribute(attributeName)).ToList();
        }
    }
}
