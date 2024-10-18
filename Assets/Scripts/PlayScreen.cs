/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayScreen.cs
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

public class PlayScreen : MonoBehaviour
{
    [SerializeField] private AudioSource mSoundSource = null;
    [SerializeField] private AudioClip mStartVoice = null;
    [SerializeField] private AudioClip mPowerUp = null;

    private void OnEnable()
    {
        Actions.KillCount?.Invoke(0);
        mSoundSource.PlayOneShot(mPowerUp);
        Invoke("PlaySound", 2.1f);
    }

    private void PlaySound()
    {
        mSoundSource.PlayOneShot(mStartVoice);
    }
}