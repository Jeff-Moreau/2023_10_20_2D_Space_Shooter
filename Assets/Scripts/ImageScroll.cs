/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ImageScroll.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 10, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace TrenchWars
{
    public class ImageScroll : MonoBehaviour
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] private Data.ImageScrollData _myData = null;
        [Header("IMAGES >============================================")]
        [SerializeField] private RawImage _myImage = null;
        [SerializeField] private LevelControl _levelControl = null;

        #endregion
        #region Private Fields: For Internal Use

        private float _levelSpeed;

        #endregion
        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Start()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            _myImage.texture = _myData.GetTexture;
            _myImage.color = _myData.GetImageTint;
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            _myImage.uvRect = new Rect(_myImage.uvRect.position + (new Vector2(_myData.GetScrollSpeed * _levelControl.LevelSpeed, 0) * Time.deltaTime), _myImage.uvRect.size);
        }

        #endregion
    }
}