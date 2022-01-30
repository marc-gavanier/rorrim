using System.Runtime.InteropServices;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Game {
	public class Level : MonoBehaviour {
		[SerializeField] private string nextLevel;
		
		private bool blackOut;
		private bool whiteOut;

		public void Leave(PlayerController character) {
			if (character.Color == PlayerColor.Black) {
				blackOut = true;
			}
			else {
				whiteOut = true;
			}
			
			character.gameObject.SetActive(false);

			if (blackOut && whiteOut) {
				SceneManager.LoadScene(nextLevel);
			}
		}
	}
}