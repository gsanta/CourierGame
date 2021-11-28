using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls
{
    public class TagManager
    {


        public static bool IsBuilding(string tag)
        {
            return tag == "Building Selector";
        }

        public static bool IsExitBuilding(string tag)
        {
            return tag == "Exit Building Selector";
        }

        public static bool IsHoverable(string tag)
        {
            return IsBuilding(tag) || IsExitBuilding(tag);
        }
    }
}
