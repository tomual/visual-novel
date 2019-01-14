using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNController : MonoBehaviour {

    private Text textComponent;

    private List<Line> dialogue;

    private bool scrolling;
    private float scrollSpeed = 0.02f;
    private float lastPrint;

    private string text;
    private int scrollingCursor;
    private int lineCursor = 0;

    private GameObject characterRight;
    private GameObject characterLeft;

    void Start ()
    {
        dialogue = new List<Line>();
        dialogue.Add(new Line(Position.LEFT, "Hello, I'm a merman. I am half fish and half man."));
        dialogue.Add(new Line(Position.RIGHT, "Hello, I'm a mermaid. I am half man and half fish."));
        dialogue.Add(new Line(Position.LEFT, "Jolly good."));
        dialogue.Add(new Line(Position.RIGHT, "What say you do a cup of tea at Margaret's Cafe?"));
        dialogue.Add(new Line(Position.LEFT, "It sounds most enjoyable."));

        textComponent = GameObject.FindGameObjectWithTag("ScrollingText").GetComponent<Text>();

        characterRight = GameObject.FindGameObjectWithTag("CharacterRight");
        characterLeft = GameObject.FindGameObjectWithTag("CharacterLeft");

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
        NextPage();
    }

    void NextPage()
    {
        Line line = dialogue[lineCursor];
        Debug.Log(line.Position);
        ChangeText(line.Message);
        if (line.Position == Position.LEFT)
        {
            Debug.Log(characterRight);
            characterRight.GetComponent<RawImage>().color = new Color32(126, 143, 156, 255);
            characterLeft.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
        } else
        {
            Debug.Log(characterLeft);
            characterLeft.GetComponent<RawImage>().color = new Color32(126, 143, 156, 255);
            characterRight.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
        }
        lineCursor++;
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

public enum Position { LEFT, RIGHT };

public class Line {
    public Position Position;
    public string Message;

    public Line(Position position, string message)
    {
        Position = position;
        Message = message;
    }

}