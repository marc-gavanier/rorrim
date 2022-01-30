using Game;
using Player;
using UnityEngine;
using Utility;

namespace Levels {
	public class LevelDoor : MonoBehaviour {
		[SerializeField] private Level level;
		
		private void OnDrawGizmos() {
			Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
			Gizmos.DrawCube(transform.position, transform.lossyScale);
		}

		private void OnStand(GameObject obj) {
			if (!obj.CompareTag("Player")) return;

			level.Leave(obj.GetComponent<PlayerController>());
		}
	}
}