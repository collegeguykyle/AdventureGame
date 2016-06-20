using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using Vectrosity;
using System.Collections.Generic;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float delay = 0.5f;
    public GameObject panel;    

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }

}