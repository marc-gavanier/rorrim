using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
	public class StartButton : MonoBehaviour {
		[SerializeField] private string startLevel;

		public void StartGame() {
			SceneManager.LoadScene(startLevel);
		}
	}
}