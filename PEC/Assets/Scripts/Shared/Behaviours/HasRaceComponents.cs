using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using Shared.Controllers;

namespace Shared.Behaviours {

    /**
     * An object that targets the race components.
     *
     * The race components are a circuit, a player car and a ghost car. They
     * may be tagged with "Circuit", "Player" and "Ghost" if autotargeting of
     * the components is enabled.
     */
    public abstract class HasRaceComponents : MonoBehaviour {

        /** Tag that identifies a car */
        private readonly string TAG_OF_CAR = "Player";

        /** Tag that identifies a circuit */
        private readonly string TAG_OF_CIRCUIT = "Circuit";

        /** Tag that identifies a ghost car */
        private readonly string TAG_OF_GHOST = "Ghost";

        /** If the components must be obtained automatically */
        [SerializeField] protected bool autoTargetComponents = true;

        [Header("Components")]

        /** Player car controller instance */
        [SerializeField] protected CarController car = null;

        /** Ghost car controller instance */
        [SerializeField] protected GhostController ghost = null;

        /** Race circuit controller instance */
        [SerializeField] protected CircuitController circuit = null;


        /**
         * Invoked when the script is enabled.
         */
        protected virtual void Start() {
            if (autoTargetComponents == true) {
                FindAndTargetCar();
                FindAndTargetGhost();
                FindAndTargetCircuit();
            }
        }


        /**
         * Auto-target an object tagged with TAG_OF_CAR.
         */
        private void FindAndTargetCar() {
            var target = GameObject.FindGameObjectWithTag(TAG_OF_CAR);
            if (target) car = target.GetComponent<CarController>();
        }


        /**
         * Auto-target an object tagged with TAG_OF_GHOST.
         */
        private void FindAndTargetGhost() {
            var target = GameObject.FindGameObjectWithTag(TAG_OF_GHOST);
            if (target) ghost = target.GetComponent<GhostController>();
        }


        /**
         * Auto-target an object tagged with TAG_OF_CIRCUIT.
         */
        private void FindAndTargetCircuit() {
            var target = GameObject.FindGameObjectWithTag(TAG_OF_CIRCUIT);
            if (target) circuit = target.GetComponent<CircuitController>();
        }
    }
}
