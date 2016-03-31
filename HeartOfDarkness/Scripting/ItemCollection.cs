using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartOfDarkness.Scripting
{
    public class ItemCollection
    {
        private Dictionary<Item, int> myCollections;

        public ItemCollection()
        {
            myCollections = new Dictionary<Item, int>();
        }

        public int GetNumItems()
        {
            return myCollections.Count;
        }

        public int GetItemCount(Item item)
        {
            if (myCollections.ContainsKey(item))
                return myCollections[item];
            else
                return 0;
        }

        public void AddItem(Item item, int count = 1)
        {
            if (!myCollections.ContainsKey(item))
                myCollections.Add(item, count);
            else
                myCollections[item] += count;
        }

        public void RemoveItem(Item item, int count = 1)
        {
            if (!myCollections.ContainsKey(item))
                myCollections.Add(item, -count);
            else
                myCollections[item] -= count;
        }
    }
}
