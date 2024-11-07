/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ProjectileBase.cs
 * Date Created: October 30, 2024
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
	public class ProjectileBase : Entity
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] protected Data.ProjectileData _myData = null;

        #endregion
        #region Private Fields: For Internal Use

        protected GameObject _owner;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public GameObject Owner
        {
            get => _owner;
            set => _owner = value;
        }

        #endregion

        //METHODS
        #region Private Activation Methods: For Script Activation

        protected virtual void OnEnable()
		{
            Vector2 direction = transform.up;
            _myRigidbody.velocity = direction.normalized * _myData.GetMovementSpeed;
        }

        #endregion
        #region Private Physics Methods: For Object Interactions

        protected virtual void OnTriggerEnter2D(Collider2D hitTarget)
        {
            if (_owner.CompareTag("Enemy"))
            {
                if (hitTarget.gameObject != _owner && !hitTarget.gameObject.CompareTag("Enemy"))
                {
                    if (hitTarget.gameObject.TryGetComponent<ITakeDamage>(out ITakeDamage targetHit))
                    {
                        targetHit.TakeDamage(_myData.GetDamage);
                        ResetObject();
                    }
                }
            }
            else
            {
                if (hitTarget.gameObject != _owner)
                {
                    if (hitTarget.gameObject.TryGetComponent<ITakeDamage>(out ITakeDamage targetHit))
                    {
                        targetHit.TakeDamage(_myData.GetDamage);
                        ResetObject();
                    }
                }
            }
        }


        #endregion
        #region Private Implementation Methods: For Class Use

        private void ResetObject()
        {
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }

        #endregion
        #region Private Deactivation Methods: For Class Exit and Cleanup

        protected virtual void OnBecameInvisible()
        {
            ResetObject();
        }

        #endregion
    }
}