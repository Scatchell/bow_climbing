using UnityEngine;
using System.Collections;

namespace VRTK.Examples
{
	public class BowStorage : VRTK_InteractableObject
	{
		private bool stored = false;

		protected override void Start()
		{
			base.Start ();
		}

		public override void StartUsing (GameObject usingObject)
		{
			Debug.Log ("using..........");
			Debug.Log (usingObject);
			base.StartUsing (usingObject);

			if (usingObject.CompareTag ("GameController")) {
				VRTK_InteractGrab controller = usingObject.GetComponent<VRTK_InteractGrab> ();

				if (HasBowAsChild ()) {
					GameObject childBow = ChildBow ();
					UnStore (childBow);
					controller.AttemptGrab ();
				} else if (!HasBowAsChild () && !stored && usingObject.transform.FindChild ("BasicBow") != null) {
					GameObject bow = usingObject.transform.FindChild ("BasicBow").gameObject;

					controller.ForceRelease ();
					Store (bow);

					Debug.Log ("Inside storage");
				}
			}

			base.StopUsing (usingObject);
		}

		public override void StopUsing (GameObject previousUsingObject)
		{
			Debug.Log ("No longer being used...");
			base.StopUsing (previousUsingObject);
		}

		protected override void Update()
		{
			base.Update ();
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

		private bool HasBowAsChild ()
		{
			return (gameObject.transform.FindChild ("BasicBow") != null);
		}

		private GameObject ChildBow ()
		{
			return gameObject.transform.FindChild ("BasicBow").gameObject;
		}
	}
}
	

//void OnTriggerStay(Collider other) {
//	VRTK_InteractGrab grabbingController = getGrabbingController (other);
//	if (grabbingController != null && ControllerTriggerPressed(grabbingController) && stored && ChildBow().GetComponent<VRTK_InteractableObject>().IsTouched() && Time.time >= storeDelayTimer) {
//		Debug.Log ("Releasing....");
//		GameObject childBow = ChildBow ();
//		UnStore (childBow);
//		grabbingController.AttemptGrab ();
//		storeDelayTimer = Time.time + storeDelay;
//	} else if (other.gameObject.CompareTag ("Bow") && Time.time >= storeDelayTimer) {
//		GameObject bow = other.gameObject;
//
//		bool bowBeingHeld = bow.GetComponent<BowAim> ().IsHeld ();
//
//		if (ControllerTriggerPressed (grabbingController)) {
//			if (!stored) {
//				grabbingController.ForceRelease ();
//
//				Store (bow);
//
//				storeDelayTimer = Time.time + storeDelay;
//
//				Debug.Log ("Inside storage");
//			}
//		}
//	}
//}