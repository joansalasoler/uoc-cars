using UnityEngine;
using UnityEngine.UI;
using System;

/**
 * Head up display controller.
 */
public class HeadupController : MonoBehaviour {

    /** Format string for the displayed times */
    public readonly string TIME_FORMAT = @"mm\:ss\.f";

    /** Container for the lap time marks */
    [SerializeField] private Transform timeMarksContainer = null;

    /** Template prefab for the time marks */
    [SerializeField] private Text timeMarkPrefab = null;

    /** Elapsed time for the current lap */
    [SerializeField] private Text lapTimeText = null;

    /** Total elapsed time for the race */
    [SerializeField] private Text raceTimeText = null;

    /** Current car speed in km/h */
    [SerializeField] private Text carSpeedText = null;


    /**
     * Shows a rigidbody velocity on the car speed text.
     */
    public void ShowCarSpeed(Rigidbody rigidbody) {
        double speed = 3.6 * rigidbody.velocity.magnitude;
        carSpeedText.text = $"{speed:0}";
    }


    /**
     * Shows an elapsed timespan as the lap time text.
     */
    public void ShowLapTime(TimeSpan timeSpan) {
        lapTimeText.text = timeSpan.ToString(TIME_FORMAT);
    }


    /**
     * Shows an elapsed timespan as the race time text.
     */
    public void ShowRaceTime(TimeSpan timeSpan) {
        raceTimeText.text = timeSpan.ToString(TIME_FORMAT);
    }


    /**
     * Adds a timespan as a new time mark and shows it.
     */
    public void PushTimeMark(TimeSpan timeSpan) {
        string time = timeSpan.ToString(TIME_FORMAT);
        int count = 1 + timeMarksContainer.childCount;
        Text timeMark = Instantiate(timeMarkPrefab, timeMarksContainer);
        timeMark.text = $"<color=#ffd077>{ count }</color> { time }";
    }
}
