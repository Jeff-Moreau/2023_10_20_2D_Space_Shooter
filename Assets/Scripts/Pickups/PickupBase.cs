/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PickupBase.cs
 * Date Created: October 31, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 31, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

namespace TrenchWars
{
	public class PickupBase : MonoBehaviour
	{
		#region Inspector Variable Declarations and Initializations to empty or null

		[SerializeField] protected Data.PickupData MyData = null;
		
		#endregion
		
		//FUNCTIONS
        #region Physics Methods/Functions

        protected virtual void OnTriggerEnter2D(Collider2D aHitTarget)
        {
			if (aHitTarget.gameObject.CompareTag("Player"))
            {
                if (aHitTarget.gameObject.TryGetComponent<ITakeDamage>(out ITakeDamage hitTarget))
                {
                    hitTarget.HealDamage(MyData.GetHealAmount);
                    gameObject.SetActive(false);
                }
            }
        }

        #endregion
        #region Implementation Methods/Functions

        protected virtual void Update()
		{
            transform.position -= new Vector3(0, MyData.GetMoveSpeed * Time.deltaTime, 0);
        }

        #endregion
        #region Closing Methods/Functions

        protected virtual void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}