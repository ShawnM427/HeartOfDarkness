/* Author:      Shawn
 * Date:        2/24/2015 3:43:13 PM
 * Project:     
 * Description:
 *  
 */

using HeartOfDarkness.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HeartOfDarkness.Dialogue
{
    /// <summary>
    /// Represents a Lua condition + command that is used when a dialog choice is excepted
    /// </summary>
    public class DialogueCommand
    {
        string m_command;
        string m_condition;

        /// <summary>
        /// Gets or sets the Lua command that this command uses. If this is empty, then this command will not be written to XML
        /// </summary>
        public string Command
        {
            get { return m_command; }
            set
            {
                m_command = value;
            }
        
        }

        /// <summary>
        /// Gets or sets the Lua condition to check when trying to invoke this command
        /// </summary>
        public string Condition
        {
            get { return m_condition; }
            set
            {
                if (value.ToLower().Trim() == "true" || string.IsNullOrEmpty(value))
                    m_condition = "true";
                else
                    m_condition = value;
            }
        }

        /// <summary>
        /// Creates a new empty dialog command
        /// </summary>
        public DialogueCommand()
        {
            m_command = "";
            m_condition = "true";
        }

        /// <summary>
        /// Creates a new Dialog Command with no condition
        /// </summary>
        /// <param name="command">The Lua command to use when invoked</param>
        public DialogueCommand(string command)
        {
            Command = command;
            m_condition = "true";
        }
        
        /// <summary>
        /// Creates a new Dialog Command with the given command and condition
        /// </summary>
        /// <param name="command">The Lua command to use when invoked</param>
        /// <param name="condition">The Lua condition to check before invoking</param>
        public DialogueCommand(string command, string condition)
        {
            Command = command;
            Condition = condition;
        }

        /// <summary>
        /// Checks if the conditions are met to run this command
        /// </summary>
        /// <param name="context">The Lua context to run the command with</param>
        /// <returns>True if the command's conditions are met, otherwise false</returns>
        public bool ConditionsMet(LuaContext context)
        {
            return (bool)context.DoString("return " + m_condition)[0]; // Simply perform the Lua operation and return it's result
        }

        /// <summary>
        /// Performs this dialog command if the condition is met
        /// </summary>
        /// <param name="context">The Lua context to use for performing this command</param>
        public void Perform(LuaContext context)
        {
            if (ConditionsMet(context)) // Ensure that the command's conditions are met
                context.DoString(m_command); // Perform the command
        }

        /// <summary>
        /// Writes this command to an XML stream
        /// </summary>
        /// <param name="writer">The XML writer to write to</param>
        public void WriteToXml(XmlWriter writer)
        {
            // Only save this thing if there is actually a command to be excecuted
            if (!string.IsNullOrEmpty(m_command))
            {
                // Write the start of this command
                writer.WriteStartElement("command");                            // <command

                // Only write the condition attribute if there is one
                if (m_condition != "true")
                    writer.WriteAttributeString("condition", m_condition);      // condition="$condition"

                // Write the actual command as the value
                writer.WriteValue(m_command);                                   // >$command

                // End this command element
                writer.WriteEndElement();                                       // </command>
            }
        }

        /// <summary>
        /// Reads a DialogCommand from an XML node
        /// </summary>
        /// <param name="node">The node to read from</param>
        /// <returns>A DialogCommand read from the node</returns>
        public static DialogueCommand ReadFromXml(XmlNode node)
        {
            // Define a new command to return the result in
            DialogueCommand command = new DialogueCommand();

            // Set the command to the node's inner text
            command.m_command = node.InnerText;

            // If the node has the condtion attribute defined, load it's value into the condition
            if (node.Attributes["condition"] != null)
                command.m_condition = node.Attributes["condition"].Value;

            // Return the result
            return command;

        }

        /// <summary>
        /// Creates a human-readable string representation of this Dialog command
        /// </summary>
        /// <returns>A string representation of this instance</returns>
        public override string ToString()
        {
            return m_command + (m_condition != "true" ? " if " + m_condition : "");
        }
        
        /// <summary>
        /// Implicitly converts a string into a command. The command's condition will be "true"
        /// </summary>
        /// <param name="command">The text command to convert into a dialog command</param>
        /// <returns>A Dialog Command created from the command</returns>
        public static implicit operator DialogueCommand(string command)
        {
            return new DialogueCommand(command);
        }
    }
}
