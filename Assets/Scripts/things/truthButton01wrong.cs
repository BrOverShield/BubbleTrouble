
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class truthButton01wrong : MonoBehaviour {

	public Button yourButton;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(changemenuscene);
	}
	
	public void changemenuscene()
	{
		SceneManager.LoadScene("Truth01-wrong");
		Debug.Log("01Wrong!");
	}
}
