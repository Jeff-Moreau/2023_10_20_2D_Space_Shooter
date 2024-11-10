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