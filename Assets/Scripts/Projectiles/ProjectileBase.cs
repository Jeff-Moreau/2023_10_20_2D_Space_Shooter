/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ProjectileBase.cs
 * Date Created: October 30, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 30, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars
{
	public class ProjectileBase : MonoBehaviour
	{
		//VARIABLES
		#region Inspector Variable Declarations and Initializations to empty or null

		[SerializeField] protected Data.ProjectileData MyData = null;
        [SerializeField] protected Rigidbody2D MyRigidbody = null;
		[SerializeField] protected Animator MyAnimator = null;

        #endregion
        #region Private Variable Declarations Only

        protected GameObject Owner;

        #endregion

        //GETTERS AND SETTERS
        #region Mutators/Setters

        public void SetOwner(GameObject aOwner) => Owner = aOwner;

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        private void OnEnable()
		{
            Vector2 direction = transform.up;
            MyRigidbody.velocity = direction.normalized * MyData.GetMovementSpeed;
        }

        #endregion
        #region Physics Methods/Functions

        protected virtual void OnTriggerEnter2D(Collider2D aHitTarget)
        {
            if (aHitTarget.gameObject != Owner)
            {
                if (aHitTarget.gameObject.TryGetComponent<ITakeDamage>(out ITakeDamage hitTarget))
                {
                    hitTarget.TakeDamage(MyData.GetDamage);
                    gameObject.SetActive(false);
                }
            }
        }

        #endregion
        #region Closing Methods/Functions

        protected virtual void OnBecameInvisible() => gameObject.SetActive(false);

        #endregion
    }
}