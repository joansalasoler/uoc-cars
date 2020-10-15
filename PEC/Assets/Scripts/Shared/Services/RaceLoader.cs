using UnityEngine;
using System;
using Shared.Models;

namespace Shared.Services {

    /**
     * Loads the race components from the player preferences.
     */
    public class RaceLoader : MonoBehaviour {

        /** */
        [SerializeField] private RaceSetup setup = null;


        /**
         *
         */
        private void Awake() {
            string carName = PlayerPrefs.GetString("CarSetupName");
            string circuitName = PlayerPrefs.GetString("CircuitSetupName");
            if (carName == string.Empty) carName = setup.availableCars[1].name;
            if (circuitName == string.Empty) circuitName = setup.availableCircuits[1].name;

            Instantiate(GetCarByName(carName));
            Instantiate(GetGhostByName(carName));
            Instantiate(GetCircuitByName(circuitName));
        }


        private GameObject GetCarByName(string name) {
            Debug.Log("CAR NAME" + name);
            var value = Array.Find(setup.availableCars, e => name == e.name);
            return value.car;
        }


        private GameObject GetGhostByName(string name) {
            var value = Array.Find(setup.availableCars, e => name == e.name);
            return value.ghost;
        }


        private GameObject GetCircuitByName(string name) {
            var value = Array.Find(setup.availableCircuits, e => name == e.name);
            return value.circuit;
        }
    }
}
