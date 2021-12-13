using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private UnityEvent buttonPressedEvent;
    private Text buttonText;

    public void Start()
    {
        buttonText = GetComponent<Text>();
    }

    public void Press()
    {
        buttonPressedEvent.Invoke();
    }

    public void ChangeButtonVisual(String newColorName, Color newColor)
    {
        buttonText.text = newColorName;
        buttonText.color = new Color(newColor.r, newColor.g, newColor.b);
    }
}




