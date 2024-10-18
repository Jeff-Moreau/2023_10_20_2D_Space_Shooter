/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: AudioData.cs
 * Date Created: October 18, 2024
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

namespace TrenchWars.Data
{
	[CreateAssetMenu(fileName = "AudioData", menuName = "Data/New AudioData", order = 0)]
	public class AudioData : ScriptableObject
	{
        //VARIABLES
        #region Inspector Variable Declarations and Initializations

        [Space(10)]
        [Header("MUSIC DATA NEEDED >======================---")]
        [SerializeField] private float MusicFadeDuration = 2.0f;
        [Space(10)]
        [Header("MUSIC LIST >======================---")]
        [NonReorderable]
        [SerializeField] private AudioClip[] MusicList = null;
        [Space(10)]
        [Header("SOUNDFX LIST >===================---")]
        [NonReorderable]
        [SerializeField] private AudioClip[] SoundFXList = null;
        #endregion

        //GETTERS
        #region Accessors/Getters

        public float GetMusicFadeDuration => MusicFadeDuration;
        public AudioClip[] GetMusicList => MusicList;
        public AudioClip[] GetSoudFXList => SoundFXList;

        #endregion
    }
}