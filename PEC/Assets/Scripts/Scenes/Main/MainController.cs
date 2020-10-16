using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Shared.Models;


/**
 * Opening scene controller.
 */
public class MainController : MonoBehaviour {

    /** Specifies the available cars and circuits */
    [SerializeField] private RaceSetup options = null;

    /** Option picker button template */
    [SerializeField] private GameObject optionPrefab = null;

    /** Main menu canvas */
    [SerializeField] private GameObject menu = null;

    /** Circuit picker button group */
    [SerializeField] private Transform circuits = null;

    /** Car picker button group */
    [SerializeField] private Transform cars = null;


    /**
     * Initialization.
     */
    private void Start() {
        InitCarOptions();
        InitCircuitOptions();
        menu.SetActive(true);
    }


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


    /**
     * Initialize the car chooser menu.
     */
    private void InitCarOptions() {
        foreach (var car in options.availableCars) {
            GameObject o = Instantiate(optionPrefab, cars);
            o.GetComponentInChildren<Text>().text = car.name;
            o.GetComponentInChildren<Button>().onClick.AddListener(() => {
                OnCarChoosen(car.name);
            });
        }
    }


    /**
     * Initialize the circuit chooser menu.
     */
    private void InitCircuitOptions() {
        foreach (var circuit in options.availableCircuits) {
            GameObject o = Instantiate(optionPrefab, circuits);
            o.GetComponentInChildren<Text>().text = circuit.name;
            o.GetComponentInChildren<Button>().onClick.AddListener(() => {
                OnCircuitChoosen(circuit.name);
            });
        }
    }


    /**
     * Sets the choosen a car option as default.
     */
    private void OnCarChoosen(string name) {
        PlayerPrefs.SetString("CarSetupName", name);
        cars.parent.gameObject.SetActive(false);
        menu.SetActive(true);
    }


    /**
     * Sets the choosen a circuit option as default.
     */
    private void OnCircuitChoosen(string name) {
        PlayerPrefs.SetString("CircuitSetupName", name);
        circuits.parent.gameObject.SetActive(false);
        menu.SetActive(true);
    }
}
