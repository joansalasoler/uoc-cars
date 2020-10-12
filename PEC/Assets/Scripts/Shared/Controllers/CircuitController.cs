using UnityEngine;
using Shared.Events;

namespace Shared.Controllers {

    /**
     * Controller for the racing circuits.
     */
    public class CircuitController : MonoBehaviour {

        /** Start position of the car on the circuit */
        [SerializeField] private Transform polePosition = null;

        /** Ordered controls points that define a complete lap */
        [SerializeField] private Collider[] controls = null;

        /** Invoked when the control target completes a lap */
        [SerializeField] private CircuitCompletionEvent onCompletion =
            new CircuitCompletionEvent();

        /** Next control point that must be reached */
        private int nextControlIndex = 0;


        /**
         * Translates a game object into the pole position.
         */
        public void MoveToPole(Transform transform) {
            transform.position = polePosition.position;
            transform.rotation = polePosition.rotation;
        }


        /**
         * Emitted when this script is enabled.
         */
        private void Start() {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("ControlTarget");

            foreach (GameObject target in targets) {
                AttachControlEvents(target);
            }
        }


        /**
         * Attach the required control events to a target.
         */
        private void AttachControlEvents(GameObject o) {
            var emitter = o.GetComponent<CollisionEmitter>();
            emitter.onTriggerEnter.AddListener(OnControlPointEnter);
        }


        /**
         * Invoked when the control target collision with a control point.
         */
        private void OnControlPointEnter(Collider collider) {
            if (collider == controls[nextControlIndex]) {
                nextControlIndex = (1 + nextControlIndex) % controls.Length;

                if (nextControlIndex == 0) {
                    onCompletion.Invoke();
                }
            }
        }
    }
}
