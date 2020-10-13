using UnityEngine;
using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Models {

    /**
     * Records a set of game objects.
     */
    public class GameRecorder : MonoBehaviour {

        /** Time difference between two samples */
        [SerializeField] private float samplingPeriod = 0.25f;

        /** Data of the race being recorded */
        [SerializeField] private Recording recording = null;

        /** Objects that must be recorded */
        [SerializeField] private GameObject[] targets = null;

        /** Current time stamp */
        private float stamp = 0.0f;


        /**
         * Get the recording data.
         */
        public Recording GetRecording() {
            return recording;
        }


        /**
         * Starts or stops this game recorder.
         */
        public void SetActive(bool enabled) {
            this.enabled = enabled;
        }


        /**
         * Starts the recording.
         */
        private void OnEnable() {
            stamp = 0.0f;
            recording.SamplingPeriod = samplingPeriod;
            recording.Reset();
        }


        /**
         * Stops the recording.
         */
        private void OnDisable() {
            stamp = 0.0f;
        }


        /**
         * Records a sample for each sampling period.
         */
        private void Update() {
            stamp += Time.deltaTime;

            if (stamp >= samplingPeriod) {
                stamp -= samplingPeriod;
                recording.Push(targets);
            }
        }
    }
}
