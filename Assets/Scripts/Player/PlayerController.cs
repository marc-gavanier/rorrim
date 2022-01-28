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

			moving = true;
			var target = transform.position + direction.ToVector3();
			StartCoroutine(ProcessMovement(target));
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