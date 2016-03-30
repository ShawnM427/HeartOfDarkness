/* Author:      Shawn
 * Date:        3/7/2015 2:49:46 AM
 * Project:     
 * Description:
 *  
 */

using HeartOfDarkness.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HeartOfDarkness.Scripting
{
    public class ScriptTools
    {
        /// <summary>
        /// Evaluates a string and excecutes any embedded scripts that are denoted by %script%
        /// to put an actual % in the string, you need to use \%
        /// </summary>
        /// <param name="text">The string to parse and excecute</param>
        /// <param name="context">The lua context to evaluate with</param>
        /// <returns>The line of text with the scripts evaluated and put in place</returns>
        public static string GetTextEmbedded(string text, LuaContext context)
        {
            string message = text;

            // Get the regex matches for %xxx%
            MatchCollection matches = Regex.Matches(message, @"[^\\]%([^%]*)%");

            // Iterate over all matches
            foreach (Match command in matches)
            {
                // We check each time, because some scripts are used multiple times in one string
                if (message.Contains(command.Value))
                {
                    // Trim the %'s off the match
                    string luaCommand = command.Value.Trim('%', command.Value[0]);

                    // Make sure the script has a return
                    if (!luaCommand.Contains("return "))
                        luaCommand = luaCommand.Insert(0, "return ");

                    // Perform the script and convert the return to a string
                    string result = (string)context.DoString(luaCommand)[0];

                    // Puts the result into the message
                    message = message.Replace(command.Value.Trim(command.Value[0]), result);
                }
            }

            // Replace \% with just plain %
            message = message.Replace("\\%", "%");

            // Return the result
            return message;
        }
    }
}
