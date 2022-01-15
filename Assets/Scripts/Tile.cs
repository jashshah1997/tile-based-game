using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] 
    private Color m_baseColor, m_offsetColor;

    [SerializeField] 
    private SpriteRenderer m_renderer;

    [SerializeField]
    private GameObject m_highlight;

    public void Init(bool isOffset)
    {
        m_renderer.color = isOffset ? m_offsetColor : m_baseColor;
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