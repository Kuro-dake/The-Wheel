using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool clicked{ get; set; }

    RectTransform rt => GetComponent<RectTransform>();
    private Vector3 orig_pos;
    void Start()
    {
        orig_pos = rt.anchoredPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(clicked)
        {
            rt.anchoredPosition = Vector3.MoveTowards(rt.anchoredPosition, orig_pos, Time.deltaTime * 150f);
        }
        else
        {
            rt.anchoredPosition = Vector3.MoveTowards(rt.anchoredPosition, Vector3.up * 100, Time.deltaTime * 150f);
        }
        cg.alpha = Mathf.MoveTowards(cg.alpha, mouse_over ? 1f : .5f, Time.deltaTime * 2f);
    }

    CanvasGroup cg => GetComponent<CanvasGroup>();
    private bool mouse_over;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
}
