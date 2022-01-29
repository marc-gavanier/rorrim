using UnityEngine;

namespace Camera
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class FollowCamera : MonoBehaviour {
		[SerializeField] private Transform target;
		[SerializeField] public bool reverseOffset;
		[SerializeField] private UnityEngine.Camera mainCamera;
		private UnityEngine.Camera hostCamera;
		public UnityEngine.Camera HostCamera => hostCamera;


		// Start is called before the first frame update
		void Awake() {
			hostCamera = GetComponent<UnityEngine.Camera>();
		}

		// Update is called once per frame
		void Update() {
			Vector3 worldDimensions = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
			Vector2 position = target.position;
			hostCamera.transform.SetPositionAndRotation(new Vector3(position.x - (worldDimensions.x / 2f) * (reverseOffset ? -1f : 1f), position.y, hostCamera.transform.position.z), target.rotation);
		}

		public void ReverseOffset() {
			reverseOffset = true;
		}

		public void NoReverseOffset() {
			reverseOffset = false;
		}
	}
}
