using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Shared.Controllers {

    /**
     * Pause screen controller.
     */
    public class PauseController : MonoBehaviour {

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
            Time.timeScale = canvas.enabled ? 1.0f : 0.0f;
            AudioListener.pause = (canvas.enabled == false);
            canvas.enabled = (canvas.enabled == false);
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
