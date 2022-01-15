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

    [SerializeField]
    private Color m_baseColor, m_offsetColor, m_fullColor, m_halfColor, m_qurterColor, m_noneColor;

    [SerializeField] 
    private SpriteRenderer m_renderer;

    [SerializeField]
    private GameObject m_highlight;

    public void Init(bool isOffset)
    {
        m_renderer.color = isOffset ? m_offsetColor : m_baseColor;
        m_goldStatusDict = new Dictionary<int, Color>();
        m_goldStatusDict[GoldStatus.NONE] = m_noneColor;
        m_goldStatusDict[GoldStatus.QUARTER] = m_qurterColor;
        m_goldStatusDict[GoldStatus.HALF] = m_halfColor;
        m_goldStatusDict[GoldStatus.FULL] = m_fullColor;
    }

    public bool isOccupied()
    {
        return m_goldStatus > GoldStatus.NONE;
    }

    public void setGoldStatus(int goldStatus)
    {
        m_goldStatus = goldStatus;
        m_renderer.color = m_goldStatusDict[m_goldStatus];
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

}