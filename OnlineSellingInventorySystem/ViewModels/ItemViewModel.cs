using Caliburn.Micro;
using OnlineSellingInventorySystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;

namespace OnlineSellingInventorySystem.ViewModels
{
    [Export(typeof(ItemViewModel))]
    public class ItemViewModel : Conductor<Item>.Collection.OneActive
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            DisplayName = "Item Management";

            New();
        }

        public void Populate()
        {

        }

        public void New()
        {
            ActiveItem = new Item();
        }

        public void ReloadButton()
        {

        }

        public void SaveButton()
        {

        }

        public void AddButton()
        {

        }
    }
}
