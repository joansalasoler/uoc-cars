using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;
using System.Collections.Generic;
using Shared.Behaviours;
using Shared.Models;
using Shared.Services;


/**
 * Replay scene controller.
 */
public class ReplayController : HasRaceComponents {

    /** Tag of the circuit cameras */
    public readonly string TAG_OF_CAMERA = "MainCamera";

    [Header("Race Recordings")]

    /** Replayer of the best race as a ghost car */
    [SerializeField] private GameReplayer replayer = null;

    /** Available circuit cameras */
    private List<Camera> cameras = new List<Camera>();

    /** Current active camera */
    private Camera activeCamera = null;


    /**
     * Invoked when the script is enabled.
     */
    protected override void Start() {
        base.Start();

        ghost.gameObject.SetActive(false);
        circuit.MoveToPole(car.GetComponent<Transform>());

        BlockCarComponents(car);
        SetupCircuitCameras();
        SetupGameRecordings();
        StartReplayer();

        InvokeRepeating("SwitchPlaybackCamera", 0.0f, 0.5f);
    }


    /**
     * Disables the controllers of a car.
     */
    private void BlockCarComponents(CarController car) {
        car.GetComponent<Rigidbody>().isKinematic = true;
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControl>().enabled = false;
        car.GetComponent<CarAudio>().enabled = false;
    }


    /**
     * Starts the game replayer.
     */
    private void StartReplayer() {
        replayer.SetActive(true);
    }


    /**
     * Initialize the game replayer.
     */
    private void SetupGameRecordings() {
        var carTargets = car.GetComponent<GameRecorderTargets>();
        var circuitSlots = circuit.GetComponent<GameRecorderSlots>();

        replayer.SetTargets(carTargets.targets);
        replayer.SetRecording(circuitSlots.lastRace);
    }


    /**
     * Obtains all the circuit cameras active on the scene.
     */
    private void SetupCircuitCameras() {
        Camera carCamera = car.GetComponentInChildren<Camera>();

        foreach (var o in GameObject.FindGameObjectsWithTag(TAG_OF_CAMERA)) {
            Camera camera = o.GetComponent<Camera>();

            if (camera != null && camera != carCamera) {
                cameras.Add(camera);
            }
        }
    }


    /**
     * Switch the enabled camera to the one closest to the car.
     */
    private void SwitchPlaybackCamera() {
        float closestDistance = float.PositiveInfinity;
        Camera currentCamera = activeCamera;

        foreach (var camera in cameras) {
            float distance = GetDistance(car.gameObject, camera.gameObject);

            if (distance < closestDistance) {
                closestDistance = distance;
                activeCamera = camera;
            }
        }

        if (currentCamera != activeCamera) {
            activeCamera.enabled = true;

            if (currentCamera != null) {
                currentCamera.enabled = false;
            }
        }
    }


    /**
     * Obtain the distance between two game objects.
     */
    private float GetDistance(GameObject a, GameObject b) {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }
}
