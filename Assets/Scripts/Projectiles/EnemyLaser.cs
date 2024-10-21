/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: EnemyLaser.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 20, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars
{
    public class EnemyLaser : MonoBehaviour
    {
        [SerializeField] private Data.ProjectileData mProjectileData = null;
        [SerializeField] private Rigidbody2D mRigidbody = null;

        private GameObject mPlayer;

        private void OnEnable()
        {
            mPlayer = GameObject.FindGameObjectWithTag("Player");
            var direction = mPlayer.transform.position - transform.position;
            mRigidbody.velocity = new Vector2(direction.x, direction.y).normalized * mProjectileData.GetMovementSpeed;
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}