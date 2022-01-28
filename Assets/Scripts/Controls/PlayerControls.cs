using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace Controls {
	public class PlayerControls : MonoBehaviour {
		[SerializeField] private PlayerController player;
		[SerializeField] private PlayerController mirroredPlayer;
		
		private Direction direction = Direction.None;
		
		public void OnMove(InputValue value) {
			var input = value.Get<Vector2>();
			direction = input.ToDirection();
		}

		private void Update() {
			if (direction == Direction.None || player.Moving || mirroredPlayer.Moving) return;
			
			player.Move(direction);
			mirroredPlayer.Move(direction.Opposite());
		}
	}
}