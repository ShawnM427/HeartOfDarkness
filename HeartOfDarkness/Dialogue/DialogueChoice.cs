/* Author:      Shawn
 * Date:        2/23/2015 11:48:49 PM
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
using System.Xml;

namespace HeartOfDarkness.Dialogue
{
    /// <summary>
    /// Represents a choice in a branching dialog tree
    /// </summary>
    public class DialogueChoice : IEquatable<DialogueChoice>
    {
        const string DEFAULT_MESSAGE = "RESPONSE NOT FOUND";

        private int m_id;
        private int m_nextId;
        public string m_condition;
        public string m_message;

        public List<DialogueCommand> m_commands;
        
        /// <summary>
        /// Gets or sets the ID for this choice
        /// </summary>
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        /// <summary>
        /// Gets or sets the ID of the dialog option that this choice points to
        /// </summary>
        public int NextId
        {
            get { return m_nextId; }
            set { m_nextId = value; }
        }
        
        /// <summary>
        /// Creates a new empty Dialog option
        /// </summary>
        public DialogueChoice()
        {
            m_commands = new List<DialogueCommand>();
            m_message = DEFAULT_MESSAGE;

            // Initialize ID to -1 so that we can flag it later if it fails to load correctly
            m_nextId = -1;
            m_condition = "true";
        }

        public DialogueChoice(string message) : this()
        {
            m_message = message;
        }

        /// <summary>
        /// Checks if this choice's conditions are met
        /// </summary>
        /// <param name="context">The Lua context to use for checking conditions</param>
        /// <returns>True if the choice's conditions are met, otherwise false</returns>
        public bool IsAvailable(LuaContext context)
        {
            return (bool)context.DoString(string.Format("return {0}", m_condition))[0]; // Simply perform the Lua operation and return it's result
        }

        /// <summary>
        /// Performs the actions tied to this choice if the choice is available
        /// </summary>
        /// <param name="context">The Lua context to use for excecuting commands</param>
        public void PerformCommands(LuaContext context)
        {
            if (IsAvailable(context)) // Ensure that this choice should be available
            {
                foreach (DialogueCommand command in m_commands) // Iterate over all commands and perform them
                    command.Perform(context);
            }
        }

        /// <summary>
        /// Writes this choice to an XML stream
        /// </summary>
        /// <param name="writer">The XML writer to write to</param>
        public void WriteToXml(XmlWriter writer, bool writeEditor = true)
        {
            // Begin this element
            writer.WriteStartElement("choice");                                 // <choice

            // Write the ID as an attribute
            writer.WriteAttributeString("id", m_id.ToString());                 // id="$id"

            // Write the Next dialog ID as an attribute
            writer.WriteAttributeString("nextId", m_nextId.ToString());         // nextId="$nextId"
            
            // We only write the condition if there is a unique condition
            if (m_condition != "true")
                writer.WriteAttributeString("condition", m_condition);          // condition="$condition"

            // Write the message to the output
            if (m_message != DEFAULT_MESSAGE)
                writer.WriteElementString("message", m_message);                // <message>$message</message>

            // Write the start of the commands list
            writer.WriteStartElement("commands");                               // <commands>

            // Iterate over each command and write it
            foreach (DialogueCommand command in m_commands)
                command.WriteToXml(writer);                                     //  <command.... />
            
            // Close the commands list
            writer.WriteEndElement();                                           // </commands>
               
            // Close this element
            writer.WriteEndElement();                                           // </choice>
        }

        /// <summary>
        /// Reads a DialogChoice from an XML node
        /// </summary>
        /// <param name="node">The node to read from</param>
        /// <returns>A DialogChoice read from the node</returns>
        public static DialogueChoice ReadFromXml(XmlNode node)
        {
            // Define an empty choice to return the result
            DialogueChoice choice = new DialogueChoice();

            // If the ID attribute exists, read it
            if (node.Attributes["id"] != null)
                choice.m_id = int.Parse(node.Attributes["id"].Value);

            // If the Next ID attribute exists, read it
            if (node.Attributes["nextId"] != null)
                choice.m_nextId = int.Parse(node.Attributes["nextId"].Value);

            // If the condition attribute exists, read it
            if (node.Attributes["condition"] != null)
                choice.m_condition = node.Attributes["condition"].Value;

            // If the message node exists, save it to the output
            if (node["message"] != null)
                choice.m_message = node["message"].InnerText;

            // Get the commands subnode
            XmlNode commands = node.SelectSingleNode("commands");

            // If the commands list is defined
            if (commands != null)
            {
                // Iterate over all subnodes
                foreach (XmlNode commandNode in commands)
                {
                    // Load the command from the subnode
                    choice.m_commands.Add(DialogueCommand.ReadFromXml(commandNode));
                }
            }

            // Return the result
            return choice;
        }

        /// <summary>
        /// Creates a human-readable string representation of this Dialog choice
        /// </summary>
        /// <returns>A string representation of this instance</returns>
        public override string ToString()
        {
            return string.Format("Dialog ID: {0} | Commands: {1} | Condition: {2}", m_nextId, m_commands.Count, m_condition);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(DialogueChoice))
                return false;
            else
            {
                DialogueChoice choice = (DialogueChoice)obj;
                return this.Equals(choice);
            }
        }

        public override int GetHashCode()
        {
            return m_message.GetHashCode();
        }

        public bool Equals(DialogueChoice choice)
        {
            return choice.m_message == m_message & choice.m_nextId == m_nextId & choice.m_condition == m_condition & choice.m_commands == m_commands;
        }

        public string GetMessage(LuaContext context)
        {
            return ScriptTools.GetTextEmbedded(m_message, context);
        }
    }
}
