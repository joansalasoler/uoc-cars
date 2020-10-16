using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shared.Behaviours {

    /**
     * Automatically focus a button component.
     */
    public class HasButtonFocus : MonoBehaviour {

        /**
         * Select the button when it is enabled.
         */
        private void OnEnable() {
            Invoke("SelectButton", 0.05f);
        }


        /**
         * Select the button when a key is pressed.
         */
        private void Update() {
            if (Input.anyKey && !SelectionExists()) {
                SelectButton();
            }
        }


        /**
         * Select the button this script belongs to.
         */
        private void SelectButton() {
            GetComponent<Button>().Select();
        }


        /**
         * Check if a game object is currently selected.
         */
        private bool SelectionExists() {
            return EventSystem.current.currentSelectedGameObject != null;
        }
    }
}
