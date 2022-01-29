using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Camera
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class FollowCamera : MonoBehaviour {
		[SerializeField] private Transform target;
		[SerializeField] private bool reverseOffset;
		private new UnityEngine.Camera camera;


		// Start is called before the first frame update
		void Awake() {
			camera = GetComponent<UnityEngine.Camera>();
		}

		// Update is called once per frame
		void Update() {
			// camera.size * camera.ratio
			Debug.Log("Screen Width : " + Screen.width);
			Debug.unityLogger.Log(camera.pixelWidth);
			Debug.unityLogger.Log(camera.orthographicSize);
			Vector2 position = target.position;
			camera.transform.SetPositionAndRotation(new Vector3(position.x - (Screen.width / 251.38f) * (reverseOffset ? -1f : 1f), position.y, camera.transform.position.z), target.rotation);
		}
	}
}
