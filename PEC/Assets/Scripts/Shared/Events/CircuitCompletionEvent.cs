using UnityEngine;
using UnityEngine.Events;
using System;

namespace Shared.Events {

    /**
     * Emitted when a full circuit lap is completed.
     */
    [Serializable]
    public class CircuitCompletionEvent : UnityEvent {}

}
