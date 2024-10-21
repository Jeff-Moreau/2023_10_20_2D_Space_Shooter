/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayScreen.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 19, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars
{
    public class LevelControl : MonoBehaviour
    {
        //VARIABLES
        #region Constant Variable Declarations and Initializations

        // private const int MY_AGE = 44;  // Example

        #endregion
        #region Inspector Variable Declarations and Initializations to empty or null

        [SerializeField] private Data.LevelData MyLevelData = null;

        #endregion
        #region Private Variable Declarations Only

        // private int mMyInt;  // Example

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
            Manager.AudioManager.Access.PlayMusic(MyLevelData.GetMusic, eMusicSource.Normal, true);
        }

        #endregion
        #region Physics Methods/Functions

        /*private void FixedUpdate()
		{
			#NOTRIM#
		}*/

        /*private void OnCollisionEnter(Collision collision)
		{
			#NOTRIM#
		}*/

        /*private void OnCollisionStay(Collision collision)
		{
			#NOTRIM#
		}*/

        /*private void OnCollisionExit(Collision collision)
		{
			#NOTRIM#
		}*/

        /*private void OnTriggerEnter(Collider other)
		{
			#NOTRIM#
		}*/

        /*private void OnTriggerStay(Collider other)
		{
			#NOTRIM#
		}*/

        /*private void OnTriggerExit(Collider other)
		{
			#NOTRIM#
		}*/

        #endregion
        #region Implementation Methods/Functions

        /*private void Update()
		{
			#NOTRIM#
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
			#NOTRIM#
		}*/

        /*private void OnDestroy()
		{
			#NOTRIM#
		}*/

        #endregion
        #region Old Code

        /*[SerializeField] private AudioSource mSoundSource = null;
        [SerializeField] private AudioClip mStartVoice = null;
        [SerializeField] private AudioClip mPowerUp = null;

        private void OnEnable()
        {
            UIActions.KillCount?.Invoke(0);
            mSoundSource.PlayOneShot(mPowerUp);
            Invoke("PlaySound", 2.1f);
        }

        private void PlaySound()
        {
            mSoundSource.PlayOneShot(mStartVoice);
        }*/

        #endregion
    }
}