using System;
using UnityEngine;
using UnityEngine.UI;

namespace MKTechTest.Assets.Scripts
{
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

        public void OnPressed()
        {
            optionButtonPressed.Invoke(textComponent.text);
        }
    }
}
