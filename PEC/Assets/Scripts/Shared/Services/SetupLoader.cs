using UnityEngine;
using System;
using Shared.Models;

namespace Shared.Services {

    /**
     * Loads the race components automatically.
     *
     * This class instantiates the player's car, ghost car and circuit
     * according to the stored values on the player preferences.
     */
    public class SetupLoader : MonoBehaviour {

        /** Specifies the available cars and circuits */
        [SerializeField] private RaceSetup options = null;


        /**
         * Instantiate the objects.
         */
        private void Awake() {
            string carName = PlayerPrefs.GetString("CarSetupName");
            string circuitName = PlayerPrefs.GetString("CircuitSetupName");
            Instantiate(GetCarByName(carName));
            Instantiate(GetGhostByName(carName));
            Instantiate(GetCircuitByName(circuitName));
        }


        /**
         * Get a car from the options given its name.
         */
        private GameObject GetCarByName(string name) {
            var cars = options.availableCars;
            var value = Array.Find(cars, e => name == e.name);

            return value != null ? value.car : cars[0].car;
        }


        /**
         * Get a ghost car from the options given its name.
         */
        private GameObject GetGhostByName(string name) {
            var cars = options.availableCars;
            var value = Array.Find(cars, e => name == e.name);

            return value != null ? value.ghost : cars[0].ghost;
        }


        /**
         * Get a circuit from the options given its name.
         */
        private GameObject GetCircuitByName(string name) {
            var circuits = options.availableCircuits;
            var value = Array.Find(circuits, e => name == e.name);

            return value != null ? value.circuit : circuits[0].circuit;
        }
    }
}
