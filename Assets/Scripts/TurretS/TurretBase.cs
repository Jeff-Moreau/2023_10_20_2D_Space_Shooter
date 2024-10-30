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
        [SerializeField] private Data.TurretData MyTurretData = null;
        [SerializeField] private AudioSource MyAudioSource = null;

        private float mCurrentHealth;
        private bool mCanTakeDamage;

        private void Start()
        {
            mCurrentHealth = MyTurretData.GetHealth;
            mCanTakeDamage = false;
        }

        private void Update()
        {
            transform.position -= new Vector3(0, MyTurretData.GetMoveSpeed * Time.deltaTime, 0);
        }

        private void TurretDie()
        {
            LevelActions.UpdateEnemiesKilled?.Invoke();
            Instantiate(MyTurretData.GetExplosionAnimation, transform.position, transform.rotation);
            gameObject.SetActive(false);
            mCurrentHealth = MyTurretData.GetHealth;
        }

        private void OnBecameVisible() => mCanTakeDamage = true;

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
            mCanTakeDamage = false;
            mCurrentHealth = MyTurretData.GetHealth;
        }

        public void TakeDamage(float aDamage)
        {
            if (mCanTakeDamage)
            {
                if (mCurrentHealth - aDamage <= 0)
                {
                    TurretDie();
                }
                else
                {
                    MyAudioSource.PlayOneShot(MyTurretData.GetTakeDamageSound);
                    mCurrentHealth -= aDamage;
                }
            }
        }
    }
}