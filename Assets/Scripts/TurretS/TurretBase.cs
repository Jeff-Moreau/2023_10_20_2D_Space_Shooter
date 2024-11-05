/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretBase.cs
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
    public class TurretBase : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private Data.TurretData MyData = null;
        [SerializeField] private AudioSource MyAudioSource = null;

        private float mCurrentHealth;
        private bool mCanTakeDamage;

        private void Start()
        {
            mCurrentHealth = MyData.GetHealth;
            mCanTakeDamage = false;
        }

        private void Update()
        {
            transform.position -= new Vector3(0, MyData.GetMoveSpeed * Time.deltaTime, 0);
        }

        private void OnDeath()
        {
            LevelActions.UpdateEnemiesKilled?.Invoke();
            //Update score witha value
            LevelActions.DropAPickup?.Invoke(gameObject.transform, MyData.GetMoveSpeed);
            //Make this call from pooled explosions and not instantiate a new one
            Instantiate(MyData.GetExplosionAnimation, transform.position, transform.rotation);
            gameObject.SetActive(false);
            mCurrentHealth = MyData.GetHealth;
        }

        private void OnBecameVisible() => mCanTakeDamage = true;

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
            mCanTakeDamage = false;
            mCurrentHealth = MyData.GetHealth;
        }

        public void TakeDamage(float aDamage)
        {
            if (mCanTakeDamage)
            {
                MyAudioSource.PlayOneShot(MyData.GetTakeDamageSound);

                if (mCurrentHealth - aDamage <= 0)
                {
                    OnDeath();
                }
                else
                {
                    mCurrentHealth -= aDamage;
                }
            }
        }

        public void HealDamage(float aHeal)
        {
            return;
        }
    }
}