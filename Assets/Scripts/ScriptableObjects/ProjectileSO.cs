/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ProjectileSO.cs
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

using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileBase", menuName = "ScriptableObjects/ProjectileBase")]
public class ProjectileSO : ScriptableObject
{
    [SerializeField] private AudioClip mFireSound = null;
    [SerializeField] private float mSpeed = 0.0f;

    public AudioClip GetFireSound => mFireSound;
    public float GetSpeed => mSpeed;
}