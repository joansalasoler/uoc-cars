using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Shared.Models;


/**
 * Replay screen controller.
 */
public class ReplayController : MonoBehaviour {

    /** Race car object */
    [SerializeField] private GameObject car = null;

    /** Ghost car object */
    [SerializeField] private GameObject ghost = null;

    /** Race circuit object */
    [SerializeField] private GameObject circuit = null;

    /** Replayer of the best race as a ghost car */
    [SerializeField] private GameReplayer replayer = null;

    /** Available cameras */
    private List<Camera> cameras = new List<Camera>();

    /** Current camera */
    private Camera activeCamera = null;


    private void Start() {
        ghost.SetActive(false);
        cameras.AddRange(GetChildCameras(circuit));

        foreach (var c in cameras) {
            c.gameObject.SetActive(true);
        }

        StartReplayer();
        InvokeRepeating("SwitchCamera", 0.0f, 0.5f);
    }


    /**
     * Starts the game replayer.
     */
    private void StartReplayer() {
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
            float d = GetDistance(car, camera.gameObject);

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
}
