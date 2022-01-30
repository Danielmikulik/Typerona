using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScrollRectEnsureVisible : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //script to ensure dropdown is scrolling when browsing through options with arrow keys

    private RectTransform scrollRectTransform;
    private RectTransform contentPanel;
    private RectTransform selectedRectTransform;
    private GameObject lastSelected;

    private bool mouseHover;

    private Vector2 targetPos;

    private void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();

        if (contentPanel == null)
            contentPanel = GetComponent<ScrollRect>().content;  //scrolling content

        targetPos = contentPanel.anchoredPosition;
    }

    private void Update()
    {
        if (!mouseHover)
            AutoScroll();
    }

    /// <summary>
    /// scrolling textArea with mouse wheel if mouse is hovering over it.
    /// </summary>
    public void AutoScroll()
    {
        if (contentPanel == null)
        {
            contentPanel = GetComponent<ScrollRect>().content;
        }

        GameObject selected = EventSystem.current.currentSelectedGameObject;

        if (selected == null)
        {
            return;
        }
        if (selected.transform.parent != contentPanel.transform)
        {
            return;
        }
        if (selected == lastSelected)
        {
            return;
        }

        selectedRectTransform = (RectTransform)selected.transform;
        targetPos.x = contentPanel.anchoredPosition.x;
        targetPos.y = -(selectedRectTransform.localPosition.y) - (selectedRectTransform.rect.height / 2);
        targetPos.y = Mathf.Clamp(targetPos.y, 0, contentPanel.sizeDelta.y - scrollRectTransform.sizeDelta.y);

        contentPanel.anchoredPosition = targetPos;
        lastSelected = selected;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseHover = false;
    }
}