/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectMovingScript.cs
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

public class ObjectMovingScript : MonoBehaviour
{
    void Update()
    {
        transform.position -= new Vector3(0, (4.2f * Time.deltaTime), 0);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
