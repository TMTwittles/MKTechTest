using System;
using UnityEngine;
using UnityEngine.UI;

namespace MKTechTest.Assets.Scripts.Menus
{
    public class StartMenu : ColoredMenu
    {
        [Header("UI Elements")]
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button accessibilityButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            newGameButton.onClick.AddListener(delegate { OnPressNewGame(); });
            accessibilityButton.onClick.AddListener(delegate { OnPressAccessibility(); });
            quitButton.onClick.AddListener(delegate {OnPressQuit();});
            InitializeColorGroups();
        }

        private void OnEnable()
        {
            Reset();
        }

        public override void Reset()
        {
            UpdateColorGroupsRandomly();
        }

        private void OnPressNewGame()
        {
            GameManager.Instance.SetActiveMenu(MenuID.GameMenu, true);
        }

        private void OnPressAccessibility()
        {
            GameManager.Instance.SetActiveMenu(MenuID.AccessibilityMenu, false);
        }

        private void OnPressQuit()
        {
            GameManager.Instance.Quit();
        }
    }
}
