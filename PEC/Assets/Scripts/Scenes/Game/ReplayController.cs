using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using System.Collections;
using System.Collections.Generic;
using Shared.Behaviours;
using Shared.Models;
using Shared.Services;


/**
 * Replay screen controller.
 */
public class ReplayController : HasRaceComponents {

    [Header("Interface Controllers")]

    /** Video display controller */
    [SerializeField] private VideoController video = null;

    [Header("Race Recordings")]

    /** Replayer of the best race as a ghost car */
    [SerializeField] private GameReplayer replayer = null;

    /** Available cameras */
    private List<Camera> cameras = new List<Camera>();

    /** Current camera */
    private Camera activeCamera = null;


    protected override void Start() {
        base.Start();

        var cr = car.GetComponent<GameRecorderTargets>();
        replayer.SetTargets(cr.targets);

        var cs = circuit.GetComponent<GameRecorderSlots>();
        replayer.SetRecording(cs.lastRace);

        ghost.gameObject.SetActive(false);
        BlockCarControls(car);

        cameras.AddRange(GetChildCameras(circuit.gameObject));
        StartReplayer();
        InvokeRepeating("SwitchCamera", 0.0f, 0.5f);
    }


    /**
     * Starts the game replayer.
     */
    private void StartReplayer() {
        video.GetComponent<Canvas>().enabled = true;
        replayer.SetActive(true);
    }


    /**
     *
     */
    private Camera[] GetChildCameras(GameObject o) {
        return o.GetComponentsInChildren<Camera>(true);
    }


    /**
     *
     */
    private void SwitchCamera() {
        float distance = float.PositiveInfinity;

        if (activeCamera != null) {
            activeCamera.enabled = false;
        }

        foreach (var camera in cameras) {
            float d = GetDistance(car.gameObject, camera.gameObject);

            if (d < distance) {
                distance = d;
                activeCamera = camera;
            }
        }

        activeCamera.enabled = true;
    }


    private float GetDistance(GameObject a, GameObject b) {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }


    /**
     * Disables the user controller of a car.
     */
    private void BlockCarControls(CarController car) {
        car.GetComponent<CarController>().enabled = false;
        car.GetComponent<CarUserControl>().enabled = false;
    }
}
