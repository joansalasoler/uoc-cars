using System;
using UnityEngine;

namespace Shared.Timers {

    /**
     * A stop watch timer implementation that takes into account the current
     * time scale of the game.
     */
    public class StopWatch {

        /** Wether the timer is running */
        private bool active = false;

        /** Time when the watch was started in seconds */
        private float start = 0.0f;

        /** Seconds since the timer was started */
        private float elapsed = 0.0f;


        /**
         * Start or resume the watch.
         */
        public void Start() {
            if (active == false) {
                active = true;
                start = Time.time;
            }
        }


        /**
         * Stops the watch.
         */
        public void Stop() {
            if (active == true) {
                elapsed += Time.time - start;
                active = false;
            }
        }


        /**
         * Resets and starts the watch.
         */
        public void Restart() {
            active = true;
            start = Time.time;
            elapsed = 0.0f;
        }


        /**
         * Gets the elsapsed time in seconds.
         */
        public float GetSeconds() {
            return elapsed + (active ? Time.time - start : 0.0f);
        }


        /**
         * Gets the elsapsed time as a time span.
         */
        public TimeSpan GetTimeSpan() {
            return TimeSpan.FromSeconds(GetSeconds());
        }
    }
}
