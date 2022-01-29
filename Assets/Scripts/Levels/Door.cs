using Data;
using Player;
using UnityEngine;

namespace Levels {
	public class Door : MonoBehaviour {
		[SerializeField] private Item requiredKey;

		private void OnInteract(PlayerController character) {
			if (character.Inventory.HasItem(requiredKey)) {
				Destroy(gameObject);
			}
		}
	}
}