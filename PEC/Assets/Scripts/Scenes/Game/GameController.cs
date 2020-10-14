using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using Shared;
using Shared.Controllers;
using Shared.Models;
using Shared.Timers;


/**
 * Race game logic controller.
 */
public class GameController : MonoBehaviour {

    /** Number of laps to complete on a race */
    [SerializeField] private int laps = 3;

    /** Player's car controller */
    [SerializeField] private CarController car = null;

    /** Race circuit controller */
    [SerializeField] private CircuitController circuit = null;

    /** Head up display controller */
    [SerializeField] private HeadupController headup = null;

    /** Head up display controller */
    [SerializeField] private ReplayController replay = null;

    /** Recorder of the current race */
    [SerializeField] private GameRecorder recorder = null;

    /** Replayer of the best race as a ghost car */
    [SerializeField] private GameReplayer replayer = null;

    /** Chronometer for the time spent on the race */
    private StopWatch stopWatch = new StopWatch();

    /** Number of completed laps */
    private int lapCount = 0;


    /**
     * Invoked when the script is enabled.
     */
    private void Start() {
        Time.timeScale = 1.0f;
        InvokeRepeating("RefreshHeadupDisplay", 0.1f, 0.1f);
        circuit.MoveToPole(car.GetComponent<Transform>());
        stopWatch.Restart();
        LoadBestRaceRecording();
        StartReplayer();
        StartRecorder();
    }


    /**
     * Update the headup display information.
     */
    private void RefreshHeadupDisplay() {
        headup.ShowRaceTime(stopWatch.GetTimeSpan());
        headup.ShowLapTime(stopWatch.GetMarkTimeSpan());
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
     * If the last lap was completed.
     */
    private bool RaceHasFinished() {
        return this.lapCount >= this.laps;
    }


    /**
     * Increase the numbre of laps completed.
     */
    private int IncreseLapCount() {
        return ++this.lapCount;
    }


    /**
     * Starts the ghost replayer.
     */
    private void StartReplayer() {
        if (replayer.GetRecording().Count > 0) {
            replayer.SetActive(true);
        }
    }


    /**
     * Starts the game recorder.
     */
    private void StartRecorder() {
        recorder.SetActive(true);
    }


    /**
     * Stops the game recorder.
     */
    private void StopRecorder() {
        recorder.SetActive(false);
        UpdateBestRaceRecording();
    }


    /**
     * Loads the best race recording from player prefs.
     */
    private void LoadBestRaceRecording() {
        if (PlayerPrefs.HasKey("BestRace")) {
            Recording recording = replayer.GetRecording();
            string json = PlayerPrefs.GetString("BestRaceRecording");
            JsonUtility.FromJsonOverwrite(json, recording);
        }
    }


    /**
     * Populates the best race recording with the current game
     * recording if the time is better than what was stored.
     */
    private void UpdateBestRaceRecording() {
        Recording best = replayer.GetRecording();
        Recording last = recorder.GetRecording();

        if (best.Count == 0 || best.Duration > last.Duration) {
            string json = JsonUtility.ToJson(last);
            PlayerPrefs.SetString("BestRaceRecording", json);
            best.Copy(last);
        }
    }


    /**
     * Invoked each time a lap is completed on the circuit.
     */
    private void OnCircuitCompletion() {
        headup.PushTimeMark(stopWatch.GetMarkTimeSpan());
        this.IncreseLapCount();

        if (this.RaceHasFinished()) {
            this.BlockCarControls(car);
            this.Invoke("OnRaceFinished", 3.0f);
            stopWatch.Stop();
        } else {
            StartReplayer();
            stopWatch.Mark();
        }
    }


    /**
     * Invoked when the race finishes.
     */
    private void OnRaceFinished() {
        StopRecorder();
        headup.gameObject.SetActive(false);
        replay.gameObject.SetActive(true);
    }
}
