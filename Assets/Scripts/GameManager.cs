using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
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
    [SerializeField] private ColorData backgroundColorData;

    [Header("Colors")] 
    [SerializeField] private RandomColors visualRandomColors;

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
        visualRandomColors.Reset();
        visualRandomColors.IgnoreColor(backgroundColorData.ColorName);
        gameCamera.backgroundColor = Helper.GenColor(backgroundColorData.ColorRGB);
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

    public void NewGame()
    {
        MenuID activeMenuID = canvasController.GetActiveMenuID();

        // The results menu is used quite often, so this can stay in memory. 
        if (activeMenuID == MenuID.ResultsMenu)
        {
            canvasController.DisableMenu(activeMenuID);
            canvasController.EnableMenu(MenuID.GameMenu);
        }
        // The start menu is only used once, so it is destroyed to not waste memory
        else if (activeMenuID == MenuID.StartMenu)
        {
            canvasController.DestroyMenu(activeMenuID);
            canvasController.InstantiateMenu(MenuID.GameMenu);
        }
    }

    /// <summary>
    /// Updates the background color of the camera
    /// </summary>
    /// <param name="newColorData">Color data of the new color to change the background color</param>
    public void UpdateBackgroundColor(ColorData newColorData)
    {
        backgroundColorData = newColorData;
        gameCamera.backgroundColor = Helper.GenColor(newColorData.ColorRGB);
    }
}
