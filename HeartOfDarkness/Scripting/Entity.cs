/* Author:      Shawn
 * Date:        5/12/2015 6:03:28 PM
 * Project:     
 * Description:
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartOfDarkness.Scripting
{
    public abstract class Entity
    {
        protected AttributeCollection<object> myAttributes;
        protected ItemCollection myItems;

        public object this[string index]
        {
            get { return myAttributes[index]; }
            set { myAttributes[index] = value; }
        }

        protected Entity()
        {
            myItems = new ItemCollection();
            myAttributes = new AttributeCollection<object>();
        }
    }
}
