/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: Entity.cs
 * Date Created: November 6, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

namespace TrenchWars
{
	public abstract class Entity : MonoBehaviour
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] protected Data.EntityData _myData = null;
        [SerializeField] protected Health _myHealth = null;

        #endregion
        #region Private Fields: For Internal Use

        // private int _myInt;  // Example

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        protected virtual void Awake()
		{
			
		}

        protected virtual void Start()
		{
			InitializeVariables();
		}

        protected virtual void InitializeVariables()
		{

        }

        #endregion
        #region Private Activation Methods: For Script Activation

        protected virtual void OnEnable()
		{
			if (_myHealth != null)
            {
                _myHealth.OnHealthChange += MyHealthChanged;
                _myHealth.OnDeath += IDied;
            }
		}

        protected virtual void OnDisable()
		{
            if (_myHealth != null)
            {
                _myHealth.OnHealthChange -= MyHealthChanged;
                _myHealth.OnDeath -= IDied;
            }
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        /*protected virtual void Update()
	    {
	
	    }*/

        #endregion
        #region Private Implementation Methods: For Class Use

        protected virtual void MyHealthChanged(float currentHealth)
        {
            // Does anything happen when my health changes
        }

        protected virtual void IDied()
        {
            // What happens when i die
        }

        #endregion
    }
}