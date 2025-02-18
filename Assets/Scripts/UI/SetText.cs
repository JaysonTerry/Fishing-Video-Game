using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
public static List<string> lines = new List<string>();
public  TMP_Text textComponent;
public int numberOfLines = 0;
public int maxLines = 5;
   
    void Start()
    {
        textComponent = gameObject.GetComponentInChildren<TMP_Text>();
      //  lines.Add("Remember to do your wordle!");
        //lines.Add("And I never did...");
       // numberOfLines = 0;
       
    }

  
    void Update()
    {
   UpdateTextbox();
    }

    public void AddLine(string line) {
            lines.Add(line);
     if (lines.Count > maxLines) {
            lines.RemoveAt(0); // Removes the first line to scroll the text
        }

    }

    void UpdateTextbox() {
     // Create a string to hold all the lines with <br> tags for line breaks
        string textToDisplay = "";

        foreach (string line in lines)
        {
            textToDisplay += line + "<br>"; // Add <br> to create a new line in the TMP_Text component
        }

        // Update the TMP_Text component with the concatenated text
        textComponent.text = textToDisplay;
        }
}
//TODO: 
/* import text for SetDangerLvl(); DONE
   make display line by line DONE
   make text delete the earliest line then scroll up when full */