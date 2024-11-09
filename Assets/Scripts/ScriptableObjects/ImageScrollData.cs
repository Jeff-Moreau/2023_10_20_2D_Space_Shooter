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

        [SerializeField] private Texture _textureToUse = null;
        [SerializeField] private float _scrollSpeed = 0.0f;
        [SerializeField] private Color _imageTint = Color.white;

        #endregion

        //METHODS
        #region Public Properties: For Accessing Class Fields

        public Texture GetTexture => _textureToUse;
        public Color GetImageTint => _imageTint;
        public float GetScrollSpeed => _scrollSpeed;

        #endregion
    }
}