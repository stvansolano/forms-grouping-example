using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormsIssueProject.Models;

namespace FormsIssueProject.Services
{
	public class MockDataStore : IDataStore<Item>
	{
		List<Item> items;

		public MockDataStore()
		{
			items = new List<Item>();
			var mockItems = new List<Item>
			{
				new Item { Id = Guid.NewGuid().ToString(), Category = "Category 1", Text = "First item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Category = "Category 1", Text = "Second item", Description="an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Category = "Category 2 with a very very very long text", Text = "Fourth item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Category = "This is category 3 with a very very long text", Text = "Fourth item", Description="This is an item description." },
				new Item { Id = Guid.NewGuid().ToString(), Category = "Category 3 with a very long text", Text = "Fifth item", Description="This unique description." },
				new Item { Id = Guid.NewGuid().ToString(), Category = "Category 4", Text = "Sixth item", Description="This is a short text." },
			};

			foreach (var item in mockItems)
			{
				items.Add(item);
			}
		}

		public async Task<bool> AddItemAsync(Item item)
		{
			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(Item item)
		{
			var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(oldItem);
			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(string id)
		{
			var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
			items.Remove(oldItem);

			return await Task.FromResult(true);
		}

		public async Task<Item> GetItemAsync(string id)
		{
			return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(items);
		}
	}
}