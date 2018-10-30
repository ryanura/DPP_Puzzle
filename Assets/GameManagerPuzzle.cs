using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class GameManagerPuzzle : MonoBehaviour {
	[SerializeField]
	private Transform PuzzleField;

	[SerializeField]
	private GameObject Button;

	[SerializeField]
	private Sprite Button_BG;
	public Sprite [] Puzzle_Spr;

	public Text Score_txt;
	public Text Timer_txt;

	public List<Button> ButtonList = new List<Button> ();
	public List<Sprite> SpriteList = new List<Sprite> ();

	private bool[] Pick_Index;
	private int PickCounter;

	private string[] PickPuzzle;
	public AudioSource Audio1;
	public AudioSource Audio2;
	public AudioSource AudioButton;
	public AudioSource AudioFlip;


}
