/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: LaserOne.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 22, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars
{
    public class LaserOne : MonoBehaviour
    {
        [SerializeField] private Data.ProjectileData MyProjectileData = null;

        private Collider2D mShooter;

        private void Update()
        {
            transform.position += new Vector3(0, (MyProjectileData.GetMovementSpeed * Time.deltaTime), 0);
        }

        private void OnTriggerEnter2D(Collider2D aTarget)
        {
            ITakeDamage hitTarget = aTarget.gameObject.GetComponent<ITakeDamage>();

            if (hitTarget != null)
            {
                hitTarget.TakeDamage(MyProjectileData.GetDamage);
                gameObject.SetActive(false);
            }
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}