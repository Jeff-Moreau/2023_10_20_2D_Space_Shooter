/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: Turret.cs
 * Date Created: November 12, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 12, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

namespace TrenchWars
{
	public class Turret : EnemyController
	{
		//METHODS
		#region Private Initialization Methods: For Class Setup
		
		protected override void Awake()
		{
			base.Awake();
		}
		
		protected override void Start()
		{
			base.Start();
			InitializeFields();
		}
		
		protected override void InitializeFields()
		{
			base.InitializeFields();
		}
		
		#endregion
		#region Private Real-Time Methods: For Per-Frame Game Logic
		
		protected override void Update()
		{
			base.Update();
            TargetPlayer();
            transform.position -= new Vector3(0, _myData.GetMovementSpeed * _levelControl.LevelSpeed * Time.deltaTime, 0);
        }

        #endregion
    }
}