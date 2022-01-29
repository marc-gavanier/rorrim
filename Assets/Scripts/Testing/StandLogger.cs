using UnityEngine;

namespace Testing {
	public class StandLogger : MonoBehaviour {
		private void OnStand(GameObject emitter) {
			Debug.Log($"{name} stands");
		}
	}
}