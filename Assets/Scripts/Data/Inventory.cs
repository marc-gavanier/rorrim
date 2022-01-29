using System.Collections.Generic;
using UnityEngine;

namespace Data {
	[CreateAssetMenu(menuName = "Mirror/Inventory")]
	public class Inventory : ScriptableObject {
		private readonly List<Item> items = new List<Item>();

		public void AddItem(Item item) {
			items.Add(item);
			Debug.Log($"Added item {item.name} ({items.Count})");
		}

		public bool HasItem(Item item) {
			return items.Contains(item);
		}
	}
}