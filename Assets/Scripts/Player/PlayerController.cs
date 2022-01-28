using System.Collections;
using UnityEngine;
using Utility;

namespace Player {
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float walkingSpeed = 4f;
		
		private bool moving;

		public bool Moving => moving;

		public void Move(Direction direction) {
			if (moving) return;
			
			var target = transform.position + direction.ToVector3();

			if (!CanWalkTo(target)) return;

			moving = true;
			StartCoroutine(ProcessMovement(target));
		}

		private bool CanWalkTo(Vector3 target) {
			var coll = Physics2D.OverlapPoint(target);

			return coll == null || coll.isTrigger;
		}

		private IEnumerator ProcessMovement(Vector3 target) {
			while (Vector3.Distance(transform.position, target) > 0.001f) {
				var distance = Time.deltaTime * walkingSpeed;
				transform.position = Vector3.MoveTowards(transform.position, target, distance);

				yield return new WaitForEndOfFrame();
			}

			transform.position = target;
			moving = false;
		}
	}
}