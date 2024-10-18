/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretBase.cs
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

public class TurretBase : MonoBehaviour
{
    [SerializeField] private TurretSO mTurretData = null;
    [SerializeField] private GameObject mExplosion = null;

    private float mCurrentHealth;

    private void Start()
    {
        mCurrentHealth = mTurretData.GetHealth;
    }

    private void Update()
    {
        transform.position -= new Vector3(0, (mTurretData.GetMoveSpeed * Time.deltaTime), 0);

        if (mCurrentHealth <= 0)
        {
            Actions.KillCount?.Invoke(1);
            Instantiate(mExplosion, transform.position, transform.rotation);
            TurretDie();
        }
    }

    private void TurretDie()
    {
        gameObject.SetActive(false);
        mCurrentHealth = mTurretData.GetHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            mCurrentHealth -= 1;
            collision.gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        mCurrentHealth = mTurretData.GetHealth;
    }
}
