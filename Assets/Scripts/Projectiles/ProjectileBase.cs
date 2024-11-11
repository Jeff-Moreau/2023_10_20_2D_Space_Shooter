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
        #region Private Constants: For Class-Specific Fixed Values

        private const string ENEMY = "Enemy";
        private const string LEFT_WALL = "LeftWall";
        private const string RIGHT_WALL = "RightWall";

        #endregion
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] protected Data.ProjectileData _myData = null;

        #endregion
        #region Private Fields: For Internal Use

        protected GameObject _myOwner;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public GameObject Owner
        {
            get => _myOwner;
            set => _myOwner = value;
        }

        #endregion

        //METHODS
        #region Private Activation Methods: For Script Activation

        protected virtual void OnEnable()
		{
            _myRigidbody.velocity = Vector2.zero;
            /*Vector2 direction = transform.right;
            _myRigidbody.velocity = direction.normalized * _myData.GetMovementSpeed;*/
        }

        public void Launch(Vector2 direction)
        {
            _myRigidbody.velocity = direction * _myData.GetMovementSpeed;
        }

        #endregion
        #region Private Physics Methods: For Object Interactions

        protected virtual void OnTriggerEnter2D(Collider2D iHitSomething)
        {
            GameObject objectIHit = iHitSomething.gameObject;

            if (_myOwner.CompareTag(ENEMY))
            {
                if (objectIHit != _myOwner && !objectIHit.CompareTag(ENEMY))
                {
                    if (objectIHit.TryGetComponent<ITakeDamage>(out ITakeDamage makeTheObject))
                    {
                        makeTheObject.TakeDamage(_myData.GetDamage);
                        ResetObject();
                    }
                    else if (objectIHit.CompareTag(LEFT_WALL) || objectIHit.CompareTag(RIGHT_WALL))
                    {
                        // make this its own explosion with no sound
                        GameObject impact = Instantiate(_myData.GetImpactAnimation, transform.position, transform.rotation);
                        impact.transform.localScale = transform.localScale;
                        ResetObject();
                    }
                }
            }
            else
            {
                if (objectIHit != _myOwner)
                {
                    if (objectIHit.TryGetComponent<ITakeDamage>(out ITakeDamage makeTheObject))
                    {
                        makeTheObject.TakeDamage(_myData.GetDamage);
                        ResetObject();
                    }
                    else if (objectIHit.CompareTag(LEFT_WALL) || objectIHit.CompareTag(RIGHT_WALL))
                    {
                        // make this its own explosion with no sound
                        GameObject impact = Instantiate(_myData.GetImpactAnimation, transform.position, transform.rotation);
                        impact.transform.localScale = transform.localScale;
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