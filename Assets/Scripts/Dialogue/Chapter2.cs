using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter2 : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogue;
    string text = " ";
    int line = 0;

    string[] Wave_1_lines = new string[]
    {
        "This place looks like...",
        "It's my school!",
        "Maybe there's someone inside...?"
    };

    string[] Wave_2_lines = new string[]
    {
        "The whole school is also infested!",
        "...",
        "Something's written on the board...",
        "\"Building 0820. Roof Top. We'll wait for survivors.\"",
        "!?",
        "I must get there!"
    };

    string[] Wave_3_lines = new string[]
    {
        "I did it...",
        "Now where do I go?",
        "Ah! This way!"
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
        SoundManagerScript.PlaySound("Button");
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
