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

//ENUMERATORS
#region Public Enumerator Declarations Only

// public enum eEnumName  // Example
// {
// 		Hey,
//		You
// }

#endregion

namespace TrenchWars
{
	public class PickupBase : MonoBehaviour
	{
		//ENUMERATORS
		#region Private Enumerator Declarations Only

		// private enum eEnumName  // Example
		// {
		// 		Hey,
		//		You
		// }

		#endregion

		//VARIABLES
		#region Constant Variable Declarations and Initializations

		// private const int MY_AGE = 44;  // Example

		#endregion
		#region Inspector Variable Declarations and Initializations to empty or null

		[SerializeField] protected Data.PickupData MyData = null;
		
		#endregion
		#region Private Variable Declarations Only
		
		// private int mMyInt;  // Example
		
		#endregion
		
		//GETTERS AND SETTERS
		#region Accessors/Getters
		
		#endregion
		#region Mutators/Setters
		
		#endregion
		
		//FUNCTIONS
		#region Initialization Methods/Functions
		
		/*private void Awake()
		{
			//used for when the object is FIRST activated and ONLY ONCE
		}*/
		
		/*private void OnEnable()
		{
			//Anytime the Object is set to active this is called
		}*/
		
		/*private void OnDisable()
		{
			
		}*/
		
		private void Start() => InitializeVariables();
		
		private void InitializeVariables()
		{
			
		}
		
		#endregion
		#region Physics Methods/Functions
		
		/*private void FixedUpdate()
		{
			
		}*/
		
		/*private void OnCollisionEnter(Collision collision)
		{
			
		}*/
		
		/*private void OnCollisionStay(Collision collision)
		{
			
		}*/
		
		/*private void OnCollisionExit(Collision collision)
		{
			
		}*/
		
		/*private void OnTriggerEnter(Collider other)
		{
			
		}*/
		
		/*private void OnTriggerStay(Collider other)
		{
			
		}*/
		
		/*private void OnTriggerExit(Collider other)
		{
			
		}*/
		
		#endregion
		#region Implementation Methods/Functions
		
		/*private void Update()
		{
			
		}*/
		
		/*private void LateUpdate()
		{
			//Just like Updated but done after Update
		}*/
		
		#endregion
		#region Private Methods/Functions
		
		/*private void Save()
		{
		
		}*/
		
		/*private void Load()
		{
		
		}*/
		
		#endregion
		#region Public Methods/Functions
		
		//Public made functions go here
		
		#endregion
		#region Closing Methods/Functions
		
		/*private void OnApplicationQuit()
		{
			
		}*/
		
		/*private void OnDestroy()
		{
			
		}*/
		
		#endregion
	}
}