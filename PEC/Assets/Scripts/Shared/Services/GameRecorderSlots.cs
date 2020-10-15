using UnityEngine;
using System;
using Shared.Models;

namespace Shared.Services {

    /**
     * Encapsulates the recording slots of a circuit.
     */
    [Serializable]
    public class GameRecorderSlots : MonoBehaviour {

        /** Race with the better time */
        [SerializeField] public Recording bestRace = null;

        /** Last playet race */
        [SerializeField] public Recording lastRace = null;
    }
}
