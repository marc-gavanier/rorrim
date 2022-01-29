using UnityEngine;


namespace Camera
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class FollowCamera : MonoBehaviour {
		[SerializeField] private Transform target;
		[SerializeField] private bool reverseOffset;
		[SerializeField] private UnityEngine.Camera mainCamera;
		private new UnityEngine.Camera camera;


		// Start is called before the first frame update
		void Awake() {
			camera = GetComponent<UnityEngine.Camera>();
		}

		// Update is called once per frame
		void Update() {
			Vector3 worldDimensions = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
			Vector2 position = target.position;
			camera.transform.SetPositionAndRotation(new Vector3(position.x - (worldDimensions.x / 2f) * (reverseOffset ? -1f : 1f), position.y, camera.transform.position.z), target.rotation);
		}
	}
}
