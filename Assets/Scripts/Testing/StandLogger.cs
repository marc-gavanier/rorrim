using UnityEngine;

namespace Testing {
	public class StandLogger : MonoBehaviour {
		private void OnDrawGizmos() {
			Gizmos.color = new Color(1f, 0.95f, 0.44f, 0.31f);
			Gizmos.DrawCube(transform.position, transform.localScale);
		}
		
		private void OnStand(GameObject emitter) {
			Debug.Log(emitter);
		}
	}
}