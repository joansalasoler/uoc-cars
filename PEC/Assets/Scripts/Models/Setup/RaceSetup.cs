using UnityEngine;
using System;
using System.Collections.Generic;

namespace Shared.Models {

    /**
     * Encapsulates the settings for a race.
     */
    [CreateAssetMenu, Serializable]
    public class RaceSetup : ScriptableObject {

        /** Available player car definitions */
        public CarSetup[] availableCars = null;

        /** Available race circuit definitions */
        public CircuitSetup[] availableCircuits = null;
    }
}
