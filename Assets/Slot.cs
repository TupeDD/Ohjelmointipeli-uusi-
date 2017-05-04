using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public GameObject text;
	private string textText = "Vedä toimintoja tänne";
	public static bool sainToiminnan = false;
	public static int toiminnot = 0;
	public GameObject dropZone;

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		dragDrop d = eventData.pointerDrag.GetComponent<dragDrop> ();
		if (d != null) {
			sainToiminnan = true;
			d.parentToReturnTo = this.transform;
			dropZone.SetActive (false);
		}
	}
	#endregion
	
	#region IPointerEnterHandler implementation
	public void OnPointerEnter (PointerEventData eventData)
	{
		if (dragDrop.liikuttamassa) {
			text.GetComponent<Text> ().text = " ";
		} //else if (!dragDrop.liikuttamassa && toiminnot == 0){
			//text.GetComponent<Text> ().text = textText;
		//}
	}
	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		if (toiminnot == 0 && dragDrop.liikuttamassa) {
			text.GetComponent<Text> ().text = textText;
		}
		if (toiminnot != 0 && dragDrop.liikuttamassa) {
			dropZone.SetActive (true);
		}
		sainToiminnan = false;
	}

	#endregion
}
