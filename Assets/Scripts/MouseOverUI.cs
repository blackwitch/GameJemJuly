using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverUI : MonoBehaviour
{
    public GameObject descriptionUI;
    public RectTransform canvasRect;

    public void OnPointerEnter()
    {
        StartCoroutine(OpenDescriptionUI());
    }

    IEnumerator OpenDescriptionUI()
    {
        if (descriptionUI.activeInHierarchy == false)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
            descriptionUI.GetComponent<RectTransform>().anchoredPosition = anchoredPos + new Vector2(130f, -130f);
            yield return new WaitForSeconds(0.3f);
            descriptionUI.SetActive(true);
            descriptionUI.GetComponent<DescriptionUI>().Set(gameObject.name);
        }
    }
    
    public void OnPointerExit()
    {
        if (descriptionUI.activeInHierarchy)
        {
            descriptionUI.SetActive(false);
        }
    }
}
