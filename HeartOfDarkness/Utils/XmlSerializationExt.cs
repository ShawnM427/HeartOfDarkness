/* Author:      Shawn
 * Date:        5/12/2015 5:11:29 PM
 * Project:     
 * Description:
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HeartOfDarkness
{
    public static class XmlSerializationExt
    {
        public static void WriteAttributeInt(this XmlWriter writer, string name, int value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteValue(value);
            writer.WriteEndAttribute();
        }

    }
}
