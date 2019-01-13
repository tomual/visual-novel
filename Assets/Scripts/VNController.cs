using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNController : MonoBehaviour {

    private Text scrollingText;

	void Start () {
        scrollingText = GameObject.FindGameObjectWithTag("ScrollingText").GetComponent<Text>();
        InitConversation();
	}
	
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            NextPage();
        }
    }

    void InitConversation()
    {
        ChangeText("Page one");
    }

    void NextPage()
    {
        ChangeText("Page Two");
    }

    void ChangeText(string message)
    {
        scrollingText.text = message;
    }
}
