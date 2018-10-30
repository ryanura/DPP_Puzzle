using UnityEngine;
using System.Collections;

public class AddButton : MonoBehaviour {
	[SerializeField]
	private Transform PuzzleField;

	[SerializeField]
	private GameObject btn;

	public int totalGrid;
	public int Gridy;

	void Awake(){
		for (int i = 0; i < totalGrid; i++) {
			//for (int j = 0; j <Gridy; i++){
				GameObject button = Instantiate (btn);
				button.name = "" + i;
				button.transform.SetParent (PuzzleField, false);		
			//}
		}
	}
}
