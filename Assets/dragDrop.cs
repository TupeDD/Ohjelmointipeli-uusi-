using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform parentToReturnTo = null;
	private GameObject liiku;
	public GameObject dropZone;
	public static bool liikuttamassa = false;
	public bool isClone = false;
	public GameObject aloitaButton;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		if (Slot.toiminnot < 6) {
			parentToReturnTo = this.transform;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
			if (!isClone) {
				liiku = Instantiate (this.gameObject);
				parentToReturnTo = this.transform.parent;
			} else {
				liiku = this.gameObject;
				parentToReturnTo = this.transform.parent.parent;
				dropZone.SetActive (true);
			}
			liiku.transform.SetParent (parentToReturnTo, false); 
			liikuttamassa = true;
		}
	}
	#endregion

	public void OnDrag(PointerEventData eventData) {
		if (Slot.toiminnot < 6) {
			liiku.transform.position = eventData.position;
		}
	}

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		liikuttamassa = false;
		//GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (!Slot.sainToiminnan || Slot.toiminnot > 5) {
			Destroy (liiku);
			if (!isClone) {
				this.gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			}
			dropZone.SetActive (false);
		} else {
			liiku.transform.SetParent (parentToReturnTo);
			if (!isClone) {
				liiku.GetComponent<dragDrop> ().isClone = true;
				liiku.GetComponent<CanvasGroup> ().blocksRaycasts = true;
				this.gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			} else {
				liiku.GetComponent<dragDrop> ().isClone = true;
				this.gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			}
		}
	}

	#endregion
}
