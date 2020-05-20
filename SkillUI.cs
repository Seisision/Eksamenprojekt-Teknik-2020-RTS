using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject dBox;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dBox.SetActive(false);
    }
}
