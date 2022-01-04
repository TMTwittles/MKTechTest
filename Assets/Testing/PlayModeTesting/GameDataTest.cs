using System.Collections;
using MKTechTest.Assets.Scripts;
using MKTechTest.Assets.Scripts.Menus;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MKTechTest.Assets.Testing.PlayModeTesting
{
    public class GameDataTest
    {
        [UnityTest]
        public IEnumerator Can_disable_dynamic_background()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            CanvasController canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();

            yield return null;

            gameManager.SetActiveMenu(MenuID.GameMenu, true);

            yield return null;

            GameMenu gameMenu = (GameMenu) gameManager.GetActiveMenu();

            bool disableDynamicBackground = true;
            bool initialSetting = gameManager.GameData.EnableDynamicBackground;
            gameManager.GameData.EnableDynamicBackground = false;
            ColorData backgroundColorData = gameManager.GameData.CustomRandomColors.GetRandomColor();
            gameManager.SetBackgroundColor(backgroundColorData);

            yield return null;

            int numOptionsButtons = gameMenu.OptionButtons.Count;

            // iterate through an entire game session and ensure background remains the same.
            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                gameMenu.GetOptionButton(Random.Range(0, numOptionsButtons)).OnPressed();

                yield return null;

                if (gameManager.BackgroundColorData != backgroundColorData)
                {
                    disableDynamicBackground = false;
                }
            }

            // Iterate through each menu and ensure the background color remains the same.
            foreach (Menu menu in canvasController.MenuTypes)
            {
                gameManager.SetActiveMenu(menu.ID, true);

                if (gameManager.BackgroundColorData != backgroundColorData)
                {
                    disableDynamicBackground = false;
                }
            }
            gameManager.GameData.EnableDynamicBackground = initialSetting;

            Assert.IsTrue(disableDynamicBackground);
        }
    }
}
