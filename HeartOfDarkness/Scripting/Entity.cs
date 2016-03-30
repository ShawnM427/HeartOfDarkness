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
    public class Entity
    {
        protected AttributeCollection<object> m_attributes;

        public object this[string index]
        {
            get { return m_attributes[index]; }
            set { m_attributes[index] = value; }
        }
    }
}
