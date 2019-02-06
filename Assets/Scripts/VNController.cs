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

    RawImage characterRightImage;
    RawImage characterLeftImage;

    void Start ()
    {
        Texture2D tunaDefaultImage = Resources.Load<Texture2D>("VNImages/fishman");
        Texture2D dianaDefaultImage = Resources.Load<Texture2D>("VNImages/titsfish");
        Texture2D dianaMehImage = Resources.Load<Texture2D>("VNImages/titsfish_meh");

        Debug.Log(tunaDefaultImage);

        nameComponent = GameObject.FindGameObjectWithTag("NameText").GetComponent<Text>();
        textComponent = GameObject.FindGameObjectWithTag("ScrollingText").GetComponent<Text>();

        characterRight = GameObject.FindGameObjectWithTag("CharacterRight");
        characterLeft = GameObject.FindGameObjectWithTag("CharacterLeft");

        characterRightImage = characterRight.GetComponent<RawImage>();
        characterLeftImage = characterLeft.GetComponent<RawImage>();

        dialogue = new List<Line>();
        dialogue.Add(new Line(Position.LEFT, "You", "Hello, I'm a merman. I am half fish and half man.", tunaDefaultImage));
        dialogue.Add(new Line(Position.RIGHT, "You", "Hello, I'm a mermaid. I am half man and half fish.", dianaMehImage));
        dialogue.Add(new Line(Position.LEFT, "You", "Jolly good.", null));
        dialogue.Add(new Line(Position.RIGHT, "You", "What say you do a cup of tea at Margaret's Cafe?", dianaDefaultImage));
        dialogue.Add(new Line(Position.LEFT, "You", "It sounds most enjoyable.", tunaDefaultImage));

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
        characterLeftImage.texture = Resources.Load<Texture2D>("VNImages/fishman");
        characterRightImage.texture = Resources.Load<Texture2D>("VNImages/titsfish");
        NextPage();
    }

    void NextPage()
    {
        if (lineCursor >= dialogue.Count)
        {
            return;
        }

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
            if (characterRightImage.texture)
            {
                characterRightImage.color = greyed;
            }

            characterLeftImage.texture = line.Image;
            if (line.Image)
            {
                characterLeftImage.color = opaque;
            } else
            {
                characterLeftImage.color = transparent;
            }
        }
        else
        {
            if (characterLeftImage.texture)
            {
                characterLeftImage.color = greyed;
            }

            characterRightImage.texture = line.Image;
            if (line.Image)
            {
                characterRightImage.color = opaque;
            }
            else
            {
                characterRightImage.color = transparent;
            }
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