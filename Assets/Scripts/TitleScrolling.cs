/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TitleScrolling.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class TitleScrolling : MonoBehaviour
{
    //FIELDS
    #region Private Serialized Fields: For Inspector Editable Values

    [SerializeField] private RawImage _myImage = null;

    #endregion

    //METHODS
    #region Private Real-Time Methods: For Per-Frame Game Logic

    private void Update()
    {
        _myImage.uvRect = new Rect(_myImage.uvRect.position + (new Vector2(0.1f, 0) * Time.deltaTime), _myImage.uvRect.size);
    }

    #endregion
}
