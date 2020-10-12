using UnityEngine;
using UnityEngine.Events;
using System;

namespace Shared.Events {

    /**
     * Convenience behaviour that emits events when a trigger collision
     * is detected on a game object.
     */
    public class CollisionEmitter : MonoBehaviour {

        /** Emitted on trigger enter signals */
        public readonly CollisionEvent onTriggerEnter = new CollisionEvent();


        /**
         * Emitted when two game objects collide.
         */
        private void OnTriggerEnter(Collider collider) {
            onTriggerEnter.Invoke(collider);
        }
    }
}
