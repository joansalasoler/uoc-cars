using UnityEngine;
using System;

namespace Shared.Models {

    /**
     * A set of samples on a recording. This is a wrapper class required
     * to support the serialization of the samples array.
     */
    [Serializable]
    public class Frame {

        /** Samples of the frame */
        public Sample[] samples;


        /**
         * Create a new frame.
         */
        public Frame(Sample[] samples) {
            this.samples = samples;
        }
    }
}
