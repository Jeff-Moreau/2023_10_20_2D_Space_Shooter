/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: HealthDisplay.cs
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private PlayerInputs mPlayer = null;
    [SerializeField] private Image mBackground = null;
    [SerializeField] private TextMeshProUGUI mHealthText = null;
    [SerializeField] private Color mColorFull = Color.white;
    [SerializeField] private Color mColorEighty = Color.white;
    [SerializeField] private Color mColorSixty = Color.white;
    [SerializeField] private Color mColorForty = Color.white;
    [SerializeField] private Color mColorTwenty = Color.white;
    [SerializeField] private Color mColorDead = Color.white;

    private void Update()
    {
        if (mPlayer.GetHealth == 5)
        {
            mHealthText.text = "100%";
            mBackground.color = mColorFull;
        }
        else if (mPlayer.GetHealth == 4)
        {
            mHealthText.text = "80%";
            mBackground.color = mColorEighty;
        }
        else if (mPlayer.GetHealth == 3)
        {
            mHealthText.text = "60%";
            mBackground.color = mColorSixty;
        }
        else if (mPlayer.GetHealth == 2)
        {
            mHealthText.text = "40%";
            mBackground.color = mColorForty;
        }
        else if (mPlayer.GetHealth == 1)
        {
            mHealthText.text = "20%";
            mBackground.color = mColorTwenty;
        }
        else if (mPlayer.GetHealth == 0)
        {
            mHealthText.text = "0%";
            mBackground.color = mColorDead;
        }
    }
}
