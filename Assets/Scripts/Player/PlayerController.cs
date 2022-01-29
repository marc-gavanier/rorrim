using System.Collections;
using Data;
using UnityEngine;
using Utility;

namespace Player {
	[RequireComponent(typeof(PlayerAnimator))]
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float walkingSpeed = 4f;
		[SerializeField] private Transform destinationCollider;
		[SerializeField] private LayerMask movableLayers;
		[SerializeField] private LayerMask standableLayers;
		[SerializeField] private LayerMask interactableLayers;
		[SerializeField] private Inventory inventory;

		private PlayerAnimator animator;
		private Direction direction;
		private bool moving;

		public Inventory Inventory => inventory;
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
				coll.SendMessage("OnInteract", this);
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

		private IEnumerator ProcessMovement(Direction moveDirection, GameObject movable) {
			animator.SetMoving(true);
			var directionVector = moveDirection.ToVector3();
			var target = transform.position + directionVector;
			var movableTarget = target + directionVector;
			var remainingDistance = 1f;
			destinationCollider.position = target;

			yield return null;
			yield return null;

			if (movable != null) {
				movable.SendMessage("OnMovementStart", directionVector);
			}

			while (Vector3.Distance(transform.position, target) > 0.001f) {
				var distance = Mathf.Min(Time.deltaTime * walkingSpeed, remainingDistance);
				remainingDistance -= distance;
				var movement = directionVector * distance;
				transform.position += movement;
				destinationCollider.position = target;

				if (movable != null) {
					movable.transform.position += movement;
					movable.SendMessage("OnMove", movement);
				}

				yield return null;
			}

			transform.position = destinationCollider.position = target;
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