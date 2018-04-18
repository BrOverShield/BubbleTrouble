
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class dareButton01 : MonoBehaviour {

	public Button yourButton;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(changemenuscene);
	}
	
	public void changemenuscene()
	{
		SceneManager.LoadScene("Dare01");
		Debug.Log("To Dare01!");
	}
}
