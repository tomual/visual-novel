using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VNController : MonoBehaviour {

    private Text nameComponent;
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
        Texture2D tunaDefaultImage = Resources.Load<Texture2D>("VNImages/fishman");
        Texture2D dianaDefaultImage = Resources.Load<Texture2D>("VNImages/titsfish");
        Texture2D dianaMehImage = Resources.Load<Texture2D>("VNImages/titsfish_meh");

        Debug.Log(tunaDefaultImage);

        dialogue = new List<Line>();
        dialogue.Add(new Line(Position.LEFT, "You", "Hello, I'm a merman. I am half fish and half man.", tunaDefaultImage));
        dialogue.Add(new Line(Position.RIGHT, "You", "Hello, I'm a mermaid. I am half man and half fish.", dianaMehImage));
        dialogue.Add(new Line(Position.LEFT, "You", "Jolly good.", tunaDefaultImage));
        dialogue.Add(new Line(Position.RIGHT, "You", "What say you do a cup of tea at Margaret's Cafe?", dianaDefaultImage));
        dialogue.Add(new Line(Position.LEFT, "You", "It sounds most enjoyable.", tunaDefaultImage));

        nameComponent = GameObject.FindGameObjectWithTag("NameText").GetComponent<Text>();
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
        ChangeText(line.Name, line.Message);

        Color32 greyed = new Color32(126, 143, 156, 255);
        Color32 transparent = new Color32(255, 255, 255, 0);
        Color32 opaque = new Color32(255, 255, 255, 255);

        RawImage characterRightImage = characterRight.GetComponent<RawImage>();
        RawImage characterLeftImage = characterLeft.GetComponent<RawImage>();
        if (line.Position == Position.LEFT)
        {
            Debug.Log(characterRightImage.texture);


            characterRightImage.color = greyed;

            characterLeftImage.color = opaque;
            characterLeftImage.texture = line.Image;
        }
        else
        {
            Debug.Log(characterLeft);
            characterLeftImage.color = greyed;
            characterRightImage.color = opaque;
            characterRightImage.texture = line.Image;
        }
        lineCursor++;
    }

    void ChangeText(string name, string message)
    {
        nameComponent.text = name;
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
    public string Name;
    public string Message;
    public Texture2D Image;

    public Line(Position position, string name, string message, Texture2D image)
    {
        Position = position;
        Name = name;
        Message = message;
        Image = image;
    }

}