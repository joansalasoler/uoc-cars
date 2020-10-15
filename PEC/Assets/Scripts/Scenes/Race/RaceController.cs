using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;
using Shared;
using Shared.Behaviours;
using Shared.Controllers;
using Shared.Models;
using Shared.Services;
using Shared.Timers;


/**
 * Race game logic controller.
 */
public class RaceController : HasRaceComponents {

    [Header("Interface Controllers")]

    /** Head up display controller */
    [SerializeField] private HeadupController headup = null;

    [Header("Race Recordings")]

    /** Recorder of the current race */
    [SerializeField] private GameRecorder recorder = null;

    /** Replayer of the best race as a ghost car */
    [SerializeField] private GameReplayer replayer = null;

    [Header("Race Settings")]

    /** Number of laps to complete on a race */
    [SerializeField] private int numberOfLaps = 3;

    /** Chronometer for the time spent on the race */
    private StopWatch stopWatch = new StopWatch();

    /** Number of completed laps */
    private int currentLapCount = 0;


    /**
     * Invoked when the script is enabled.
     */
    protected override void Start() {
        base.Start();

        Time.timeScale = 1.0f;
        InvokeRepeating("RefreshHeadupDisplay", 0.1f, 0.1f);

        circuit.onCompletion.AddListener(OnCircuitCompletion);
        circuit.MoveToPole(car.GetComponent<Transform>());
        ghost.gameObject.SetActive(false);

        SetupGameRecordings();
        LoadBestRaceRecording();

        stopWatch.Restart();
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
        return this.currentLapCount >= this.numberOfLaps;
    }


    /**
     * Increase the numbre of laps completed.
     */
    private int IncreaseCurrentLapCount() {
        return ++this.currentLapCount;
    }


    /**
     * Starts the ghost replayer.
     */
    private void StartReplayer() {
        if (replayer.GetRecording().Count > 0) {
            ghost.gameObject.SetActive(true);
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
     * Best race key name on the player preferences.
     */
    private string GetBestRacePrefsKey() {
        return $"BestRecording[{ circuit.GetId() }]";
    }


    /**
     * Initialize the game recorders and replayers.
     */
    private void SetupGameRecordings() {
        var carTargets = car.GetComponent<GameRecorderTargets>();
        var ghostTargets = ghost.GetComponent<GameRecorderTargets>();
        var circuitSlots = circuit.GetComponent<GameRecorderSlots>();

        recorder.SetTargets(carTargets.targets);
        replayer.SetTargets(ghostTargets.targets);
        recorder.SetRecording(circuitSlots.lastRace);
        replayer.SetRecording(circuitSlots.bestRace);
    }


    /**
     * Loads the best race recording from player prefs.
     */
    private void LoadBestRaceRecording() {
        if (PlayerPrefs.HasKey(GetBestRacePrefsKey())) {
            Recording recording = replayer.GetRecording();
            string json = PlayerPrefs.GetString(GetBestRacePrefsKey());
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
            PlayerPrefs.SetString(GetBestRacePrefsKey(), json);
            best.Copy(last);
        }
    }


    /**
     * Invoked each time a lap is completed on the circuit.
     */
    private void OnCircuitCompletion() {
        headup.PushTimeMark(stopWatch.GetMarkTimeSpan());
        this.IncreaseCurrentLapCount();

        if (this.RaceHasFinished()) {
            this.BlockCarControls(car);
            this.Invoke("OnRaceFinished", 2.0f);
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
        SceneManager.LoadScene("Replay");
    }
}
