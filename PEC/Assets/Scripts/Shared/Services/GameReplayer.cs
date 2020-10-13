using UnityEngine;
using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Models {

    /**
     * Replays a set of recorded game objects.
     */
    public class GameReplayer : MonoBehaviour {

        /** Recording of the game to play */
        [SerializeField] private Recording recording = null;

        /** Objects that must be replayed */
        [SerializeField] private GameObject[] targets = null;

        /** Current time stamp */
        private float stamp = 0.0f;

        /** Current frame index */
        private int frame = 0;


        /**
         * Get the recording data.
         */
        public Recording GetRecording() {
            return recording;
        }


        /**
         * Starts or stops this game replayer.
         */
        public void SetActive(bool enabled) {
            this.enabled = enabled;
        }


        /**
         * Starts the playback.
         */
        private void OnEnable() {
            frame = 0;
            stamp = 0.0f;
        }


        /**
         * Stops the playback.
         */
        private void OnDisable() {
            frame = 0;
            stamp = 0.0f;
        }


        /**
         * Transform the playback targets on each frame.
         */
        private void Update() {
            stamp += Time.deltaTime;

            if (stamp >= recording.SamplingPeriod) {
                stamp -= recording.SamplingPeriod;
                frame++;
            }

            if (frame < recording.Count) {
                float amount = stamp / recording.SamplingPeriod;
                recording.Transform(targets, frame, amount);
            }
        }
    }
}
