using MKTechTest.Assets.Scripts.Menus;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Menu = MKTechTest.Assets.Scripts.Menus.Menu;

namespace MKTechTest.Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance; // static singleton instance
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        [Header("Menus")] 
        [SerializeField] private CanvasController canvasController;

        [Header("Camera")] 
        [SerializeField] private Camera gameCamera;
        private ColorData backgroundColorData;

        [Header("Game data")] 
        [SerializeField] private GameData data;
        public ColorData BackgroundColorData
        {
            get { return backgroundColorData; }
        }

        public GameData GameData
        {
            get { return data; }
        }

        [SerializeField] private PlayerData playerData;
        public PlayerData PlayerData
        {
            get { return playerData; }
        }

        private void Awake()
        {
            instance = this; // Basic singleton implementation.
        }

        private void Start()
        {
            data.InitializeCustomRandomColors();

            // Set background color
            backgroundColorData = data.CustomRandomColors.GetRandomColor();
            data.CustomRandomColors.IgnoreColor(backgroundColorData.ColorName);
            gameCamera.backgroundColor = backgroundColorData.ColorRGB;

            // Instantiate start menu
            canvasController.InstantiateMenu(MenuID.StartMenu);
        }

        private void HandleActiveMenu(bool destroyActiveMenu)
        {
            if (destroyActiveMenu)
                canvasController.DestroyMenu(canvasController.GetActiveMenuID());
            else
                canvasController.DisableMenu(canvasController.GetActiveMenuID());
        }

        /// <summary>
        /// Instantiates or enables a chosen menu, destroying or deleting the active menu.
        /// </summary>
        /// <param name="newActiveMenuID"> Enum MenuID, ID of the menu to set active </param>
        /// <param name="destroyCurrentMenu"> Bool, whether to destroy or disable the current active menu. </param>
        public void SetActiveMenu(MenuID newActiveMenuID, bool destroyCurrentMenu)
        {
            HandleActiveMenu(destroyCurrentMenu);
            if (canvasController.IsMenuInstantiated(newActiveMenuID))
                canvasController.EnableMenu(newActiveMenuID);
            else
                canvasController.InstantiateMenu(newActiveMenuID);
        }

        /// <summary>
        /// Resets the active menu to its initial state.
        /// </summary>
        public void ResetActiveMenu()
        {
            canvasController.GetActiveMenu().Reset();
        }

        /// <summary>
        /// Instantiates or enables the previously active menu, destroying or deleting the active menu.
        /// </summary>
        /// <param name="destroyCurrentMenu"> Bool, whether to destroy or disable the current active menu. </param>
        public void GoBack(bool destroyCurrentMenu)
        {
            HandleActiveMenu(destroyCurrentMenu);
            MenuID previousMenuID = canvasController.GetPreviousMenuID();

            if (canvasController.IsMenuInstantiated(previousMenuID))
                canvasController.EnableMenu(previousMenuID);
            else
                canvasController.InstantiateMenu(previousMenuID);
        }

        /// <summary>
        /// Sets the background color data of the camera
        /// </summary>
        /// <param name="newColorData">Color data of the new color to change the background color</param>
        public void SetBackgroundColor(ColorData newColorData)
        {
            backgroundColorData = newColorData;
            gameCamera.backgroundColor = newColorData.ColorRGB;
        }

        /// <summary>
        /// Gets the active menu in the scene.
        /// </summary>
        /// <returns>Menu object, returns the active menu in the scene</returns>
        public Menu GetActiveMenu()
        {
            return canvasController.GetActiveMenu();
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
    }
}
