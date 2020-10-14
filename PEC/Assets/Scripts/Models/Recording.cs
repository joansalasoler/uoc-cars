using UnityEngine;
using System;
using System.Collections.Generic;

namespace Shared.Models {

    /**
     * Stores the state of game objects on a period of time.
     */
    [CreateAssetMenu, Serializable]
    public class Recording : ScriptableObject {

        /** Orientation samples of the recorded objects */
        [SerializeField] protected List<Frame> frames = new List<Frame>();

        /** Time difference between two samples */
        public float SamplingPeriod = 0.25f;


        /**
         * Number of frames of the recording.
         */
        public int Count {
            get { return frames.Count; }
            private set {}
        }


        /**
         * Duration of the recording.
         */
        public float Duration {
            get { return SamplingPeriod * frames.Count; }
            private set {}
        }


        /**
         * Obtain the samples at the given index.
         */
        public Sample[] Get(int index) {
            return frames[index].samples;
        }


        /**
         * Sample the given game object's into a new frame.
         */
        public void Push(GameObject[] objects) {
            frames.Add(new Frame(CreateSamples(objects)));
        }


        /**
         * Copy this recording information from another recording.
         */
        public void Copy(Recording recording) {
            frames = new List<Frame>(recording.frames);
            SamplingPeriod = recording.SamplingPeriod;
        }


        /**
         * Remove all the frames from this race.
         */
        public void Reset() {
            frames.Clear();
        }


        /**
         * Transforms a set of game objects given a reference frame and the
         * transformation amount between frames as a percentage.
         */
        public void Transform(GameObject[] objects, int index, float amount) {
            Sample[] currentSamples = frames[Math.Max(0, index)].samples;
            Sample[] previousSamples = frames[Math.Max(0, index - 1)].samples;

            for (int i = 0; i < objects.Length; i++) {
                GameObject target = objects[i];
                Sample current = currentSamples[i];
                Sample previous = previousSamples[i];

                current.Transform(target, previous, amount);
            }
        }


        /**
         * Creates a set of samples given a set of game objects.
         */
        private Sample[] CreateSamples(GameObject[] objects) {
            Sample[] samples = new Sample[objects.Length];

            for (int i = 0; i < objects.Length; i++) {
                samples[i] = new Sample(objects[i]);
            }

            return samples;
        }
    }
}
