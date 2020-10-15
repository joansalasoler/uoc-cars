using UnityEngine;
using System;

namespace Shared.Models {

    /**
     * Encapsulates the setup of a car.
     */
    [Serializable]
    public class CarSetup {

        /** Unique car name */
        public string name = "Undefined";

        /** Player's car template */
        public GameObject car = null;

        /** Ghost of the player car template */
        public GameObject ghost = null;
    }
}
