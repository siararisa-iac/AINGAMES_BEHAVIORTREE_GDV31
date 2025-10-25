using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHunger : MonoBehaviour
{
    [SerializeField]
    private float maxHunger = 100;
    [SerializeField]
    private float hungerDecreaseRate = 5.0f;

    private float hunger;

    // This is the declaration.
    // We create "HungerDelegate" type that can store a function
    // with return type void and parameter list of (float, float)
    // Method signature
    public delegate void HungerDelegate(float current, float max);
    // This is the instance of the HungerDelegate that can store functions
    // with the same signature
    public static HungerDelegate OnHungerUpdate;


    public float MaxHunger => maxHunger;
    public float Hunger
    {
        get
        {
            return Mathf.Clamp(hunger, 0, maxHunger);
        }
        private set
        {
            hunger = value;
        }
    }
    //or
    //public float Hunger => Mathf.Clamp(hunger, 0, maxHunger);

    private void Start()
    {
        Hunger = maxHunger;
        OnHungerUpdate += PrintHunger;
        OnHungerUpdate += CheckHunger;
    }

    private void Update()
    {
        Hunger -= Time.deltaTime * hungerDecreaseRate;
        OnHungerUpdate.Invoke(Hunger, MaxHunger);
    }

    public void IncreaseHunger(float value)
    {
        Hunger += value;
    }

    private void PrintHunger(float value, float maxValue)
    {
        Debug.Log($"{value} / {maxValue}");
    }

    private void CheckHunger(float current, float max)
    {
        if (current < max / 2)
        {
            Debug.Log("HUNGRY");
        }
    }
}
