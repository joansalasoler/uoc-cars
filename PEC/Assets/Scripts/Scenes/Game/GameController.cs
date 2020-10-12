using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using Shared;
using Shared.Controllers;
using Shared.Timers;

/**
 * Race game controller.
 */
public class GameController : MonoBehaviour {

    /** Number of laps to complete on a race */
    [SerializeField] private int raceLaps = 3;

    /** Player's car controller */
    [SerializeField] private CarController car = null;

    /** Race circuit controller */
    [SerializeField] private CircuitController circuit = null;

    /** Head up display controller */
    [SerializeField] private HeadupController headup = null;

    /** Time spent on the whole race */
    private StopWatch raceTimer = new StopWatch();

    /** Time spent on the current lap */
    private StopWatch lapTimer = new StopWatch();

    /** Number of completed laps */
    private int lapCount = 0;


    /**
     * Invoked when the script is enabled.
     */
    private void Start() {
        Time.timeScale = 1.0f;
        circuit.MoveToPole(car.GetComponent<Transform>());
        InvokeRepeating("RefreshHeadupDisplay", 0.1f, 0.1f);
        raceTimer.Restart();
        lapTimer.Restart();
    }


    /**
     * Update the headup display information.
     */
    private void RefreshHeadupDisplay() {
        headup.ShowLapTime(lapTimer.GetTimeSpan());
        headup.ShowRaceTime(raceTimer.GetTimeSpan());
        headup.ShowCarSpeed(car.GetComponent<Rigidbody>());
    }


    /**
     * Disables the user controller of a car.
     */
    private void BlockCarControls(CarController car) {
        car.GetComponent<CarUserControl>().enabled = false;
        car.Move(0.0f, 0.0f, 0.0f, 1.0f);
    }


    /**
     * Invoked each time a lap is completed on the circuit.
     */
    private void OnCircuitCompletion() {
        headup.PushTimeMark(lapTimer.GetTimeSpan());

        if (++lapCount >= raceLaps) {
            BlockCarControls(car);
            raceTimer.Stop();
            lapTimer.Stop();
        } else {
            lapTimer.Restart();
        }
    }
}
