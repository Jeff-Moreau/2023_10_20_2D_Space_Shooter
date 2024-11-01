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
using static UnityEditor.Experimental.GraphView.GraphView;

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
        [SerializeField] private Data.PlayerData PlayerData = null;
        [SerializeField] private GameObject PlayerSpawnLocation = null;
        [SerializeField] private GameObject ThePlayer = null;
        [SerializeField] private GameObject[] TurretSpawnLocations = null;
        [SerializeField] private ObjectPoolManager LevelObjectManager = null;
        [SerializeField] private GameObject TheTurret = null;
        [SerializeField] private GameObject TheHealthPickup = null;
        //[SerializeField] private GameObject[] EnemySpawnLocations = null;

        #endregion
        #region Private Variable Declarations Only

        private int mCurrentEnemyKills;
        private float mSpawnTimer;
        private float mSpawnTimeLimit;
        private GameObject mThePlayer;
        private float mPlayerHealth;

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        private void Awake()
		{
            mThePlayer = GameObject.FindGameObjectWithTag("Player");
		}

        private void OnEnable()
        {
            mThePlayer = GameObject.FindGameObjectWithTag("Player");
            Instantiate(ThePlayer, PlayerSpawnLocation.transform.position, PlayerSpawnLocation.transform.rotation);
            LevelActions.UpdateEnemiesKilled += AddKills;
            LevelActions.DropAPickup += DropPickup;
            PlayerActions.CurrentHealth += PlayerHealth;
        }

        private void OnDisable()
        {
            LevelActions.UpdateEnemiesKilled -= AddKills;
            LevelActions.DropAPickup -= DropPickup;
            PlayerActions.CurrentHealth -= PlayerHealth;
        }

        private void Start() => InitializeVariables();

        private void InitializeVariables()
        {
            mSpawnTimeLimit = 5;
            mCurrentEnemyKills = 0;
            mPlayerHealth = PlayerData.GetMaxHealth;
            mSpawnTimer = mSpawnTimeLimit;
        }

        private void AddKills() => mCurrentEnemyKills += 1;

        private void PlayerHealth(float aAmount)
        {
            mPlayerHealth = aAmount * PlayerData.GetMaxHealth;
        }

        private void DropPickup(Transform aDropLocation, float aMoveSpeed)
        {
            Debug.Log(mPlayerHealth);
            if (mPlayerHealth < PlayerData.GetMaxHealth)
            {
                float randomChance = Random.Range(0, 100);

                if (randomChance <= 30)
                {
                    GameObject newPickup = LevelObjectManager.GetPickup(TheHealthPickup);

                    if (newPickup != null)
                    {
                        newPickup.transform.SetPositionAndRotation(aDropLocation.position, aDropLocation.rotation);
                        newPickup.SetActive(true);
                    }
                }
            }
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

        private void Update()
		{
            mSpawnTimer += Time.deltaTime;

            if (mSpawnTimer >= mSpawnTimeLimit)
            {
                GameObject newTurret = LevelObjectManager.GetEnemy(TheTurret);
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