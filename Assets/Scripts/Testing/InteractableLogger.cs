using UnityEngine;

namespace Testing {
	public class InteractableLogger : MonoBehaviour {
		private void OnInteract(GameObject interactor) {
			Debug.Log($"Interact with {interactor.name}");
		}
	}
}