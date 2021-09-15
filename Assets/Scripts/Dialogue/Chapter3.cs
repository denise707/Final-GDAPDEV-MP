using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter3 : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogue;
    string text = " ";
    int line = 0;

    string[] Wave_1_lines = new string[]
    {
        "Almost there...!"
    };

    string[] Wave_2_lines = new string[]
    {
        "I can do this...",
        "I must survive!",
    };

    string[] Wave_3_lines = new string[]
    {
        "SOLDIER 1: Hey are you alright?",
        "SOLDIER 2: Stay there! We'll help you",
        "(I did it. I survived.)"
    };

    private void OnEnable()
    {
        switch (GameSystem.wave - 1)
        {
            case 1: text = Wave_1_lines[0]; break;
            case 2: text = Wave_2_lines[0]; break;
            case 3: text = Wave_3_lines[0]; break;
        }

        dialogue.GetComponent<Text>().text = text;
    }

    public void onNext()
    {
        switch (GameSystem.wave - 1)
        {
            case 1:
                Debug.Log(line);
                if (line >= Wave_1_lines.Length - 1)
                {
                    line = 0;
                    GameSystem.dialogue_end = true;
                    dialogueBox.SetActive(false);
                }
                else
                {
                    line++;
                    text = Wave_1_lines[line];
                }
                break;
            case 2:
                if (line >= Wave_2_lines.Length - 1)
                {
                    line = 0;
                    GameSystem.dialogue_end = true;
                    dialogueBox.SetActive(false);
                }
                else
                {
                    line++;
                    text = Wave_2_lines[line];
                }
                break;
            case 3:
                if (line >= Wave_3_lines.Length - 1)
                {
                    line = 0;
                    GameSystem.dialogue_end = true;
                    dialogueBox.SetActive(false);
                }
                else
                {
                    line++;
                    text = Wave_3_lines[line];
                }
                break;
        }

        dialogue.GetComponent<Text>().text = text;
    }

    private void Update()
    {

    }
}
