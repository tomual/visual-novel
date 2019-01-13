using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNController : MonoBehaviour {

    private Text textComponent;

    private string[] dialogue;

    private bool scrolling;
    private float scrollSpeed = 0.02f;
    private float lastPrint;

    private string text;
    private int scrollingCursor;

	void Start ()
    {
        dialogue = new string[] {
            "Page One",
            "Page Two"
        };

        textComponent = GameObject.FindGameObjectWithTag("ScrollingText").GetComponent<Text>();
        InitConversation();
    }
	
	void Update () {
        if (scrolling && Time.time - lastPrint >= scrollSpeed)
        {
            textComponent.text += text.ToCharArray()[scrollingCursor].ToString();
            ++scrollingCursor;
            if (scrollingCursor == text.Length)
            {
                scrolling = false;
            }
            lastPrint = Time.time;
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
        ChangeText(dialogue[0]);
    }

    void NextPage()
    {
        ChangeText(dialogue[1]);
    }

    void ChangeText(string message)
    {
        textComponent.text = "";
        text = message;
        scrolling = true;
        scrollingCursor = 0;
        lastPrint = Time.time - scrollSpeed;
    }
}
