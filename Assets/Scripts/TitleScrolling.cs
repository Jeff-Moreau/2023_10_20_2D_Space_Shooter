/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TitleScrolling.cs
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
using UnityEngine.UI;

public class TitleScrolling : MonoBehaviour
{
    [SerializeField] private RawImage mBackgroundImage = null;

    private void Update()
    {
        mBackgroundImage.uvRect = new Rect(mBackgroundImage.uvRect.position + new Vector2(0.1f, 0) * Time.deltaTime, mBackgroundImage.uvRect.size);
    }
}
