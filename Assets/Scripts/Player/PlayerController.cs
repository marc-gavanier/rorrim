using System.Collections;
using UnityEngine;
using Utility;

namespace Player {
	[RequireComponent(typeof(PlayerAnimator))]
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float walkingSpeed = 4f;

		private PlayerAnimator animator;
		private bool moving;

		public bool Moving => moving;

		public void Move(Direction direction) {
			if (moving) return;
			
			var target = transform.position + direction.ToVector3();

			if (!CanWalkTo(target)) return;

			moving = true;
			animator.SetDirection(direction);
			StartCoroutine(ProcessMovement(target));
		}

		private void Awake() {
			animator = GetComponent<PlayerAnimator>();
		}

		private bool CanWalkTo(Vector3 target) {
			var coll = Physics2D.OverlapPoint(target);

			return coll == null || coll.isTrigger;
		}

		private IEnumerator ProcessMovement(Vector3 target) {
			animator.SetMoving(true);
			
			while (Vector3.Distance(transform.position, target) > 0.001f) {
				var distance = Time.deltaTime * walkingSpeed;
				transform.position = Vector3.MoveTowards(transform.position, target, distance);

				yield return new WaitForEndOfFrame();
			}

			transform.position = target;
			moving = false;

			yield return new WaitForEndOfFrame();

			if (!moving) {
				animator.SetMoving(false);
			}
		}
	}
}