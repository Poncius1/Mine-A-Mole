using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform Button;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Button.GetComponent<Animator>().Play("Start_Off");
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Button.GetComponent<Animator>().Play("Start_On");
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        Button.GetComponent<Animator>().Play("Start_Off");
    }
}
