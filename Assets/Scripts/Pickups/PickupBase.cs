/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PickupBase.cs
 * Date Created: October 31, 2024
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

#region Public Enums: For Cross-Class References

public enum PickupType
{
    Health,
    Damage,
    Weapon
}

#endregion

namespace TrenchWars
{
	public class PickupBase : Entity
	{
        //FIELDS
        #region Private Constants: For Class-Specific Fixed Values

        private const string PLAYER = "Player";

        #endregion
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] protected Data.PickupData _myData = null;

        #endregion

        //METHODS
        #region Private Physics Methods: For Object Interactions

        protected virtual void OnTriggerEnter2D(Collider2D iHitSomething)
        {
            switch (_myData.GetPickupType)
            {
                case PickupType.Health:
                    HealthPickup(iHitSomething);
                    break;

                case PickupType.Damage:
                    break;

                case PickupType.Weapon:
                    WeaponPickup(iHitSomething);
                    break;

                default:
                    break;
            }
        }

        private void WeaponPickup(Collider2D iHitSomething)
        {
            if (iHitSomething.gameObject.CompareTag(PLAYER))
            {
                iHitSomething.GetComponent<PlayerController>().EquipWeapon(_myData.GetWeaponType);
                gameObject.SetActive(false);
            }
        }

        private void HealthPickup(Collider2D iHitSomething)
        {
            if (iHitSomething.gameObject.CompareTag(PLAYER))
            {
                if (iHitSomething.gameObject.TryGetComponent<ITakeDamage>(out ITakeDamage makeTheObject))
                {
                    makeTheObject.HealDamage(_myData.GetHealAmount);
                    gameObject.SetActive(false);
                }
            }
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        protected virtual void Update()
		{
            transform.position -= new Vector3(0, _myData.GetMoveSpeed * Time.deltaTime, 0); // get speed from object that dropped you
        }

        #endregion
        #region Private Deactivation Methods: For Class Exit and Cleanup

        protected virtual void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}