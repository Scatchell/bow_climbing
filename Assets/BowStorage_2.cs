using UnityEngine;
using System.Collections;

namespace VRTK
{
	public class BowStorage : VRTK_InteractableObject {
		private bool stored = false;
		// Use this for initialization
		void Start () {
			
		}

		void Update() {
		}

		public override void StartUsing(GameObject usingObject)
		{
			base.StartUsing (usingObject);

			if (usingObject.CompareTag ("Controller"))
			{
				VRTK_InteractGrab controller = usingObject.GetComponent<VRTK_InteractGrab>();

				if (HasBowAsChild()) {
					GameObject childBow = ChildBow ();
					UnStore (childBow);
				} else if (usingObject.CompareTag ("Controller") && !HasBowAsChild() && !stored) {
					GameObject bow = usingObject.transform.FindChild("BasicBow").gameObject;

					controller.ForceRelease ();
					Store (bow);

					Debug.Log ("Inside storage");
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
	}
}