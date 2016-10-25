using UnityEngine;
using System.Collections;

namespace VRTK.Examples.Archery
{
	public class BowStorage : MonoBehaviour {
		public bool stored = false;
		// Use this for initialization
		void Start () {
			
		}

		void Update() {
		}
		// Update is called once per frame
		void OnTriggerStay(Collider other) {
			if (other.gameObject.CompareTag ("Bow")) {
				GameObject bow = other.gameObject;

				bool bowBeingHeld = bow.GetComponent<BowAim> ().IsHeld ();
				VRTK_InteractGrab grabbingController = (bow.GetComponent<VRTK_InteractGrab>() ? bow.GetComponent<VRTK_InteractGrab>() : bow.GetComponentInParent<VRTK_InteractGrab>());

				if (grabbingController && grabbingController.gameObject.GetComponent<VRTK_ControllerEvents>().triggerPressed && !stored) {
					grabbingController.ForceRelease();

					bow.GetComponent<VRTK_InteractableObject>().ToggleKinematic(true);
					bow.transform.parent = gameObject.transform;

					stored = true;

					Debug.Log ("Inside storage");
				}
			}
		}

		private bool HasBowAsChild()
		{
			return (gameObject.transform.FindChild("BasicBow") != null);
		}

		void OnTriggerExit(Collider other) {
			if (other.gameObject.CompareTag ("Bow")) {
				GameObject bow = other.gameObject;
				VRTK_InteractGrab grabbingController = (bow.GetComponent<VRTK_InteractGrab>() ? bow.GetComponent<VRTK_InteractGrab>() : bow.GetComponentInParent<VRTK_InteractGrab>());

				if (grabbingController != null && stored)
				{
					//bow.transform.parent = null;
					//bow.GetComponent<VRTK_InteractableObject>().ToggleKinematic(false);
					stored = false;

					Debug.Log ("Released from storage");
				}
			}
		}
	}
}