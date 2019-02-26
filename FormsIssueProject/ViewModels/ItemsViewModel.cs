using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using FormsIssueProject.Models;
using FormsIssueProject.Views;
using System.Collections.Generic;
using System.Windows.Input;

namespace FormsIssueProject.ViewModels
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableCollection<Grouping<string, Item>> Items { get; set; }
		public Command LoadItemsCommand { get; set; }

		public ItemsViewModel()
		{
			Title = "Browse";
			Items = new ObservableCollection<Grouping<string, Item>>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
			{
				var newItem = item as Item;
				//Items.Add(newItem);
				//await DataStore.AddItemAsync(newItem);
			});
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();

				await Task.Delay(2000);

				//var items = await DataStore.GetItemsAsync(true);

				var mockItems = new List<Item>
				{
					new Item { Id = Guid.NewGuid().ToString(), Category = "Category 1", Text = "First item", Description="This is an item description." },
					new Item { Id = Guid.NewGuid().ToString(), Category = "Category 1", Text = "Second item", Description="an item description." },
					new Item { Id = Guid.NewGuid().ToString(), Category = "Category 2 with a very very very long text", Text = "Fourth item", Description="This is an item description." },
					new Item { Id = Guid.NewGuid().ToString(), Category = "This is category 3 with a very very long text", Text = "Fourth item", Description="This is an item description." },
					new Item { Id = Guid.NewGuid().ToString(), Category = "Category 3 with a very long text", Text = "Fifth item", Description="This unique description." },
					new Item { Id = Guid.NewGuid().ToString(), Category = "Category 4", Text = "Sixth item", Description="This is a short text." },
				};

				var grouped = (from e in mockItems
							   group e by e.Category into g
							   select new Grouping<string, Item>(g.Key, g));

				foreach (var item in grouped)
				{
					Items.Add(item);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		public class Grouping
			<K, T> : ObservableCollection<T>
		{
			public Grouping(K key, System.Collections.Generic.IEnumerable<T> items)
			{
				Key = key;

				foreach (var item in items)
					this.Items.Add(item);
			}

			public K Key { get; private set; }
		}
	}
}