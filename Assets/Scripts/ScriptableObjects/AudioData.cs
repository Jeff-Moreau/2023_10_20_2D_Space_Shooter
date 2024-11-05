/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: AudioData.cs
 * Date Created: October 18, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 5, 2024
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

        [Space(10)]
        [Header("MUSIC DATA NEEDED >======================---")]
        [SerializeField] private float _musicFadeDuration = 2.0f;
        [Space(10)]
        [Header("MUSIC LIST >======================---")]
        [NonReorderable]
        [SerializeField] private AudioClip[] _musicList = null;
        [Space(10)]
        [Header("SOUNDFX LIST >===================---")]
        [NonReorderable]
        [SerializeField] private AudioClip[] _soundFXList = null;
        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public float GetMusicFadeDuration => _musicFadeDuration;
        public AudioClip[] GetMusicList => _musicList;
        public AudioClip[] GetSoudFXList => _soundFXList;

        #endregion
    }
}