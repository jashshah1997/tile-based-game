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
    private GameObject m_gridManager;
    private int m_gameMode = GameModes.EXTRACT;
    private int m_currentScore = 0;

    public int ScanAttempts = 6;
    public int ExtractAttempts = 3;

    // Start is called before the first frame update
    void Start()
    {
        m_modeButton = GameObject.Find("ModeButton");
        m_scoreField = GameObject.Find("ScoreField");
        m_messageField = GameObject.Find("MessageField");
        m_gridManager = GameObject.Find("GridManager");

        m_modeButton.GetComponent<Button>().onClick.AddListener(onModeButtonClick);
        updateScoreField();
        m_modeButton.GetComponentInChildren<Text>().text = "Extract";
        m_messageField.GetComponent<Text>().text = "Welcome to the game! Scan fields to find Gold, extract gold to score points.";
    }

    void updateScoreField()
    {
        m_scoreField.GetComponent<Text>().text = "" + m_currentScore;
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

    public void onGridTileClicked(int x, int y)
    {
        if (m_gameMode == GameModes.SCAN)
        {
            if (ScanAttempts > 0)
            {
                ScanAttempts--;
                m_gridManager.GetComponent<GridManager>().revealNearbyTiles(x, y);
            }
            else
            {
                m_messageField.GetComponent<Text>().text = "No more scan attempts remaining!";
            }
        }
        else if (m_gameMode == GameModes.EXTRACT)
        {
            if (ExtractAttempts > 0)
            {
                ExtractAttempts--;
                m_currentScore += m_gridManager.GetComponent<GridManager>().extractTile(x, y);
                updateScoreField();
            }
            else
            {
                m_messageField.GetComponent<Text>().text = "No more extract attempts remaining!";
            }
        }
    }

}
