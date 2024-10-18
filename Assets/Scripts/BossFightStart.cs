/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: BossFightStart.cs
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
using UnityEngine.Rendering.Universal;

public class BossFightStart : MonoBehaviour
{
    [SerializeField] private GameObject mBossSpawn = null;
    [SerializeField] private GameObject mSharkBoss = null;
    [SerializeField] private TurretText mCurrentKills = null;
    [SerializeField] private TextMeshProUGUI mTurretKills = null;
    [SerializeField] private Light2D mGlobalLight = null;
    [SerializeField] private AudioSource mSoundSource = null;
    [SerializeField] private AudioClip mBossFight = null;
    [SerializeField] private AudioClip mBossIncoming = null;

    private int mBossCount;

    private void Start()
    {
        mBossCount = 0;
    }

    private void Update()
    {
        if (mCurrentKills.GetturretKills >= 20 && mBossCount <= 0)
        {
            mGlobalLight.color = Color.red;
            mBossCount = 1;
            mSoundSource.PlayOneShot(mBossFight);
            Invoke("SpawnBoss", 4);
        }
    }

    private void SpawnBoss()
    {

        if (!mSharkBoss.activeInHierarchy && mBossCount == 1)
        {
            mSoundSource.PlayOneShot(mBossIncoming);
            mSharkBoss.transform.position = mBossSpawn.transform.position;
            Instantiate(mSharkBoss);
            Debug.Log("Im a Shark");
        }
    }
}
