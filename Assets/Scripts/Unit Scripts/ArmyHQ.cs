using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyHQ : MonoBehaviour
{
    /***************************************/
    //         ARMY GROUP PROPERTIES       //
    /***************************************/
    //General and Army Name
    public string Name;
    public string General;
    //Your Turn
    public bool myTurn;
    //Enemy Army
    public ArmyHQ enemyArmy;
    //Divisions
    public Unit[] divisions;
    public Unit[] armies;
    //Cumulative Current Manpower
    int Manpower;

    //Army Alligance
    // 0 -> Axis
    // 1 -> Soviet
    public int Alligance;

    public void calculateCurrentManpower()
    {
        Manpower = 0;
        for (int i = 0; i < divisions.Length; i++)
        {
            Manpower += divisions[i].cumManpower;
        }
    }
 
    //Cumulative Current Moral
    int Moral;

    public void calculateCurrentMoral()
    {
        Moral = 0;
        for (int i = 0; i < divisions.Length; i++)
        {
            Moral += divisions[i].cumMoral;
        }
    }

    //Base Manpower
    int BaseManpower;
    public void calculateBaseManpower()
    {
        BaseManpower = 0;
        for (int i = 0; i < divisions.Length; i++)
        {
            BaseManpower += divisions[i].baseManpower;
        }
    }
    //Base Moral
    int BaseMoral;
    public void calculateBaseMoral()
    {
        BaseMoral = 0;
        for (int i = 0; i < divisions.Length; i++)
        {
            BaseMoral += divisions[i].baseMoral;
        }
    }



    // Start is called before the first frame update

    void Start()
    {
        calculateBaseManpower();
        calculateBaseMoral();
    }

    // Update is called once per frame
    void Update()
    {
        calculateCurrentManpower();
        calculateCurrentMoral();
    }
}
