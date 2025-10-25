using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awareness : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float detectRange = 3.0f;

    public bool IsNearEnemy()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            return Vector3.Distance(transform.position, enemies[i].transform.position) < detectRange;
        }
        return false;
    }
}
