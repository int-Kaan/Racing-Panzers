using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportCompanies : MonoBehaviour
{
    Unit assignedArmy;
    [SerializeField]
    public string Name;

    [SerializeField]
    public int GroudAttack; //Represents Firepower of the support company

    [SerializeField]
    public int AT; //Represents the anti-tank value of the company

    [SerializeField]
    public int EntrenchmentAttack; //Bonus on attacking entrenched targets

    [SerializeField]
    public int EntrenchmentDefence; // Bonus on defending while entrenched
    [SerializeField]
    public int AirDefence; //Air defence value
    [SerializeField]
    public int GroudDefence; // While defending on ground bonus 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
