/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: LaserOne.cs
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

namespace TrenchWars
{
    public class LaserOne : MonoBehaviour
    {
        [SerializeField] private Data.ProjectileData MyData = null;

        private Collider2D mShooter; // check for player to not hit player

        private void Update() => transform.position += new Vector3(0, MyData.GetMovementSpeed * Time.deltaTime, 0);

        private void OnTriggerEnter2D(Collider2D aTarget)
        {
            if (aTarget.gameObject.TryGetComponent<ITakeDamage>(out ITakeDamage hitTarget))
            {
                hitTarget.TakeDamage(MyData.GetDamage);
                gameObject.SetActive(false);
            }
        }

        private void OnBecameInvisible() => gameObject.SetActive(false);
    }
}