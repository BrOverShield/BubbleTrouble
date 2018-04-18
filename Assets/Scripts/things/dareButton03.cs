
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class dareButton03 : MonoBehaviour {

	public Button yourButton;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(changemenuscene);
	}
	
	public void changemenuscene()
	{
		SceneManager.LoadScene("Dare03");
		Debug.Log("To Dare03!");
	}
}
