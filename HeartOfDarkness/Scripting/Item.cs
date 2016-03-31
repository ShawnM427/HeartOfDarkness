using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartOfDarkness.Scripting
{
    /// <summary>
    /// Represents an item that can be picked up and stored in an inventory
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Stores the item's name
        /// </summary>
        private string myName;
        /// <summary>
        /// Stores the item's description
        /// </summary>
        private string myDescription;
        /// <summary>
        /// Stores the script to be invoked when the item is used
        /// </summary>
        private string myUsedScript;
        /// <summary>
        /// Stores the script to be invoked when the item is picked up
        /// </summary>
        private string myPickedUpScript;

        /// <summary>
        /// Gets or sets the item's name
        /// </summary>
        public string Name
        {
            get { return myName; }
            set { myName = value; }
        }
        /// <summary>
        /// Gets or sets the item's description
        /// </summary>
        public string Description
        {
            get { return myDescription; }
            set { myDescription = value; }
        }
        /// <summary>
        /// Gets or sets the lua script to be called when the item is used
        /// </summary>
        public string UsedScript
        {
            get { return myUsedScript; }
            set { myUsedScript = value; }
        }
        /// <summary>
        /// Gets or sets the lua script to be called when the item is picked up
        /// </summary>
        public string PickedUpScript
        {
            get { return myPickedUpScript; }
            set { myPickedUpScript = value; }
        }
        
        /// <summary>
        /// Creates a new instance of an item
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="description">The item's description</param>
        /// <param name="usedScript">The script to invoke when the item is used</param>
        /// <param name="pickedUpScript">The script to invoke when the item is picked up</param>
        public Item(string name, string description, string usedScript = null, string pickedUpScript = null)
        {
            myName = name;
            myDescription = description;
            myUsedScript = usedScript;
            myPickedUpScript = pickedUpScript;
        }

        /// <summary>
        /// Invokes the items used script with the given context
        /// </summary>
        /// <param name="context">The context to run the item's script against</param>
        public void OnUsed(LuaContext context)
        {
            if (myPickedUpScript != null)
                context.DoString(myUsedScript);
        }

        /// <summary>
        /// Invokes the items picked up script with the given context
        /// </summary>
        /// <param name="context">The context to run the item's script against</param>
        public void OnPickedUp(LuaContext context)
        {
            if (myPickedUpScript != null)
                context.DoString(myPickedUpScript);
        }

        /// <summary>
        /// Checks if this item is equal to another object
        /// </summary>
        /// <param name="obj">The object the check eqaulity against</param>
        /// <returns>True if the objects are equal, false if otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj is Item)
            {
                Item other = obj as Item;

                return (
                    other.Name.Equals(myName) && 
                    other.Description.Equals(myDescription) && 
                    other.UsedScript.Equals(myUsedScript) && 
                    other.PickedUpScript.Equals(myPickedUpScript)
                    );
            }
            else
                return false;
        }

        /// <summary>
        /// Gets a semi-unique hash code for this item
        /// </summary>
        public override int GetHashCode()
        {
            return myName.GetHashCode() & myDescription.GetHashCode() + myUsedScript.GetHashCode();
        }
    }
}
