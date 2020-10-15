using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Opening scene controller.
 */
public class MainController : MonoBehaviour {

    /**
     * Start a new race.
     */
    public void StartNewGame() {
        SceneManager.LoadScene("Race");
    }


    /**
     * Exit the game.
     */
    public void QuitApplication() {
        Application.Quit();
    }
}
