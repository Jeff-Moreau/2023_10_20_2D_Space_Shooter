/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: LevelControl.cs
 * Date Created: October 20, 2023
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
    public class LevelControl : MonoBehaviour
    {
        //VARIABLES
        #region Constant Variable Declarations and Initializations

        // private const int MY_AGE = 44;  // Example

        #endregion
        #region Inspector Variable Declarations and Initializations to empty or null

        [SerializeField] private Data.LevelData MyData = null;
        [SerializeField] private GameObject PlayerSpawnLocation = null;
        [SerializeField] private GameObject ThePlayer = null;
        [SerializeField] private GameObject[] TurretSpawnLocations = null;
        [SerializeField] private ObjectPoolManager LevelObjectManager = null;
        [SerializeField] private GameObject TheTurret = null;
        //[SerializeField] private GameObject[] EnemySpawnLocations = null;

        #endregion
        #region Private Variable Declarations Only

        private int mCurrentEnemyKills;
        private float mSpawnTimer;
        private float mSpawnTimeLimit;

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        /*private void Awake()
		{
			//used for when the object is FIRST activated and ONLY ONCE
		}*/

        private void OnEnable()
        {
            Instantiate(ThePlayer, PlayerSpawnLocation.transform.position, PlayerSpawnLocation.transform.rotation);
            LevelActions.UpdateEnemiesKilled += AddKills;
        }

        private void OnDisable() => LevelActions.UpdateEnemiesKilled -= AddKills;

        private void Start() => InitializeVariables();

        private void InitializeVariables()
        {
            Manager.AudioManager.Access.PlayMusic(MyData.GetMusic, eMusicSource.Normal, true);
            mCurrentEnemyKills = 0;
            mSpawnTimeLimit = 5;
            mSpawnTimer = mSpawnTimeLimit;
        }

        private void AddKills() => mCurrentEnemyKills += 1;

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

        private void Update()
		{
            mSpawnTimer += Time.deltaTime;

            if (mSpawnTimer >= mSpawnTimeLimit)
            {
                GameObject newTurret = LevelObjectManager.GetObject(TheTurret);
                int randomSpawn = Random.Range(0, 3);

                if (newTurret != null)
                {
                    newTurret.transform.position = TurretSpawnLocations[randomSpawn].transform.position;
                    newTurret.SetActive(true);
                    mSpawnTimer = 0;
                    mSpawnTimeLimit = Random.Range(3, 10);
                }
            }
        }

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
    }
}