using System;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace MKTechTest.Assets.Scripts.Menus
{
    public class ResultsMenu : ColoredMenu
    {
        [Header("UI Elements")]
        [SerializeField] private Text numSuccessfulAttempts;
        [SerializeField] private Text numFailedAttempts;
        [SerializeField] private Text averageAttemptTime;
        [SerializeField] private Text totalTime;
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button accessibilityButton;
        [SerializeField] private Button quitButton;
    
        [Header("Game Data")]
        [SerializeField] private PlayerData playerData;

        private String FormattedTime(float timeValue)
        {
            return TimeSpan.FromSeconds(timeValue).ToString(@"ss\:ff");
        }

        private void Awake()
        {
            newGameButton.onClick.AddListener(delegate { OnPressNewGame(); });
            accessibilityButton.onClick.AddListener(delegate{OnPressAccessibility();});
            quitButton.onClick.AddListener(delegate{OnPressQuit();});
            InitializeColorGroups();
        }

        private void OnEnable()
        {
            Reset();
        }

        public override void Reset()
        {
            UpdateResults();
            UpdateColorGroupsRandomly();
        }

        // Results are update based off player data
        private void UpdateResults()
        {
            numSuccessfulAttempts.text = playerData.SuccessfulAttempts.ToString();
            numFailedAttempts.text = playerData.FailedAttempts.ToString();
            if (playerData.TotalTime > 0.0f)
            {
                averageAttemptTime.text = FormattedTime(playerData.TotalTime / playerData.NumAttempts);
            }
            else
            {
                averageAttemptTime.text = FormattedTime(0.0f);
            }
            totalTime.text = FormattedTime(playerData.TotalTime);
        }

        private void OnPressNewGame()
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
