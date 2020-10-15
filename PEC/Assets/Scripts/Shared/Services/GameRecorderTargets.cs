using UnityEngine;
using System;
using System.Collections.Generic;

namespace Shared.Services {

    /**
     * Encapsulates the recording targets of an object.
     */
    [Serializable]
    public class GameRecorderTargets : MonoBehaviour {

        /** Objects that must be recorded */
        [SerializeField] public GameObject[] targets = null;
    }
}
