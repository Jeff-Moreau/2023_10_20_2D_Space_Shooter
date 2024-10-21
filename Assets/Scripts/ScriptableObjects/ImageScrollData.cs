/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: BackgroundScrollSO.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 20, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars.Data
{
    [CreateAssetMenu(fileName = "ImageScrollData", menuName = "Data/New ImageScrollData")]
    public class ImageScrollData : ScriptableObject
    {
        //VARIABLES
        #region Inspector Variable Declarations and Initializations

        [SerializeField] private Texture ImageToUse = null;
        [SerializeField] private float ScrollSpeed = 0.0f;
        [SerializeField] private Color ImageTint = Color.white;

        #endregion

        //GETTERS
        #region Accessors/Getters

        public Texture GetImage => ImageToUse;
        public Color GetImageTint => ImageTint;
        public float GetScrollSpeed => ScrollSpeed;

        #endregion
    }
}