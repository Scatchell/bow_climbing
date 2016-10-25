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
			if (other.gameObject.CompareTag ("Controller") && ControllerTriggerPressed(other.gameObject.GetComponent<VRTK_InteractGrab>()) && HasBowAsChild()) {
				VRTK_InteractGrab controller = other.gameObject.GetComponent<VRTK_InteractGrab>();

				GameObject childBow = ChildBow ();
				UnStore (childBow);
			} else if (other.gameObject.CompareTag ("Bow")) {
				GameObject bow = other.gameObject;

				bool bowBeingHeld = bow.GetComponent<BowAim> ().IsHeld ();
				VRTK_InteractGrab grabbingController = (bow.GetComponent<VRTK_InteractGrab>() ? bow.GetComponent<VRTK_InteractGrab>() : bow.GetComponentInParent<VRTK_InteractGrab>());

				if (ControllerTriggerPressed (grabbingController)) {
					if (!stored) {
						grabbingController.ForceRelease ();

						Store (bow);

						Debug.Log ("Inside storage");
					}
				}
			}
		}

		private void UnStore (GameObject bow)
		{
			bow.transform.parent = null;
			bow.GetComponent<VRTK_InteractableObject> ().ToggleKinematic (false);
			bow.GetComponent<VRTK_InteractableObject> ().isGrabbable = true;
			stored = false;
		}

		private void Store (GameObject bow)
		{
			bow.GetComponent<VRTK_InteractableObject> ().ToggleKinematic (true);
			bow.GetComponent<VRTK_InteractableObject> ().isGrabbable = false;
			bow.transform.parent = gameObject.transform;
			stored = true;
		}

		private static bool ControllerTriggerPressed (VRTK_InteractGrab grabbingController)
		{
			return grabbingController && grabbingController.gameObject.GetComponent<VRTK_ControllerEvents> ().triggerPressed;
		}

		private bool HasBowAsChild()
		{
			return (gameObject.transform.FindChild("BasicBow") != null);
		}

		private GameObject ChildBow()
		{
			return gameObject.transform.FindChild("BasicBow").gameObject;
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