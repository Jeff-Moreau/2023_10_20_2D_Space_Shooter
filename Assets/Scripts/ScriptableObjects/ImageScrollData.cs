/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: BackgroundScrollSO.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 5, 2024
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
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private Texture ImageToUse = null;
        [SerializeField] private float ScrollSpeed = 0.0f;
        [SerializeField] private Color ImageTint = Color.white;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public Texture GetImage => ImageToUse;
        public Color GetImageTint => ImageTint;
        public float GetScrollSpeed => ScrollSpeed;

        #endregion
    }
}