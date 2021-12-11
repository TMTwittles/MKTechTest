using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    [SerializeField]
    private GraphicRaycaster UIRaycaster;

    private PointerEventData interactData;
    private List<RaycastResult> interactResults;

    void Start()
    {
        interactData = new PointerEventData(EventSystem.current);
        interactResults = new List<RaycastResult>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Click();
        }
    }

    void Click()
    {
        interactData.position = Mouse.current.position.ReadValue();
        interactResults.Clear();
        UIRaycaster.Raycast(interactData, interactResults);

        // Iterate through each UI object hit by UIRaycaster
        foreach (RaycastResult result in interactResults)
        {
            if (result.gameObject.CompareTag("SelectColor"))
            {
                result.gameObject.GetComponent<UIButton>().Press();
            }
        }
    }


}
