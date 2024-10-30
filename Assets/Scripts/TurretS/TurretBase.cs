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
        [SerializeField] private Data.TurretData mTurretData = null;
        [SerializeField] private GameObject mExplosion = null;

        private float mCurrentHealth;
        private bool mCanTakeDamage;

        private void Start()
        {
            mCurrentHealth = mTurretData.GetHealth;
            mCanTakeDamage = false;
        }

        private void Update()
        {
            transform.position -= new Vector3(0, mTurretData.GetMoveSpeed * Time.deltaTime, 0);
        }

        private void TurretDie()
        {
            LevelActions.UpdateEnemiesKilled?.Invoke();
            Instantiate(mExplosion, transform.position, transform.rotation);
            gameObject.SetActive(false);
            mCurrentHealth = mTurretData.GetHealth;
        }

        private void OnBecameVisible() => mCanTakeDamage = true;

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
            mCanTakeDamage = false;
            mCurrentHealth = mTurretData.GetHealth;
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
                    mCurrentHealth -= aDamage;
                }
            }
        }
    }
}