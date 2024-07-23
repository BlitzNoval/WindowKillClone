using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Weapon weaponData;

    public delegate void SecondaryEffect();

    [SerializeField] private SecondaryEffect thisSecondaryEffect;

    public SecondaryEffect ThisSecondaryEffect
    {
        get => thisSecondaryEffect;
        set => thisSecondaryEffect = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thisSecondaryEffect.Invoke();
        }
    }
}
