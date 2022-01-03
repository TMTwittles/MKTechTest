using System.Collections.Generic;
using MKTechTest.Assets.Scripts.Menus;
using UnityEngine;

namespace MKTechTest.Assets.Scripts
{
    public class CanvasController : MonoBehaviour
    {

        [SerializeField] private List<Menu> menuTypes;

        public List<Menu> MenuTypes
        {
            get { return menuTypes; }
        }

        private GameObject[] menusInstantiated;
        private GameObject[] menusInitial;
        private MenuID activeMenuID;
        private MenuID previousMenuID;

        void Awake()
        {
            menusInitial = new GameObject[(int)MenuID.ResultsMenu + 1];
            menusInstantiated = new GameObject[menusInitial.Length];

            foreach (Menu menu in menuTypes)
            {
                AssignMenuType(menu);
                menusInstantiated[(int) menu.ID] = null;
            }
        }

        // Assigns a menu to the menus array based of its ID
        private void AssignMenuType(Menu menu)
        {
            switch (menu.ID)
            {
                case MenuID.StartMenu:
                    menusInitial[(int)MenuID.StartMenu] = menu.gameObject;
                    break;

                case MenuID.GameMenu:
                    menusInitial[(int)MenuID.GameMenu] = menu.gameObject;
                    break;

                case MenuID.PauseMenu:
                    menusInitial[(int)MenuID.PauseMenu] = menu.gameObject;
                    break;

                case MenuID.AccessibilityMenu:
                    menusInitial[(int)MenuID.AccessibilityMenu] = menu.gameObject;
                    break;

                case MenuID.ResultsMenu:
                    menusInitial[(int)MenuID.ResultsMenu] = menu.gameObject;
                    break;
            }
        }

        /// <summary>
        /// Instantiates a menu, will return an error if menu already exists.
        /// </summary>
        /// <param name="menuID">Enum, Type of menu to instantiate</param>
        public void InstantiateMenu(MenuID menuID)
        {
            if (menusInstantiated[(int) menuID] == null)
            {
                menusInstantiated[(int) menuID] = Instantiate(menusInitial[(int) menuID], transform); // Store newly created game object
                previousMenuID = activeMenuID;
                activeMenuID = menuID;
            }
            else
            {
                Debug.LogErrorFormat("ERROR: Trying to instantiate {0} that has already been instantiated!", menuID);
            }
        }

        /// <summary>
        /// Destroys a menu, will return an error if menu does not exists.
        /// </summary>
        /// <param name="menuID">Enum, Type of menu to destroy</param>
        public void DestroyMenu(MenuID menuID)
        {
            if (menusInstantiated[(int)menuID] != null)
            {
                Destroy(menusInstantiated[(int)menuID]);
                menusInstantiated[(int)menuID] = null; // Menu is no longer in the scene
            }
            else
            {
                Debug.LogErrorFormat("ERROR: Trying to delete {0} that does not exist!", menuID);
            }
        }

        /// <summary>
        /// Enables a menu, will return an error if menu does not exist.
        /// </summary>
        /// <param name="menuID">Enum, Type of menu to enable</param>
        public void EnableMenu(MenuID menuID)
        {
            if (menusInstantiated[(int) menuID] != null)
            {
                menusInstantiated[(int) menuID].SetActive(true);
                previousMenuID = activeMenuID;
                activeMenuID = menuID;
            }
            else
            {
                Debug.LogErrorFormat("ERROR: Trying to enable {0} that has not been instantiated!", menuID);
            }
        }

        /// <summary>
        /// Disables a menu, will return an error if menu does not exist.
        /// </summary>
        /// <param name="menuID">Enum, Type of menu to disable</param>
        public void DisableMenu(MenuID menuID)
        {
            if (menusInstantiated[(int)menuID] != null)
            {
                menusInstantiated[(int)menuID].SetActive(false);
            }
            else
            {
                Debug.LogErrorFormat("ERROR: Trying to disable {0} that does not exist!", menuID);
            }
        }

        /// <summary>
        /// Returns the current active menu.
        /// </summary>
        /// <returns> Menu object of the active menu </returns>
        public Menu GetActiveMenu()
        {
            return menusInstantiated[(int) activeMenuID].GetComponent<Menu>();
        }

        /// <summary>
        /// Get the current active menu ID
        /// </summary>
        /// <returns>Enum menu ID of the active menu </returns>
        public MenuID GetActiveMenuID()
        {
            return activeMenuID;
        }

        /// <summary>
        /// Get the previously active menu ID
        /// </summary>
        /// <returns>Enum menu ID of the previous menu </returns>
        public MenuID GetPreviousMenuID()
        {
            return previousMenuID;
        }

        /// <summary>
        /// Checks if the menu has been instantiated
        /// </summary>
        /// <param name="menuID">ID of the menu</param>
        /// <returns>Boolean value</returns>
        public bool IsMenuInstantiated(MenuID menuID)
        {
            return menusInstantiated[(int) menuID] != null;
        }

        /// <summary>
        /// Check if the menu is the current active menu
        /// </summary>
        /// <param name="menuID">ID of the menu</param>
        /// <returns>Boolean value</returns>
        public bool IsMenuActive(MenuID menuID)
        {
            return menusInstantiated[(int)menuID] != null && menusInstantiated[(int)menuID].activeInHierarchy;
        }
    }
}
