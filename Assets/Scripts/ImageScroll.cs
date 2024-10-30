/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ImageScroll.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 30, 2024
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
        [SerializeField] private Data.ImageScrollData mScrollData = null;
        [SerializeField] private RawImage mBackgroundImage = null;

        private void Start()
        {
            mBackgroundImage.texture = mScrollData.GetImage;
            mBackgroundImage.color = mScrollData.GetImageTint;
        }

        private void Update() => mBackgroundImage.uvRect = new Rect(mBackgroundImage.uvRect.position + (new Vector2(mScrollData.GetScrollSpeed, 0) * Time.deltaTime), mBackgroundImage.uvRect.size);
    }
}