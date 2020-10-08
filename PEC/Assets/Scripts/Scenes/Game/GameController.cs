using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Race game controller.
 */
public class GameController : MonoBehaviour {

    /** Pause game screen */
    [SerializeField] private GameObject pauseCanvas = null;


    /**
     * Invoked when the script is loaded.
     */
    private void Awake() {
        Time.timeScale = 1.0f;
    }


    /**
     * Invoked on each frame update.
     */
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            TogglePause();
        }
    }


    /**
     * Shows the main menu scene.
     */
    public void LoadMainMenu() {
        SceneManager.LoadScene("Main");
    }


    /**
     * Reload the current scente.
     */
    public void RestartGame() {
        SceneManager.LoadScene("Game");
    }


    /**
     * Toggle game pause status.
     */
    public void TogglePause() {
        bool wasPaused = pauseCanvas.activeSelf;
        pauseCanvas.SetActive(wasPaused == false);
        Time.timeScale = wasPaused ? 1.0f : 0.0f;
    }
}
