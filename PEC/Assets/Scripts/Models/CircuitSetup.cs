using UnityEngine;
using System;

namespace Shared.Models {

    /**
     *
     */
    [Serializable]
    public class CircuitSetup {

        /** Unique circuit name */
        public string name = "Undefined";

        /** Circuit template */
        public GameObject circuit = null;
    }
}
