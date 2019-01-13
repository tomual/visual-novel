using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNController : MonoBehaviour {

    private Text textComponent;

    private bool scrolling;
    private float scrollSpeed = 1;
    private float lastPrint;

    private string text;
    private int scrollingCursor;

	void Start () {
        textComponent = GameObject.FindGameObjectWithTag("ScrollingText").GetComponent<Text>();
        InitConversation();
	}
	
	void Update () {

        if (scrolling && Time.time - lastPrint >= 1)
        {
            textComponent.text += text.ToCharArray()[scrollingCursor].ToString();
            ++scrollingCursor;
            if (scrollingCursor == text.Length)
            {
                scrolling = false;
            }
        }

        if (!scrolling)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                NextPage();
            }
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
        textComponent.text = "";
        text = message;
        scrolling = true;
        scrollingCursor = 0;
        lastPrint = Time.time;
    }
}
