using Camera;
using UnityEngine;

public class CameraControls : MonoBehaviour {
	[SerializeField] private FollowCamera whiteCamera;
	[SerializeField] private FollowCamera blackCamera;

	private bool whiteCameraIsLeft = false;

	void OnSwitchCamera() {
		whiteCameraIsLeft = !whiteCameraIsLeft;

		if (whiteCameraIsLeft) {
			SwitchCamera(whiteCamera, blackCamera);
		}
		else {
			SwitchCamera(blackCamera, whiteCamera);
		}
	}

	void SwitchCamera(FollowCamera leftCamera, FollowCamera rightCamera) {
		leftCamera.HostCamera.rect = new Rect(0, 0, .5f, 1);
		leftCamera.NoReverseOffset();
		rightCamera.HostCamera.rect = new Rect(.5f, 0, .5f, 1);
		rightCamera.ReverseOffset();
	}
}
