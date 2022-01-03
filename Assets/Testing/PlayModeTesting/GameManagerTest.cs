using System.Collections;
using MKTechTest.Assets.Scripts;
using MKTechTest.Assets.Scripts.Menus;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

namespace MKTechTest.Assets.Testing.PlayModeTesting
{
    public class GameManagerTest
    {
        [UnityTest]
        public IEnumerator Set_active_menu_and_disable_current_menu()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject canvasGameObject = GameObject.Find("Canvas");
            CanvasController canvasController = canvasGameObject.GetComponent<CanvasController>();

            yield return null;

            bool menusAreActive = true;
            bool menusGetDisabled = true;

            string activeMenuName = "";

            activeMenuName = canvasController.GetActiveMenu().gameObject.name;
            int transformIndex = canvasGameObject.transform.childCount - 1;

            foreach (Menu menu in canvasController.MenuTypes)
            {
                if (menu.ID != MenuID.StartMenu)
                {
                    if (!GameObject.Find(activeMenuName).activeInHierarchy)
                    {
                        menusAreActive = false;
                    }

                    gameManager.SetActiveMenu(menu.ID, false);

                    if (canvasGameObject.transform.GetChild(transformIndex).gameObject.activeInHierarchy)
                    {
                        menusGetDisabled = false;
                    }

                    activeMenuName = canvasController.GetActiveMenu().gameObject.name;
                    transformIndex += 1;
                }
            }

            Assert.IsTrue(menusGetDisabled && menusAreActive);
        }

        [UnityTest]
        public IEnumerator Set_active_menu_and_destroy_current_menu()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject canvasGameObject = GameObject.Find("Canvas");
            CanvasController canvasController = canvasGameObject.GetComponent<CanvasController>();

            yield return null;

            bool menusAreActive = true;
            bool menusGetDestroyed = true;

            string activeMenuName = "";
            activeMenuName = canvasController.GetActiveMenu().gameObject.name;

            foreach (Menu menu in canvasController.MenuTypes)
            {
                if (menu.ID != MenuID.StartMenu)
                {
                    if (!GameObject.Find(activeMenuName).activeInHierarchy)
                    {
                        menusAreActive = false;
                    }

                    gameManager.SetActiveMenu(menu.ID, true);

                    yield return null;

                    if (canvasGameObject.transform.childCount != 1)
                    {
                        menusGetDestroyed = false;
                    }

                    activeMenuName = canvasController.GetActiveMenu().gameObject.name;
                }
            }

            Assert.IsTrue(menusGetDestroyed && menusAreActive);
        }

        [UnityTest]
        public IEnumerator Go_back_to_previous_active_menu()
        {
            SceneManager.LoadScene("SampleScene");

            yield return null;

            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            CanvasController canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();

            yield return null;

            bool canGoBack = true;

            int numTests = 10;
            int numMenus = canvasController.MenuTypes.Count;
            string activeMenuName = "";
            activeMenuName = canvasController.GetActiveMenu().gameObject.name;

            MenuID previousMenuID = canvasController.GetActiveMenuID();
            MenuID newID = MenuID.StartMenu;

            for (int i = 0; i < numTests; i++)
            {
                newID = canvasController.MenuTypes[Random.Range(0, numMenus)].ID;
                gameManager.SetActiveMenu(newID, true);

                yield return null;

                gameManager.GoBack(true);

                yield return null;

                if (previousMenuID != canvasController.GetActiveMenuID())
                {
                    canGoBack = false;
                    break;
                }

                gameManager.SetActiveMenu(newID, true);
                previousMenuID = newID;

                yield return null;

            }

            Assert.IsTrue(canGoBack);
        }
    }
}
