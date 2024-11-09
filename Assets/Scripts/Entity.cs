/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: Entity.cs
 * Date Created: November 6, 2024
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
	public abstract class Entity : MonoBehaviour
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("BASE COMPONENTS >===================================")]
        [SerializeField] protected Renderer _myRenderer = null;
        [SerializeField] protected Rigidbody2D _myRigidbody = null;
        [SerializeField] protected Animator _myAnimator = null;

        #endregion
    }
}