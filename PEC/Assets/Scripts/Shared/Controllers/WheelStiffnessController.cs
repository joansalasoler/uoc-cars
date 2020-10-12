using UnityEngine;

namespace Shared.Controllers {

    /**
     * Controls the friction of a wheel according to the main material
     * of the object which the wheel collides.
     */
    public class WheelStiffnessController : MonoBehaviour {

        /** Wheel collider to control */
        private WheelCollider wheelCollider = null;


        /**
         * Fired when the script is enabled.
         */
        private void Start() {
            this.wheelCollider = GetComponent<WheelCollider>();
        }


        /**
         * Change wheel stiffnes when a collision is triggered.
         */
        private void OnTriggerEnter(Collider collider) {
            PhysicMaterial material = collider.material;
            WheelFrictionCurve curve = wheelCollider.forwardFriction;

            if (material is PhysicMaterial) {
                curve.stiffness = material.staticFriction;
                wheelCollider.forwardFriction = curve;
            }
        }
    }
}
