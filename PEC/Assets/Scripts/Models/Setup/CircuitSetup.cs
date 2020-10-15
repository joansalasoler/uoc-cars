using UnityEngine;
using System;

namespace Shared.Models {

    /**
     * Encapsulates the setup of a circuit.
     */
    [Serializable]
    public class CircuitSetup {

        /** Unique circuit name */
        public string name = "Undefined";

        /** Circuit template */
        public GameObject circuit = null;
    }
}
