using Player;
using UnityEngine;

namespace Testing {
	public class InteractableLogger : MonoBehaviour {
		private void OnInteract(PlayerController character) {
			Debug.Log($"Interact with {character.name}");
		}
	}
}