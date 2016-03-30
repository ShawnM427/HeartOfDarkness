/* Author:      Shawn
 * Date:        5/12/2015 6:08:53 PM
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
    public class NPC : Entity
    {
        string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
    }
}
