using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartOfDarkness.Scripting
{
    /// <summary>
    /// Represents a collection of item instances
    /// </summary>
    public class ItemCollection
    {
        /// <summary>
        /// Stores the internal collection
        /// </summary>
        private Dictionary<string, int> myCollections;

        /// <summary>
        /// Creates a new instance of an item collection
        /// </summary>
        public ItemCollection()
        {
            myCollections = new Dictionary<string, int>();
        }

        /// <summary>
        /// Gets the number of item types in this item collection
        /// </summary>
        /// <returns>The number of items in the collection</returns>
        public int GetNumItems()
        {
            return myCollections.Count;
        }

        /// <summary>
        /// Checks whether this collection contains a given item
        /// </summary>
        /// <param name="internalItemName">The internal name of the item to search for</param>
        /// <returns>True if this collection contains the item, false if otherwise</returns>
        public bool HasItem(string internalItemName)
        {
            return myCollections.ContainsKey(internalItemName);
        }

        /// <summary>
        /// Checks whether this collection contains a given item
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>True if this collection contains the item, false if otherwise</returns>
        public bool HasItem(Item item)
        {
            return HasItem(item.InternalName);
        }

        /// <summary>
        /// Gets the number of items of a specific type in this collection
        /// </summary>
        /// <param name="item">The item to search for</param>
        /// <returns>The number of the given item in this collection</returns>
        public int GetItemCount(Item item)
        {
            return GetItemCount(item.InternalName);
        }

        /// <summary>
        /// Gets the number of items of a specific type in this collection
        /// </summary>
        /// <param name="internalName">The internal item name to search for</param>
        /// <returns>The number of the given item in this collection</returns>
        public int GetItemCount(string internalName)
        {
            if (myCollections.ContainsKey(internalName))
                return myCollections[internalName];
            else
                return 0;
        }

        /// <summary>
        /// Adds a number of a given item type to this collection
        /// </summary>
        /// <param name="item">The item type to add to the collection</param>
        /// <param name="count">The number of items to add</param>
        public void AddItem(Item item, int count = 1)
        {
            AddItem(item.InternalName, count);
        }

        /// <summary>
        /// Adds a number of a given item type to this collection
        /// </summary>
        /// <param name="internalName">The internal item name to add to the collection</param>
        /// <param name="count">The number of items to add</param>
        public void AddItem(string internalName, int count = 1)
        {
            if (!myCollections.ContainsKey(internalName))
                myCollections.Add(internalName, count);
            else
                myCollections[internalName] += count;
        }

        /// <summary>
        /// Removes a number of a given item type from this collection
        /// </summary>
        /// <param name="item">The item type to remove from the collection</param>
        /// <param name="count">The number of items to remove</param>
        public void RemoveItem(Item item, int count = 1)
        {
            RemoveItem(item.InternalName, count);
        }

        /// <summary>
        /// Removes a number of a given item type from this collection
        /// </summary>
        /// <param name="internalName">The internal item name to remove from the collection</param>
        /// <param name="count">The number of items to remove</param>
        public void RemoveItem(string internalName, int count = -1)
        {
            if (!myCollections.ContainsKey(internalName))
                myCollections.Add(internalName, -count);
            else
                myCollections[internalName] -= count;
        }
    }
}
