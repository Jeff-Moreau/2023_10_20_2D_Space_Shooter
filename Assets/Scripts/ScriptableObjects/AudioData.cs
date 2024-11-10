/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: AudioData.cs
 * Date Created: October 18, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 10, 2024
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
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("MUSIC SETTINGS >====================================")]
        [SerializeField, Min(0f)] private float _musicFadeInDuration = 2.0f;
        [SerializeField, Min(0f)] private float _musicFadeOutDuration = 1.0f;
        [Header("MUSIC CLIPS >=======================================")]
        [NonReorderable]
        [SerializeField] private AudioClip[] _musicList = null;
        [Header("SOUNDFX CLIPS >=====================================")]
        [NonReorderable]
        [SerializeField] private AudioClip[] _soundFXList = null;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public AudioClip[] GetMusicList => _musicList;
        public AudioClip[] GetSoudFXList => _soundFXList;
        public float GetMusicInFadeDuration => _musicFadeInDuration;
        public float GetMusicFadeOutDuration => _musicFadeOutDuration;

        #endregion
    }
}