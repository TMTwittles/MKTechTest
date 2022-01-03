using System.Collections;
using System.Collections.Generic;
using MKTechTest.Assets.Scripts;
using MKTechTest.Assets.Scripts.Menus;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace MKTechTest.Assets.Testing.PlayModeTesting
{
    public class ColoredMenuTest
    {
        [UnityTest]
        public IEnumerator Colorgroups_are_unique()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            CanvasController canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();

            yield return null;

            bool colorsDifferent = true;
            ColoredMenu activeColoredMenu;
            Color colorGroupColor = Color.black;
            int numColorGroups = 0;

            foreach (Menu menu in canvasController.MenuTypes)
            {
                if (menu.GetComponent<ColoredMenu>())
                {
                    gameManager.SetActiveMenu(menu.ID, true);

                    yield return null;

                    activeColoredMenu = (ColoredMenu)gameManager.GetActiveMenu();
                    numColorGroups = activeColoredMenu.ColorGroups.Count;

                    for (int i = 0; i < numColorGroups; i++)
                    {
                        colorGroupColor = activeColoredMenu.ColorGroups[i][0].color;

                        for (int j = 0; j < numColorGroups; j++)
                        {
                            if (j != i)
                            {
                                if (colorGroupColor == activeColoredMenu.ColorGroups[j][0].color)
                                {
                                    colorsDifferent = false;
                                }
                            }
                        }
                    }
                }
            }

            Assert.IsTrue(colorsDifferent);
        }

        [UnityTest]
        public IEnumerator Colorgroups_different_to_background_color()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            CanvasController canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();

            yield return null;

            bool colorsDifferent = true;
            ColoredMenu activeColoredMenu;
            int numTests = 5;
        

            for (int i = 0; i < numTests; i++)
            {
                gameManager.SetBackgroundColor(gameManager.GameData.CustomRandomColors.GetRandomColor());
                yield return null;

                foreach (Menu menu in canvasController.MenuTypes)
                {
                    if (menu.GetComponent<ColoredMenu>())
                    {
                        gameManager.SetActiveMenu(menu.ID, true);

                        yield return null;

                        activeColoredMenu = (ColoredMenu)gameManager.GetActiveMenu();

                        foreach (List<Text> colorGroup in activeColoredMenu.ColorGroups)
                        {
                            foreach (Text text in colorGroup)
                            {
                                if (text.color == gameManager.BackgroundColorData.ColorRGB)
                                {
                                    colorsDifferent = false;
                                }
                            }
                        }
                    }
                }
            }

            Assert.IsTrue(colorsDifferent);
        }

    }
}
