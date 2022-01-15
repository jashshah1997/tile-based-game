using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GoldStatus
{
    public static int FULL = 4;
    public static int HALF = 3;
    public static int QUARTER = 2;
    public static int NONE = 1;
};

public class Tile : MonoBehaviour
{
    private int m_goldStatus = GoldStatus.NONE;
    private int m_goldAmount = 0;
    private Dictionary<int, Color> m_goldStatusDict;
    private bool m_hidden = true;
    private int m_x, m_y;
    private GameObject m_gameManager;

    [SerializeField]
    private Color m_baseColor, m_offsetColor, m_fullColor, m_halfColor, m_qurterColor, m_noneColor;

    [SerializeField] 
    private SpriteRenderer m_renderer;

    [SerializeField]
    private GameObject m_highlight;
    
    public void Init(bool isOffset, int x, int y)
    {
        m_x = x;
        m_y = y;
        m_gameManager = GameObject.Find("GameManager");
        m_renderer.color = isOffset ? m_offsetColor : m_baseColor;
        m_goldStatusDict = new Dictionary<int, Color>();
        m_goldStatusDict[GoldStatus.NONE] = m_noneColor;
        m_goldStatusDict[GoldStatus.QUARTER] = m_qurterColor;
        m_goldStatusDict[GoldStatus.HALF] = m_halfColor;
        m_goldStatusDict[GoldStatus.FULL] = m_fullColor;
    }

    public void revealTile()
    {
        if (!m_hidden) return;
        m_hidden = false;
        m_renderer.color = m_goldStatusDict[m_goldStatus];
    }

    public int extractTile(bool fullExtract = false)
    {
        // Reveal the tile after extracting
        m_hidden = false;

        int extractedGold = 0;
        if (m_goldStatus == GoldStatus.NONE)
        {
            extractedGold = 0;
        }
        else if (m_goldStatus == GoldStatus.QUARTER)
        {
            extractedGold = m_goldAmount / 4;
        }
        else if (m_goldAmount == GoldStatus.HALF)
        {
            extractedGold = m_goldAmount / 2;
        } 
        else if (m_goldAmount == GoldStatus.FULL)
        {
            extractedGold = m_goldAmount;
        }

        if (fullExtract)
        {
            m_goldStatus = GoldStatus.NONE;
        } else
        {
            m_goldStatus--;
        }
        
        if (m_goldStatus < GoldStatus.NONE) m_goldStatus = GoldStatus.NONE;
        m_renderer.color = m_goldStatusDict[m_goldStatus];


        return extractedGold;
    }

    public bool isOccupied()
    {
        return m_goldStatus > GoldStatus.NONE;
    }

    public void setGoldStatus(int goldStatus)
    {
        m_goldStatus = goldStatus;
        // m_renderer.color = m_goldStatusDict[m_goldStatus];
    }

    public void setGoldAmount(int goldAmount)
    {
        m_goldAmount = goldAmount;
    }

    void OnMouseEnter()
    {
        m_highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        m_highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        m_gameManager.GetComponent<GameManager>().onGridTileClicked(m_x, m_y);
    }
}