using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [SerializeField] private GameObject laserProjectile;

    private List<GameObject> laserProjectiles;
    public List<GameObject> GetLaserProjectiles => laserProjectiles;

    private int amountOfLaserProjectiles;

    private void Start()
    {
        laserProjectiles = new List<GameObject>();

        amountOfLaserProjectiles = 50;

        for (int i = 0; i < amountOfLaserProjectiles; i++)
        {
            laserProjectiles.Add(Instantiate(laserProjectile, transform));
            laserProjectiles[i].SetActive(false);
        }
    }

    public GameObject GetLaserProjectile()
    {
        for (int i = 0; i < amountOfLaserProjectiles; i++)
        {
            if (!laserProjectiles[i].activeInHierarchy)
            {
                return laserProjectiles[i];
            }
        }

        var newLaser = Instantiate(laserProjectile, transform);
        laserProjectile.gameObject.SetActive(false);
        laserProjectiles.Add(newLaser);

        return newLaser;
    }
}
