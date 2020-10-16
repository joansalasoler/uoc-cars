using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Shared.Controllers {

    /**
     * Pause screen controller.
     */
    public class PauseController : MonoBehaviour {

        [SerializeField] private GameObject overlay = null;

        /**
         * Shows the main menu scene.
         */
        public void LoadMainMenu() {
            AudioListener.pause = false;
            SceneManager.LoadScene("Main");
        }


        /**
         * Reload the current scente.
         */
        public void RestartGame() {
            AudioListener.pause = false;
            SceneManager.LoadScene("Race");
        }


        /**
         * Toggle game pause status.
         */
        public void TogglePause() {
            Canvas canvas = GetComponent<Canvas>();
            Time.timeScale = overlay.activeSelf ? 1.0f : 0.0f;
            AudioListener.pause = (overlay.activeSelf == false);
            overlay.SetActive(overlay.activeSelf == false);
        }


        /**
         * Invoked on each frame update.
         */
        private void Update() {
            if (Input.GetButtonUp("Cancel")) {
                TogglePause();
            }
        }
    }
}
