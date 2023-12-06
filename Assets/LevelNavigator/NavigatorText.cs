using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NavigatorText : MonoBehaviour
{
    public TextMeshPro levelText;
    public TextMeshPro scoreText;
    public TextMeshPro prText;
    public int pr = 0;
    public int level;
    public int mostRecentScore = 0;

    void Start()
    {
        GameObject textObject = GameObject.Find("Canvas/Level"); // Replace with the name of your GameObject
        levelText = textObject?.GetComponent<TextMeshPro>();
        // pr = PlayerPrefs.GetInt("HighScore"+level, 0);
        
        // levelText = getComponent<TextMeshPro>("")
        // levelText.text = "level: " + level; 
        // UpdateText();
    }
    void Update()
    {

    }

}