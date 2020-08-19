using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using OnlineSellingInventorySystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;

namespace OnlineSellingInventorySystem.ViewModels
{
    public interface IShell
    {

    }

    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<Screen>
    {
        public void ActivateItemVM()
        {
            ActivateItem(ItemVM);
        }

        [Import(typeof(ItemViewModel))]
        public ItemViewModel ItemVM { get; set; }
    }
}
