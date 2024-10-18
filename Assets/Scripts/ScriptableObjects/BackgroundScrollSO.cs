/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: BackgroundScrollSO.cs
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

using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundScrollBase", menuName = "ScriptableObjects/BackgroundScrollBase")]
public class BackgroundScrollSO : ScriptableObject
{
    [SerializeField] private Texture mBackgroundImage = null;
    [SerializeField] private float mScrollSpeed = 0.0f;
    [SerializeField] private Color mImageTint = Color.white;

    public float GetScrollSpeed => mScrollSpeed;
    public Texture GetBackgroundImage => mBackgroundImage;
    public Color GetImageTint => mImageTint;
}