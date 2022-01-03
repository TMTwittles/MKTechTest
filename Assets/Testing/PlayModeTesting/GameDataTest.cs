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

            for (int i = 0; i < gameManager.GameData.TotalAttempts; i++)
            {
                gameMenu.GetOptionButton(Random.Range(0, numOptionsButtons)).OnPressed();

                yield return null;

                if (gameManager.BackgroundColorData != backgroundColorData)
                {
                    disableDynamicBackground = false;
                }
            }

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

//    [UnityTest]
//    public Test ResetCustomColors()
//    {
//    SceneManager.LoadScene("SampleScene");

//    yield return null;

//    GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


//    yield return null;

//    bool colorsReset = true;
//    Color randomColor = new Color(1.0f, 1.0f, 1.0f);


//        foreach (ColorData colorData in gameManager.GameData.CustomRandomColors.ColorList)
//    {
//        randomColor.r = Random.Range(0.0f, 1.0f);
//        randomColor.g = Random.Range(0.0f, 1.0f);
//        randomColor.b = Random.Range(0.0f, 1.0f);

//        colorData.SetColorRGB(randomColor);
//    }

//    foreach (ColorData colorData in gameManager.GameData.CustomRandomColors.ColorList)
//    {
//    colorData.SetColorRGB(gameManager.GameData.DefaultRandomColors.GetColorData(colorData.ColorName).ColorRGB);
//    }

//    foreach (ColorData colorData in gameManager.GameData.CustomRandomColors.ColorList)
//    {

//    }


//    Assert.IsTrue(disableDynamicBackground);
//}
}
