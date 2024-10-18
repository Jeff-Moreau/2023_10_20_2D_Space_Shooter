/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: HealthPickupPool.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 18, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class HealthPickupPool : MonoBehaviour
{
    [SerializeField] private GameObject mHealthPickup = null;

    private List<GameObject> mHealthPickups;
    private int mAmountOfHealthPickups;

    public List<GameObject> GetHealthPickups => mHealthPickups;


    private void Start()
    {
        mHealthPickups = new List<GameObject>();
        mAmountOfHealthPickups = 1;

        for (int i = 0; i < mAmountOfHealthPickups; i++)
        {
            mHealthPickups.Add(Instantiate(mHealthPickup, transform));
            mHealthPickups[i].SetActive(false);
        }
    }

    public GameObject GetHealthPickup()
    {
        for (int i = 0; i < mAmountOfHealthPickups; i++)
        {
            if (!mHealthPickups[i].activeInHierarchy)
            {
                return mHealthPickups[i];
            }
        }

        var newHealthPickup = Instantiate(mHealthPickup, transform);
        mHealthPickup.SetActive(false);
        mHealthPickups.Add(newHealthPickup);

        return newHealthPickup;
    }
}