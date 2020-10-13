using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/**
 * Replay screen controller.
 */
public class ReplayController : MonoBehaviour {

    /** Ghost car object */
    [SerializeField] private GameObject ghost = null;


    /**
     * Invoked when the canvas is enabled.
     */
    private void OnEnable() {
        ghost.SetActive(false);
    }
}
