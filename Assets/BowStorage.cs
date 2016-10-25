using UnityEngine;
using System.Collections;

namespace VRTK.Examples.Archery
{
	public class BowStorage : MonoBehaviour {
		// Use this for initialization
		void Start () {
			
		}

		void Update() {
			
		}
		// Update is called once per frame
		void OnTriggerStay(Collider other) {
			if (other.gameObject.CompareTag ("Bow")) {
				GameObject bow = other.gameObject;
				if (!bow.GetComponent<BowAim> ().IsHeld ()) {
					Debug.Log ("Inside storage");
					bow.transform.parent = gameObject.transform;
					//bow.transform.rotation = gameObject.transform.rotation;

					bow.GetComponent<VRTK_InteractableObject>().ToggleKinematic(true);
				}
			}
		}

		void OnTriggerExit(Collider other) {
			if (other.gameObject.CompareTag ("Bow")) {
				Debug.Log ("exiting.....");

				GameObject bow = other.gameObject;
				bow.transform.parent = null;
				bow.GetComponent<VRTK_InteractableObject>().ToggleKinematic(false);
			}
		}
	}
}