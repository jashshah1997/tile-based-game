using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameModes
{
    public static int SCAN = 1;
    public static int EXTRACT = 2;
};

public class GameManager : MonoBehaviour
{
    private GameObject m_modeButton;
    private GameObject m_scoreField;
    private GameObject m_messageField;
    private int m_gameMode = GameModes.EXTRACT;

    // Start is called before the first frame update
    void Start()
    {
        m_modeButton = GameObject.Find("ModeButton");
        m_scoreField = GameObject.Find("ScoreField");
        m_messageField = GameObject.Find("MessageField");

        m_modeButton.GetComponent<Button>().onClick.AddListener(onModeButtonClick);
        m_scoreField.GetComponent<Text>().text = "0";
        m_modeButton.GetComponentInChildren<Text>().text = "Extract";
        m_messageField.GetComponent<Text>().text = "Welcome to the game! Scan fields to find Gold, extract gold to score points.";
    }

    void onModeButtonClick()
    {
        if (m_gameMode == GameModes.SCAN)
        {
            m_gameMode = GameModes.EXTRACT;
            m_modeButton.GetComponentInChildren<Text>().text = "Extract";
        }
        else
        {
            m_gameMode = GameModes.SCAN;
            m_modeButton.GetComponentInChildren<Text>().text = "Scan";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
