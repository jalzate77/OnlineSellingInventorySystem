using Caliburn.Micro;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace OnlineSellingInventorySystem.Models
{
    public class Item : PropertyChangedBase
    {
        public Item()
        {
            Id = Guid.NewGuid();
            Prices = new BindableCollection<ItemPrice>();
            PricesView = CollectionViewSource.GetDefaultView(Prices);

            Prices.CollectionChanged += Prices_CollectionChanged;

            PricesView.SortDescriptions.Add(new SortDescription("DateApplicable", ListSortDirection.Descending));

            var editableCollection = (IEditableCollectionView)PricesView;

            if (editableCollection != null)
            {
                editableCollection.NewItemPlaceholderPosition = NewItemPlaceholderPosition.AtBeginning;
            }
        }

        private void Prices_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach(ItemPrice item in e.NewItems)
                {
                    item.DateApplicable = DateTime.Now;
                    item.UpdateParentPrice = () => NotifyOfPropertyChange("CurrentPrice");
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public double GetPrice(DateTime dateApplicable)
        {
            if (Prices != null && Prices.Count > 0 && Prices.Count(i => i.DateApplicable <= dateApplicable) > 0)
            {
                DateTime maxDate = Prices.Where(i => i.DateApplicable <= dateApplicable).Max(i => i.DateApplicable);
                ItemPrice price = Prices.FirstOrDefault(i => i.DateApplicable == maxDate);
                return price.Amount;
            }

            return 0;
        }

        public void SetPrice(DateTime dateApplicable, double amount)
        {
            Prices.Add(new ItemPrice(dateApplicable, amount));
        }

        public Guid Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string SKU { get; set; }

        public double CurrentPrice
        {
            get
            {
                return GetPrice(DateTime.Now);
            }
        }

        public IObservableCollection<ItemPrice> Prices { get; set; }
        public ICollectionView PricesView { get; }
    }

    public class ItemPrice
    {
        private DateTime dateApplicable;
        private double amount;

        public ItemPrice(DateTime dateApplicable, double amount)
        {
            Id = Guid.NewGuid();
            DateApplicable = dateApplicable;
            Amount = amount;
        }

        public ItemPrice()
        {
            Id = Guid.NewGuid();
            DateApplicable = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime DateApplicable
        {
            get => dateApplicable;
            set
            {
                dateApplicable = value;
                UpdateParentPrice?.Invoke();
            }
        }

        public double Amount
        {
            get => amount;
            set
            {
                amount = value;
                UpdateParentPrice?.Invoke();
            }
        }

        [NotMapped]
        public System.Action UpdateParentPrice { get; set; }
    }

    public class Transaction
    {
        public IObservableCollection<Item> Items { get; set; }
    }
}
