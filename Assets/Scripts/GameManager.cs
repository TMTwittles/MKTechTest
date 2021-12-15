using System;
using System.Collections;
using System.Collections.Generic;
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
    //TODO: Provide a better system for handinlg menus, this system is incredibly cumbersome.
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject resultsMenu;

    private void Awake()
    {
        instance = this; // Basic singleton implementation.
    }

    private void Start()
    {
        startMenu.SetActive(true);
    }

    public void ShowResults()
    {
        gameMenu.SetActive(false);
        resultsMenu.SetActive(true);
    }

    public void NewGame()
    {
        startMenu.SetActive(false);
        resultsMenu.SetActive(false);
        gameMenu.SetActive(true);
    }


}
