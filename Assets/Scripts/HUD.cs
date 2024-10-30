/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: HUD.cs
 * Date Created: October 21, 2024
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrenchWars
{
	public class HUD : MonoBehaviour
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

		[SerializeField] private TextMeshProUGUI ScoreText = null;
		[SerializeField] private Slider ShieldLevel = null;
		[SerializeField] private Slider SpecialMeter = null;
		[SerializeField] private Slider HealthBar = null;
		
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
		
		private void OnEnable()
		{
			HUDActions.UpdateScore += UpdateScore;
			HUDActions.UpdateHealth += UpdateHealth;
			HUDActions.UpdateShield += UpdateShield;
			HUDActions.UpdateSpecial += UpdateSpecial;
		}

        private void OnDisable()
		{
            HUDActions.UpdateScore -= UpdateScore;
            HUDActions.UpdateHealth -= UpdateHealth;
            HUDActions.UpdateShield -= UpdateShield;
            HUDActions.UpdateSpecial -= UpdateSpecial;
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

        private void UpdateSpecial(float aAmount) => SpecialMeter.value = aAmount;

        private void UpdateShield(float aAmount) => ShieldLevel.value = aAmount;

        private void UpdateHealth(float aAmount) => HealthBar.value = aAmount;

        private void UpdateScore(int aAmount) => ScoreText.text = aAmount.ToString();

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