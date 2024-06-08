using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoPanelLevel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public GameObject HoverPanel;


   public void OnPointerEnter(PointerEventData eventData)
   {
        HoverPanel.SetActive(true);
   }

   public void OnPointerExit(PointerEventData eventData)
   {
        HoverPanel.SetActive(false);
   }
}
