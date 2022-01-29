using System.Collections;
using UnityEngine;
using Utility;

namespace Player {
	[RequireComponent(typeof(PlayerAnimator))]
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float walkingSpeed = 4f;
		[SerializeField] private LayerMask movableLayers;
		[SerializeField] private LayerMask standableLayers;
		[SerializeField] private LayerMask interactableLayers;

		private PlayerAnimator animator;
		private Direction direction;
		private bool moving;

		public bool Moving => moving;

		public void Move(Direction moveDirection) {
			if (moving) return;

			direction = moveDirection;
			animator.SetDirection(direction);
			GameObject movable;

			if (!CanWalkTo(direction, out movable)) return;

			moving = true;
			StartCoroutine(ProcessMovement(direction, movable));
		}

		public void Interact() {
			var target = transform.position + direction.ToVector3();
			var coll = Physics2D.OverlapPoint(target, interactableLayers);

			if (coll != null) {
				coll.SendMessage("OnInteract", gameObject);
			}
		}

		private void Awake() {
			animator = GetComponent<PlayerAnimator>();
			direction = Direction.Down;
			animator.SetDirection(direction);
		}

		private bool CanWalkTo(Direction direction, out GameObject movable) {
			movable = null;
			var directionVector = direction.ToVector3();
			var target = transform.position + directionVector;
			var colliders = Physics2D.OverlapPointAll(target);

			foreach (var coll in colliders) {
				if (coll.isTrigger) continue;

				if ((movableLayers.value & (1 << coll.gameObject.layer)) == 0) return false;

				movable = coll.gameObject;
				var collTarget = target + directionVector;
				var otherColl = Physics2D.OverlapPoint(collTarget);

				return otherColl == null || otherColl.isTrigger;
			}

			return true;
		}

		private void EmitStand(Vector3 position, GameObject emitter) {
			var coll = Physics2D.OverlapPoint(position, standableLayers);

			if (coll != null) {
				coll.SendMessage("OnStand", emitter, SendMessageOptions.DontRequireReceiver);
			}
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
					movable.transform.position =
						Vector3.MoveTowards(movable.transform.position, movableTarget, distance);
				}

				yield return new WaitForEndOfFrame();
			}

			transform.position = target;
			EmitStand(target, gameObject);

			if (movable != null) {
				movable.transform.position = movableTarget;
				EmitStand(movableTarget, movable);
			}

			moving = false;

			yield return new WaitForEndOfFrame();

			if (!moving) {
				animator.SetMoving(false);
			}
		}
	}
}