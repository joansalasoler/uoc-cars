using UnityEngine;
using UnityEngine.Events;
using System;

namespace Shared.Events {

    /**
     * Emitted when a trigger collision is entered.
     */
    [Serializable]
    public class CollisionEvent : UnityEvent<Collider> {}

}
