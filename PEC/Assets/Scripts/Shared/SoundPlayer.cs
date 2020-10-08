using UnityEngine;

namespace Shared {

    /**
     * Plays sounds on the scenes.
     */
    public class SoundPlayer : MonoBehaviour {

        /**
         * Invoked when the script is loaded.
         */
        public void Awake() {
            if (SoundPlayerExists()) {
                Destroy(this.gameObject);
            } else {
                DontDestroyOnLoad(this.gameObject);
                GetComponent<AudioSource>().Play();
            }
        }


        /**
         * Checks if a sound player already exists.
         */
        private bool SoundPlayerExists() {
            return GameObject.FindGameObjectsWithTag("Music Player").Length > 1;
        }
    }
}
