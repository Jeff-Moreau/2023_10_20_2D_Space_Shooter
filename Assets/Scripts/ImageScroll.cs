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
        [SerializeField] private Data.ImageScrollData MyData = null;
        [SerializeField] private RawImage MyImage = null;

        private void Start()
        {
            MyImage.texture = MyData.GetImage;
            MyImage.color = MyData.GetImageTint;
        }

        private void Update() => MyImage.uvRect = new Rect(MyImage.uvRect.position + (new Vector2(MyData.GetScrollSpeed, 0) * Time.deltaTime), MyImage.uvRect.size);
    }
}