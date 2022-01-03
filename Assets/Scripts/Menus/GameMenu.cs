using System.Collections.Generic;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MKTechTest.Assets.Scripts.Menus
{
    public class GameMenu : Menu
    {
        [Header("UI Elements")]
        [SerializeField] private Text timerText;
        [SerializeField] private Text displayText;
        public Text DisplayText
        {
            get { return displayText; }
        }

        [SerializeField] private List<GameObject> optionButtons;
        public List<GameObject> OptionButtons
        {
            get { return optionButtons; }
        }
        [SerializeField] private Button pauseButton;

        [Header("Game Data")] 
        [SerializeField] private PlayerData playerData;
        [SerializeField] private GameData gameData;
        [SerializeField] private RandomColors randomColorsTextOnly;
        private Text[] optionButtonTexts; // Faster access to option button text
        private StopWatch stopWatch;
        private string correctColor;

        public string CorrectColor
        {
            get { return correctColor; }
        }

        private bool successfulAttempt = false;
        private int correctButtonIteration;

        public int CorrectButtonIteration
        {
            get { return correctButtonIteration; }
        }

        private void Awake()
        {
            pauseButton.onClick.AddListener(delegate {OnPressPause();});
        
            stopWatch = GetComponent<StopWatch>();
            optionButtonTexts = new Text[optionButtons.Count];
            int iter = 0;
            foreach (GameObject optionButton in optionButtons)
            {
                optionButton.GetComponent<OptionButton>().optionButtonPressed += OptionButtonPressed;
                optionButtonTexts[iter] = optionButton.GetComponent<Text>();
                iter += 1;
            }

            Reset();
        }

        private void OnEnable()
        {
            stopWatch.TurnOn();
        }

        public override void Reset()
        {
            playerData.ResetResults();
            stopWatch.Reset();
            stopWatch.TurnOn();
            randomColorsTextOnly.Reset();
            ResetRandomColors();
            Shuffle();
        }

        void Update()
        {
            timerText.text = stopWatch.GetTimeFormatted();
        }

        private void OnPressPause()
        {
            stopWatch.Pause();
            GameManager.Instance.SetActiveMenu(MenuID.PauseMenu, false);
        }

        // Method determines if the player selected the right color
        private void OptionButtonPressed(string selectedColor)
        {
            successfulAttempt = selectedColor == correctColor;

            if (data.EnableDynamicBackground)
            {
                data.CustomRandomColors.Reset(); // Reset all visual colors
                data.CustomRandomColors.IgnoreColor(selectedColor); // Ignore the color selected by the player as this will be the background color
                GameManager.Instance.SetBackgroundColor(data.CustomRandomColors.GetColorData(selectedColor));
            }
            else
            {
                ResetRandomColors();
            }
            randomColorsTextOnly.Reset();

            if (playerData.NumAttempts >= gameData.TotalAttempts - 1)
            {
                playerData.AddAttempt(successfulAttempt, stopWatch.ElapsedTime);
                GameManager.Instance.ShowResults();
            }
            else
            {
                playerData.AddAttempt(successfulAttempt);
                Shuffle(); // Randomize the display and option buttons as a selection has been made.
            }
        }

        private void Shuffle()
        {
            // Assign a new color to the display button, this will be the correct answer
            correctColor = randomColorsTextOnly.GetRandomColor().ColorName;

            // Ignore the correct color from all random color options
            randomColorsTextOnly.IgnoreColor(correctColor); 
            data.CustomRandomColors.IgnoreColor(correctColor);

            string randomName = "";
            ColorData randomColorData = null;
            int iter = 0;
            correctButtonIteration = Random.Range(0, optionButtonTexts.Length);

            // Apply values to the display button
            displayText.text = correctColor;
            randomColorData = data.CustomRandomColors.GetRandomColor();
            displayText.color = randomColorData.ColorRGB;

            data.CustomRandomColors.IgnoreColor(randomColorData.ColorName);
        
            // Apply new options to each option button in the menu
            foreach (Text optionButton in optionButtonTexts)
            {
                if (iter != correctButtonIteration)
                {
                    randomName = randomColorsTextOnly.GetRandomColor().ColorName;
                    randomColorsTextOnly.IgnoreColor(randomName);
                    optionButton.text = randomName;
                }
                else
                {
                    optionButton.text = correctColor; // Apply correct color as it is the correct iteration.
                }

                // Apply a random color from remaining visual colors
                randomColorData = data.CustomRandomColors.GetRandomColor();
                data.CustomRandomColors.IgnoreColor(randomColorData.ColorName);
                optionButton.color = randomColorData.ColorRGB;

                iter += 1;
            }
        
            // Once option buttons have been assigned, apply a random remaining color to the timer text and pause text
            timerText.color = data.CustomRandomColors.GetRandomColor().ColorRGB;
            pauseButton.gameObject.GetComponent<Text>().color = data.CustomRandomColors.GetRandomColor().ColorRGB;
        }

        /// <summary>
        /// Gets an option button from the game menu.
        /// </summary>
        /// <param name="index">Index of the option button to return</param>
        /// <returns>Button component of the option button</returns>
        public OptionButton GetOptionButton(int index)
        {
            return optionButtons[index].GetComponent<OptionButton>();
        }
    }
}
