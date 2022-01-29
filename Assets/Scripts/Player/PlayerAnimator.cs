using UnityEngine;
using Utility;

namespace Player {
	[RequireComponent(typeof(Animator))]
	public class PlayerAnimator : MonoBehaviour {
		private static readonly int HorizontalFloat = Animator.StringToHash("Horizontal");
		private static readonly int MovingBool = Animator.StringToHash("Moving");
		private static readonly int VerticalFloat = Animator.StringToHash("Vertical");

		private Animator animator;

		public void SetDirection(Direction direction) {
			if (direction == Direction.None) return;
			
			var vector = direction.ToVector3();
			animator.SetFloat(HorizontalFloat, vector.x);
			animator.SetFloat(VerticalFloat, vector.y);
		}

		public void SetMoving(bool moving) {
			animator.SetBool(MovingBool, moving);
		}
		
		private void Awake() {
			animator = GetComponent<Animator>();
		}
	}
}