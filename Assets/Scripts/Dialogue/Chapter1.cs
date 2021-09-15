using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter1 : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogue;
    string text = " ";
    int line = 0;

    string[] Wave_1_lines = new string[]
    {
        "These monsters...where did they came from...?",
        "Where is everyone...?",
        "I'll check if there are survivors in the park."
    };

    string[] Wave_2_lines = new string[]
    {
        "There's too many of them...",
        "I have to get out of here!",
    };

    string[] Wave_3_lines = new string[]
    {
        "I survived!",
        "But I think I hurt my arm...",
        "I need to find some place to rest..."
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
