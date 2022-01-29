using Data;
using Player;
using UnityEngine;

namespace Levels {
	public class Collectible : MonoBehaviour {
		[SerializeField] private Item item;
		
		private void OnInteract(PlayerController character) {
			Destroy(gameObject);
			
			character.Inventory.AddItem(item);
		}
	}
}