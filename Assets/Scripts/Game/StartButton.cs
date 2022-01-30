using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
	public class StartButton : MonoBehaviour {
		[SerializeField] private SceneAsset startLevel;

		public void StartGame() {
			SceneManager.LoadScene(startLevel.name);
		}
	}
}