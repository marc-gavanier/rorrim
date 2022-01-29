using UnityEngine;

namespace Levels {
	public class MovementBinder : MonoBehaviour {
		[SerializeField] private Transform boundObject;
		[SerializeField] private bool inverted;

		private bool movementAllowed;

		private void OnMove(Vector3 movement) {
			if (movementAllowed) {
				boundObject.position += inverted ? -movement : movement;
			}
		}

		private void OnMovementStart(Vector3 direction) {
			movementAllowed = true;
			var colliders = Physics2D.OverlapPointAll(boundObject.position + direction);

			foreach (var coll in colliders) {
				if (coll.isTrigger) continue;
				movementAllowed = false;

				return;
			}
		}
	}
}