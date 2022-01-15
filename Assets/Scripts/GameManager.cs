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
    private GameObject m_exitButton;
    private GameObject m_startButton;
    private GameObject m_scoreField;
    private GameObject m_messageField;
    private GameObject m_extractField;
    private GameObject m_scanField;
    private GameObject m_gridManager;
    private GameObject m_gameUI;
    private int m_gameMode = GameModes.EXTRACT;
    private int m_currentScore = 0;

    public int ScanAttempts = 6;
    public int ExtractAttempts = 3;
    private int m_scanAttemptsMax;
    private int m_extractAttemptsMax;

    // Start is called before the first frame update
    void Start()
    {
        m_scanAttemptsMax = ScanAttempts;
        m_extractAttemptsMax = ExtractAttempts;

        m_modeButton = GameObject.Find("ModeButton");
        m_scoreField = GameObject.Find("ScoreField");
        m_messageField = GameObject.Find("MessageField");
        m_gridManager = GameObject.Find("GridManager");
        m_extractField = GameObject.Find("ExtractField");
        m_scanField = GameObject.Find("ScanField");
        m_exitButton = GameObject.Find("ExitButton");
        m_startButton = GameObject.Find("StartButton");
        m_gameUI = GameObject.Find("GameUI");

        m_modeButton.GetComponent<Button>().onClick.AddListener(onModeButtonClick);
        m_exitButton.GetComponent<Button>().onClick.AddListener(onExitButtonClick);
        m_startButton.GetComponent<Button>().onClick.AddListener(onStartButtonClick);

        updateFields();
        m_gameUI.SetActive(false);
    }

    void updateFields()
    {
        m_scoreField.GetComponent<Text>().text = "" + m_currentScore;
        m_extractField.GetComponent<Text>().text = "" + ExtractAttempts;
        m_scanField.GetComponent<Text>().text = "" + ScanAttempts;
    }

    void onStartButtonClick()
    {
        ScanAttempts = m_scanAttemptsMax;
        ExtractAttempts = m_extractAttemptsMax;
        m_gameMode = GameModes.EXTRACT;
        m_currentScore = 0;
        m_modeButton.GetComponentInChildren<Text>().text = "Extract";
        m_messageField.GetComponent<Text>().text = "Welcome to the game! Scan fields to find Gold, extract gold to score points.";
        m_startButton.SetActive(false);
        m_gridManager.GetComponent<GridManager>().GenerateGrid();
        m_gameUI.SetActive(true);
        updateFields();
    }

    void onExitButtonClick()
    {
        m_gridManager.GetComponent<GridManager>().destroyGrid();
        m_gameUI.SetActive(false);
        m_startButton.SetActive(true);
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
        if (ExtractAttempts == 0)
        {
            m_messageField.GetComponent<Text>().text = "Game over! Your final score is " + m_currentScore + ".";
            return;
        }

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
            }
            else
            {
                m_messageField.GetComponent<Text>().text = "No more extract attempts remaining!";
            }
        }
        updateFields();
        if (ExtractAttempts == 0)
        {
            m_messageField.GetComponent<Text>().text = "Game over! Your final score is " + m_currentScore + ".";
        }
    }


}
