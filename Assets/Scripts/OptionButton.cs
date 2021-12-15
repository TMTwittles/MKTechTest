using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    private Button buttonComponent;
    private Text textComponent;
    public event Action<string> optionButtonPressed;
    
    void Start()
    {
        buttonComponent = GetComponent<Button>();
        textComponent = GetComponent<Text>();
        buttonComponent.onClick.AddListener(delegate{OnPressed();});
    }

    void OnPressed()
    {
        optionButtonPressed.Invoke(textComponent.text);
    }
    
}
