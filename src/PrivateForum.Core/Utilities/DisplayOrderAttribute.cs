using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateForum.Core.Utilities
{

    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayOrderAttribute : Attribute
    {

        /// <summary>
        /// The Identity Code
        /// </summary>
        public int DisplayOrder { get { return displayOrder; } }
        private int displayOrder;

        public DisplayOrderAttribute(int displayOrder)
        {
            this.displayOrder = displayOrder;
        }

    }
}
