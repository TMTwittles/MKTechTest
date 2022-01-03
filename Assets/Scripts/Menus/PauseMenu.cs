using UnityEngine;
using UnityEngine.UI;

namespace MKTechTest.Assets.Scripts.Menus
{
    public class PauseMenu : ColoredMenu
    {
        [Header("UI Elements")] 
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button accessibilityButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(delegate {OnPressResume();});
            retryButton.onClick.AddListener(delegate {OnPressRetry();});
            accessibilityButton.onClick.AddListener(delegate{OnPressAccessibility();});
            InitializeColorGroups();
        }

        private void OnEnable()
        {
            Reset();
        }

        public override void Reset()
        {
            ResetRandomColors();
            UpdateColorGroupsRandomly();
        }

        private void OnPressResume()
        {
            GameManager.Instance.SetActiveMenu(MenuID.GameMenu, true);
        }

        private void OnPressRetry()
        {
            GameManager.Instance.SetActiveMenu(MenuID.GameMenu, true);
            GameManager.Instance.ResetActiveMenu();
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
