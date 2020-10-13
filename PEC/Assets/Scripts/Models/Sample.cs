using UnityEngine;
using System;

namespace Shared.Models {

    /**
     * Information about a game object state at a point in time.
     */
    [Serializable]
    public class Sample {

        /** Position of the object's transform */
        private Vector3 position;

        /** Rotation of the object's transform */
        private Quaternion rotation;

        /** If the object's renderer is enabled */
        private bool enabled;


        /**
         * Creates a new sample for a game object.
         */
        public Sample(GameObject o) {
            this.position = o.transform.position;
            this.rotation = o.transform.rotation;
            this.enabled = IsRendererEnabled(o);
        }


        /**
         * Transforms a game object given a reference sample and the
         * transformation amount as a percentage.
         */
        public void Transform(GameObject o, Sample a, float t) {
            Renderer renderer = o.GetComponent<Renderer>();
            bool enabled = (this.enabled && a.enabled) || (a.enabled && t > 0.5);

            o.transform.position = Vector3.Slerp(a.position, position, t);
            o.transform.rotation = Quaternion.Slerp(a.rotation, rotation, t);
            if (renderer != null) renderer.enabled = enabled;
        }


        /**
         * Checks if the renderer of a game object is enabled.
         */
        private static bool IsRendererEnabled(GameObject o) {
            Renderer renderer = o.GetComponent<Renderer>();
            return renderer == null || renderer.enabled;
        }
    }
}
