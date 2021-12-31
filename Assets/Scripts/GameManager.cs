using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.XR;
using Random = UnityEngine.Random;

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

    public void ShowResults()
    {
        canvasController.DisableMenu(MenuID.GameMenu);

        // Check if result menu has been instantiated, if it is then enable it else instantiate it.
        if (canvasController.IsMenuInstantiated(MenuID.ResultsMenu))
        {
            canvasController.EnableMenu(MenuID.ResultsMenu);
        }
        else
        {
            canvasController.InstantiateMenu(MenuID.ResultsMenu);
        }
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
    /// <param name="destroyCurrentMenu"> bool, whether to destroy or disable the current active menu. </param>
    public void SetActiveMenu(MenuID newActiveMenuID, bool destroyCurrentMenu)
    {
        HandleActiveMenu(destroyCurrentMenu);

        if (canvasController.IsMenuInstantiated(newActiveMenuID))
            canvasController.EnableMenu(newActiveMenuID);
        else
            canvasController.InstantiateMenu(newActiveMenuID);
    }

    /// <summary>
    /// Instantiates or enables the previously active menu, destroying or deleting the active menu.
    /// </summary>
    /// <param name="destroyCurrentMenu"> bool, whether to destroy or disable the current active menu. </param>
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
    /// Instantiates or enables the game menu but also resets it to ensure a new game.
    /// </summary>
    /// <param name="destroyCurrentMenu"> bool, whether to destroy or disable the current active menu. </param>
    public void NewGame(bool destroyCurrentMenu)
    {
        SetActiveMenu(MenuID.GameMenu, destroyCurrentMenu);
        canvasController.GetActiveMenu().Reset(); // Reset results for the new game
    }

    /// <summary>
    /// Updates the background color of the camera
    /// </summary>
    /// <param name="newColorData">Color data of the new color to change the background color</param>
    public void UpdateBackgroundColor(ColorData newColorData)
    {
        backgroundColorData = newColorData;
        gameCamera.backgroundColor = newColorData.ColorRGB;
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
