using System.Collections;
using UnityEngine;
using Utility;

namespace Player {
	[RequireComponent(typeof(PlayerAnimator))]
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float walkingSpeed = 4f;
		[SerializeField] private LayerMask movableLayers;

		private PlayerAnimator animator;
		private bool moving;

		public bool Moving => moving;

		public void Move(Direction direction) {
			if (moving) return;
			
			animator.SetDirection(direction);
			GameObject movable;

			if (!CanWalkTo(direction, out movable)) return;

			moving = true;
			StartCoroutine(ProcessMovement(direction, movable));
		}

		private void Awake() {
			animator = GetComponent<PlayerAnimator>();
		}

		private bool CanWalkTo(Direction direction, out GameObject movable) {
			movable = null;
			var directionVector = direction.ToVector3();
			var target = transform.position + directionVector;
			var coll = Physics2D.OverlapPoint(target);

			if (coll == null || coll.isTrigger) return true;

			if ((movableLayers.value & (1 << coll.gameObject.layer)) == 0) return false;

			movable = coll.gameObject;
			var collTarget = target + directionVector;
			var otherColl = Physics2D.OverlapPoint(collTarget);

			return otherColl == null || otherColl.isTrigger;
		}

		private IEnumerator ProcessMovement(Direction direction, GameObject movable) {
			animator.SetMoving(true);
			var directionVector = direction.ToVector3();
			var target = transform.position + directionVector;
			var movableTarget = target + directionVector;

			while (Vector3.Distance(transform.position, target) > 0.001f) {
				var distance = Time.deltaTime * walkingSpeed;
				transform.position = Vector3.MoveTowards(transform.position, target, distance);

				if (movable != null) {
					movable.transform.position = Vector3.MoveTowards(movable.transform.position, movableTarget, distance);
				}

				yield return new WaitForEndOfFrame();
			}

			transform.position = target;

			if (movable != null) {
				movable.transform.position = movableTarget;
			}
			
			moving = false;

			yield return new WaitForEndOfFrame();

			if (!moving) {
				animator.SetMoving(false);
			}
		}
	}
}