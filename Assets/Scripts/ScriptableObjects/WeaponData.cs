/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: WeaponData.cs
 * Date Created: November 7, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 7, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

namespace TrenchWars.Data
{
	[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/New WeaponData", order = 0)]
	public class WeaponData : ScriptableObject
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("GENERAL INFO >======================================")]
        [SerializeField] private string _name = "";
		[SerializeField] private Texture2D _iconImage = null;
		[SerializeField] private string _shortDescription = "";
		[SerializeField] private string _longDescription = "";

        [Header("PROJECTILE INFO >=====================================")]
		[SerializeField] private GameObject _projectileType = null;
        [SerializeField] private float _fireRate = 0.0f;
		[SerializeField] private AudioClip _fireSound = null;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public string GetName => _name;
		public float GetFireRate => _fireRate;
		public Texture2D GetIcon => _iconImage;
		public AudioClip GetFireSound => _fireSound;
		public string GetLongDescription => _longDescription;
		public string GetShortDescription => _shortDescription;
		public GameObject GetProjectileType => _projectileType;
		
		#endregion
	}
}