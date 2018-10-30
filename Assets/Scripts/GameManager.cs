using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
	[SerializeField]
	private Transform PuzzleField;

	[SerializeField]
	private GameObject btn;

	[SerializeField]
	private Sprite ButtonBG;
	public Sprite[] PuzzleImg;

	public Text ScoreText;
	public Text TimerText;
	public Text FinalScoreText;
	private int score;
	public List<Button> ButtonList = new List<Button>();
	public List<Sprite> SpriteList = new List<Sprite>();

	private bool firstPick, secondPick;

	private int countPick;
	private int countCorrect;
	private int gamePick;
	private int firstPickIndex, SecondPickIndex;

	private string firstPickPuzzle, secondPickPuzzle;

	public AudioSource Audio1;
	public AudioSource Audio2;
	public AudioSource AudioButton;
	public AudioSource AudioFlip;
	//AudioSource audiobtn;
	public GameObject MainMenu;
	public GameObject DifficultyPanel;
	public int totalGrid;
	float maxTime;
	float timeRemaining;
	public Image TimerBar;
	public bool gameStarted = false;
	//public RectTransform newFillrect;


	// Use this for initialization
	 void Awake(){
		/*for (int i = 0; i < totalGrid2; i++) {
			//for (int j = 0; j <Gridy; i++){
			GameObject button = Instantiate (btn);
			button.name = "" + i;
			button.transform.SetParent (PuzzleField, false);		
			//}
		}*/
		PuzzleImg = Resources.LoadAll<Sprite> ("Sprites/Ghost");
	}

	void Start () {
		DifficultyPanel.SetActive (false);
		//audiobtn = gameObject.GetComponent<AudioSource> ();
	}

	public void ChooseDifficulty(){
		//MainMenu.SetActive (false);
		DifficultyPanel.SetActive (true);
	}

	public void ChangetoEasyGame(){
		MainMenu.SetActive (false);
		Audio1.Stop();
		Audio2.Play();
		totalGrid = 20;
		maxTime= 60f;
		gameStarted = true;
		GameStart ();
	}

	public void ChangetoHDGame(){
		MainMenu.SetActive (false);
		Audio1.Stop();
		Audio2.Play();
		totalGrid = 30;
		maxTime= 100f;
		gameStarted = true;
		GameStart ();
	}

	public void AudioButtonPressed(){
		AudioButton.Play();
	}

	public void QuitApps(){
		Application.Quit();
	}

	void GameStart(){
		timeRemaining = maxTime;
		if (gameStarted == true) {

			GetButton ();
			//TimerProgress ();
			AddListeners ();
			AddPuzzlesImage ();
			Shuffle (SpriteList);
			gamePick = SpriteList.Count / 2;
			//gameStarted = false;
		}
	}


	void Update(){
		TimerBar.fillAmount = maxTime;
		//float second;
		if (gameStarted == true) {
			if (timeRemaining <= maxTime) {
				timeRemaining -= Time.deltaTime;
				//newFillrect = timeRemaining;
				TimerBar.fillAmount = timeRemaining/maxTime;
				TimerText.text = (int)timeRemaining + " s";
				//ttDebug.Log (timeRemaining + "sec and " + totalGrid + "tiles");
			}
		}
		if (gameStarted == true && timeRemaining <= 0) {
			Debug.Log ("end");
			CheckIfFinished ();
			//Application.Quit;
		}
	}
		
	void GetButton () {
		Debug.Log ("GetButton");
		for (int i = 0; i < totalGrid; i++) {
			GameObject button = Instantiate (btn);
			button.name = "" + i;
			button.transform.SetParent (PuzzleField, false);
		}

		GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleBtn");

		for (int i = 0; i < objects.Length; i++) {
			ButtonList.Add (objects [i].GetComponent<Button> ());
			ButtonList [i].image.sprite = ButtonBG;
		}
	}

	void AddPuzzlesImage(){
		int Looper = ButtonList.Count;
		int index = 0;

		for (int i = 0; i < Looper; i++) {
			if (index == Looper/2 || index >= 5) {
				index = 0;
			}
			SpriteList.Add (PuzzleImg [index]);
			index++;
		}
	}

	void AddListeners(){
		foreach (Button Btn in ButtonList) {
			Btn.onClick.AddListener (() => ClickPuzzle ());
			//Btn.onClick.AddListener(
		}
	}

	void ClickPuzzle(){
		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
		//Debug.Log ("You choose this ghost " + name);

		if (!firstPick) {
			AudioFlip.Play();
			firstPick = true;
			firstPickIndex = int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			firstPickPuzzle = SpriteList [firstPickIndex].name;
			ButtonList [firstPickIndex].image.sprite = SpriteList [firstPickIndex];
			ButtonList [firstPickIndex].interactable = false; 
		} else if (!secondPick) {
			AudioFlip.Play();
			secondPick = true;
			SecondPickIndex= int.Parse (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			secondPickPuzzle= SpriteList [SecondPickIndex].name;
			ButtonList [SecondPickIndex].image.sprite = SpriteList [SecondPickIndex];
			//ButtonList [firstPickIndex].interactable = false;
			countPick++;
			 
			StartCoroutine (CheckPuzzleMatch());
		}
	}

	IEnumerator CheckPuzzleMatch(){
		yield return new WaitForSeconds (0.5f);
		ButtonList [firstPickIndex].interactable = true;
		if (firstPickPuzzle == secondPickPuzzle) {
			yield return new WaitForSeconds (0.5f);

			ButtonList [firstPickIndex].interactable = false;
			ButtonList [SecondPickIndex].interactable = false;

			ButtonList [firstPickIndex].image.color = new Color (0, 0, 0, 0);
			ButtonList [SecondPickIndex].image.color= new Color (0, 0, 0, 0);

			AddScore ();

			CheckIfFinished ();
		} else {
			yield return new WaitForSeconds (0.5f);
			ButtonList[firstPickIndex].image.sprite= ButtonBG;
			ButtonList[SecondPickIndex].image.sprite = ButtonBG;
		}

		yield return new WaitForSeconds (0.5f);
		firstPick =  false;
		secondPick = false;
	}

	void CheckIfFinished (){
		countCorrect++;

		if (countCorrect == gamePick ){
			gameStarted = false;
			float endtime;
			if (timeRemaining >= maxTime) {
				timeRemaining = maxTime;
			} else {
			
			}
			endtime = maxTime - timeRemaining;
			Debug.Log ("Game Finished, with count" + countPick + endtime);
			FinalScoreText.text = "Congratulation!! You scored " + score + "\nwithin " + (int)endtime + "seconds";
			//FinalScoreTex
		} else if(timeRemaining <= 0){
			FinalScoreText.text = "Time's up! You got " + score;
		}
	}

	void Shuffle(List<Sprite> list){
		for (int i = 0; i < list.Count; i++) {
			Sprite temp = list [i];
			int randomIndex = Random.Range (i, list.Count);
			list[i] = list [randomIndex];
			list [randomIndex] = temp;
		}
	}

	void AddScore (){
		int ScoreValue = 10;
		int bonustime = 1;
		score += ScoreValue;
		ScoreText.text = score.ToString();
		//timeRemaining += bonustime;
	}
}