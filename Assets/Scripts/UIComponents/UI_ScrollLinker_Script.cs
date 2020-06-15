using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;


public class UI_ScrollLinker_Script : MonoBehaviour
{
    public enum         scrollTypes
    {
        ScrollBar   = 0,
        ScrollRect  = 1
    }
    public scrollTypes  scrollType;
    public bool         isVerticalScrolling;

    public Scrollbar    scrollBar;
    public ScrollRect   scrollRect;


    void Start ()
    {
        if (scrollType == scrollTypes.ScrollBar)
        {
            scrollBar.onValueChanged.AddListener(ScrollUpdate);
        }
        else if (scrollType == scrollTypes.ScrollRect)
        {
            scrollRect.onValueChanged.AddListener(ScrollUpdate);
        }
    }

    void ScrollUpdate (float value)
    {
        if(scrollType == scrollTypes.ScrollBar)
        {
            if(isVerticalScrolling)
            {
                scrollRect.verticalNormalizedPosition = value;
            }
            else if(!isVerticalScrolling)
            {
                scrollRect.horizontalNormalizedPosition = value;
            }
        }
    }
    void ScrollUpdate (Vector2 valueV2)
    {
        float value = 0;
        if(scrollType == scrollTypes.ScrollRect)
        {
            if(isVerticalScrolling)
            {
                value = valueV2.y;
            }
            else if(!isVerticalScrolling)
            {
                value = valueV2.x;
            }
            scrollBar.value = value;
        }
    }
}
