/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TryAgain.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 18, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEditor;
using UnityEngine;

public class TryAgain : MonoBehaviour
{
    [SerializeField] private GameObject mThisScreen = null;
    [SerializeField] private GameObject mPlayScreen = null;
    [SerializeField] private GameObject mPlayScreenUI = null;
    [SerializeField] private GameObject mManagers = null;
    [SerializeField] private GameObject mPlayer = null;

    public void ClickStartGame()
    {
        mThisScreen.SetActive(false);
        mManagers.SetActive(true);
        mPlayScreen.SetActive(true);
        mPlayScreenUI.SetActive(true);
        mPlayer.SetActive(true);
    }

    public void ClickExitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false; // Remove before building the game
    }
}
