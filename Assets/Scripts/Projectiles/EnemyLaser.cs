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
        [SerializeField] private Data.ProjectileData MyData = null;
        [SerializeField] private Rigidbody2D MyRigidbody = null;

        private GameObject mPlayer;

        private void OnEnable()
        {
            mPlayer = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = mPlayer.transform.position - transform.position;
            MyRigidbody.velocity = (Vector2)direction.normalized * MyData.GetMovementSpeed;
        }

        private void OnBecameInvisible() => gameObject.SetActive(false);
    }
}