using System.Collections;
using MKTechTest.Assets.Scripts;
using MKTechTest.Assets.Scripts.Menus;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MKTechTest.Assets.Testing.PlayModeTesting
{
    public class GameMenuTest
    {
        [UnityTest]
        public IEnumerator Display_text_and_color_is_different()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SetActiveMenu(MenuID.GameMenu, true);
            GameMenu gameMenu = gameManager.GetActiveMenu().GetComponent<GameMenu>();

            yield return null;

            Color displayColor = Color.black;
            bool colorTextDifferent = true;
            int numOptionButtons = gameMenu.OptionButtons.Count;

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                displayColor = gameManager.GameData.CustomRandomColors.GetColorData(gameMenu.DisplayText.text).ColorRGB;

                if (displayColor == gameMenu.DisplayText.color)
                {
                    colorTextDifferent = false;
                    break;
                }

                gameMenu.GetOptionButton(Random.Range(0, numOptionButtons)).OnPressed();
            }

            Assert.IsTrue(colorTextDifferent);
        }

        [UnityTest]
        public IEnumerator Option_button_text_and_color_are_unique()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SetActiveMenu(MenuID.GameMenu, true);
            GameMenu gameMenu = gameManager.GetActiveMenu().GetComponent<GameMenu>();

            yield return null;

            bool optionButtonsUnique = true;
            Text optionButtonText = null;
            Text optionButtonText01 = null;
            int numOptionButtons = gameMenu.OptionButtons.Count;

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                for (int j = 0; j < numOptionButtons; j++)
                {
                    optionButtonText = gameMenu.OptionButtons[j].GetComponent<Text>();

                    for (int k = 0; k < numOptionButtons; k++)
                    {
                        if (k != j)
                        {
                            optionButtonText01 = gameMenu.OptionButtons[k].GetComponent<Text>();

                            if (optionButtonText.text == optionButtonText01.text
                                || optionButtonText.color == optionButtonText01.color)
                            {
                                optionButtonsUnique = false;
                            }
                        }
                    }
                }

                gameMenu.GetOptionButton(Random.Range(0, numOptionButtons)).OnPressed();
            }

            Assert.IsTrue(optionButtonsUnique);
        }

        [UnityTest]
        public IEnumerator Valid_option_button_configuration()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SetActiveMenu(MenuID.GameMenu, true);
            GameMenu gameMenu = gameManager.GetActiveMenu().GetComponent<GameMenu>();

            yield return null;

            bool optionButtonsValid = true;
            string displayColorName = "";
            int numCorrectOptions = 0;
            int numOptionButtons = gameMenu.OptionButtons.Count;

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                displayColorName = gameMenu.DisplayText.text;

                for (int j = 0; j < numOptionButtons; j++)
                {
                    if (displayColorName == gameMenu.OptionButtons[j].GetComponent<Text>().text)
                    {
                        numCorrectOptions += 1;
                    }
                }

                optionButtonsValid = numCorrectOptions == 1 ? true : false;
                numCorrectOptions = 0;
                gameMenu.GetOptionButton(Random.Range(0, numOptionButtons)).OnPressed();
            }

            Assert.IsTrue(optionButtonsValid);
        }

        [UnityTest]
        public IEnumerator Able_to_press_correct_button()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SetActiveMenu(MenuID.GameMenu, true);
            GameMenu gameMenu = gameManager.GetActiveMenu().GetComponent<GameMenu>();

            yield return null;

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                gameMenu.GetOptionButton(gameMenu.CorrectButtonIteration).OnPressed();
            }

            Assert.AreEqual(gameManager.GameData.TotalAttempts, gameManager.PlayerData.SuccessfulAttempts);
        }

        [UnityTest]
        public IEnumerator Able_to_press_incorrect_and_correct_buttons()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SetActiveMenu(MenuID.GameMenu, true);
            GameMenu gameMenu = gameManager.GetActiveMenu().GetComponent<GameMenu>();

            yield return null;

            int correctAttempts = 0;
            int failedAttempts = 0;
            int randomIndex = 0;
            int numOptionButtons = gameMenu.OptionButtons.Count;
            string selectedColor = "";

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                randomIndex = Random.Range(0, numOptionButtons);

                selectedColor = gameMenu.OptionButtons[randomIndex].GetComponent<Text>().text;

                if (selectedColor == gameMenu.CorrectColor)
                {
                    correctAttempts += 1;
                }
                else
                {
                    failedAttempts += 1;
                }

                gameMenu.GetOptionButton(randomIndex).OnPressed();
            }

            Assert.IsTrue((failedAttempts == gameManager.PlayerData.FailedAttempts &&
                           correctAttempts == gameManager.PlayerData.SuccessfulAttempts));
        
        }

        [UnityTest]
        public IEnumerator Stopwatch_updates_playerstats_during_gameplay()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SetActiveMenu(MenuID.GameMenu, true);
            GameMenu gameMenu = gameManager.GetActiveMenu().GetComponent<GameMenu>();

            yield return null;

            float timeOffset = Time.time;
            int numOptionButtons = gameMenu.OptionButtons.Count;
            float randomWaitTime = 0.0f;
            float totalTime = 0.0f;

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                randomWaitTime = Random.Range(0.0f, 1.0f);
                totalTime += randomWaitTime;
                yield return new WaitForSeconds(randomWaitTime);

                gameMenu.GetOptionButton(Random.Range(0, numOptionButtons)).OnPressed();
            }

            // Total time will have an incredibly small amount of time behind player time, thus testing with an at most difference.  
            float difference = totalTime - gameManager.PlayerData.TotalTime;
            float atMostDifference = 0.05f;

            Assert.Less(difference, atMostDifference);
        }

        [UnityTest]
        public IEnumerator Background_color_is_selected_color()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.SetActiveMenu(MenuID.GameMenu, true);
            GameMenu gameMenu = gameManager.GetActiveMenu().GetComponent<GameMenu>();
            bool initialSetting = gameManager.GameData.EnableDynamicBackground;
            gameManager.GameData.EnableDynamicBackground = true;

            yield return null;

        
            bool dynamicBackground = true;
            string selectedColor = "";
            int numOptionButtons = gameMenu.OptionButtons.Count;
            int randomButtonIndex = 0;

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                randomButtonIndex = Random.Range(0, numOptionButtons);
                selectedColor = gameMenu.OptionButtons[randomButtonIndex].GetComponent<Text>().text;
                gameMenu.GetOptionButton(randomButtonIndex).OnPressed();

                yield return null;

                if (gameManager.BackgroundColorData.ColorName != selectedColor)
                {
                    dynamicBackground = false;
                }
            }

            gameManager.GameData.EnableDynamicBackground = initialSetting;

            Assert.IsTrue(dynamicBackground);
        }
    }
}
