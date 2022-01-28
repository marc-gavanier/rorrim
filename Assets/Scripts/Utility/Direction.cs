using UnityEngine;

namespace Utility {
	public enum Direction {
		None, Up, Down, Left, Right
	}

	public static class DirectionExtension {
		private static Vector3[] vectors = {
			Vector3.zero, Vector3.up, Vector3.down, Vector3.left, Vector3.right
		};

		public static Direction ToDirection(this Vector2 vector) {
			if (vector.x < -0.001f) return Direction.Left;
			if (vector.x > 0.001f) return Direction.Right;
			if (vector.y < -0.001f) return Direction.Down;
			if (vector.y > 0.001f) return Direction.Up;

			return Direction.None;
		}

		public static Vector3 ToVector3(this Direction direction) {
			return vectors[(int)direction];
		}
	}
}