using System;
using System.Collections;
using System.Reflection;

namespace AbrAfzarGostaran
{
    public class LinearListItems : CollectionBase
    {
        private WinFormsLinearList owner;

        public LinearListItem this[int index]
        {
            get
            {
                return (LinearListItem)base.InnerList[index];
            }
        }

        public WinFormsLinearList Owner
        {
            get
            {
                return this.owner;
            }
        }

        public LinearListItems()
        {
        }

        public LinearListItems(WinFormsLinearList abrlist)
        {
            this.owner = abrlist;
        }

        public LinearListItem Add(string item)
        {
            LinearListItem linearListItem = new LinearListItem(this.owner)
            {
                Text = item
            };
            base.InnerList.Add(linearListItem);
            return linearListItem;
        }

        public int IndexOf(object o)
        {
            return base.InnerList.IndexOf(o);
        }
    }
}