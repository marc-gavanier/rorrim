using UnityEngine;

namespace Levels {
	public class Teleporter : MonoBehaviour {
		[SerializeField] private GameObject teleportedObject;
		[SerializeField] private Transform target;

		private void OnStand(GameObject obj) {
			if (obj == teleportedObject) {
				obj.transform.position = target.position;
			}
		}
	}
}