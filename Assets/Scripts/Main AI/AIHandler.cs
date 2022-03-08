using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour
{
    [Header("World Holder")]
    [SerializeField]
    public multi_handler world_holder;
    int COUNTERGIVER = 0;
    [Header("Turn Holder")]
    [SerializeField]
    public turnCounter turn_holder;
    public BattleEvent battler;
    int counterMAIN = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    int MODDER = 0;
    // Update is called once per frame
    void Update()
    {
        turn_holder.b.onClick.AddListener(process1);
        counterMAIN = turn_holder.BaseTurn;

    }

    //Main Ai handler trait
    public void process1()
    {
        // pull divisons
        Unit[] SovietDivisions = this.world_holder.Soviet.divisions;
        Unit[] GermanDivisions = this.world_holder.Axis.divisions;

        //control division enemy areas

        //call process 2, calculate strength index

        double strength_index_calculated = process2(SovietDivisions, GermanDivisions);
        COUNTERGIVER = 0;
        process3(strength_index_calculated, SovietDivisions);
        for (int i = 0; i < SovietDivisions.Length; i++)
        {
            if (!SovietDivisions[i].Hex.isDefensible || !SovietDivisions[i].Hex.isVictoryPoint)
            {
                SovietDivisions[i].status = 0;
                SovietDivisions[i].MovementValue++;
            }
        }
        Unit[] Soviet = SovietDivisions;
        for (int i = 0; i < Soviet.Length; i++)
        {
            if (!amINearAnEnemy(Soviet[i], 2) && Soviet[i].Hex != null)
            {
                for (int y = 0; y < Soviet[i].Hex.neighboors.Length; y++)
                {
                    if (Soviet[i].Hex.neighboors[y].HexType == 1 && Soviet[i].Hex.neighboors[y].Unit == null)
                    {
                        Soviet[i].Hex.neighboors[y].Unit = Soviet[i];
                        Soviet[i].Hex.Unit = null;
                        HolyGrasslandHex hexHolder;
                        hexHolder = Soviet[i].Hex.neighboors[y];
                        Soviet[i].Hex = hexHolder;
                    }
                    else if (Soviet[i].Hex.neighboors[y].Unit == null)
                    {
                        Soviet[i].Hex.neighboors[y].Unit = Soviet[i];
                        Soviet[i].Hex.Unit = null;
                        HolyGrasslandHex hexHolder;
                        hexHolder = Soviet[i].Hex.neighboors[y];
                        Soviet[i].Hex = hexHolder;
                    }
                }
            }
        }
    }

    public double process2(Unit[] Soviet, Unit[] German)
    {
        double strength_index_base = 100;

        double SovietIndex = 0;
        double GermanIndex = 0;

        double strength_index = 0;

        for (int i = 0; i < Soviet.Length; i++)
        {
            if (Soviet[i].styleofDivision == 0)
            {
                for (int bat = 0; bat < Soviet[i].battalions.Length; bat++)
                {
                    SovietIndex += Soviet[i].battalions[bat].artileryCount * 2 + Soviet[i].battalions[bat].tankCount * 3 + Soviet[i].battalions[bat].manpower * 2;
                }

                SovietIndex += Soviet[i].CombatValue * 10;
            }
        }
        for (int i = 0; i < German.Length; i++)
        {
            if (German[i].styleofDivision == 0)
            {
                for (int bat = 0; bat < German[i].battalions.Length; bat++)
                {
                    GermanIndex += German[i].battalions[bat].artileryCount * 2 + German[i].battalions[bat].tankCount * 3 + German[i].battalions[bat].manpower * 2;
                }

                GermanIndex += German[i].CombatValue * 10;
            }
        }

        //Debug.Log("Soviet Index = " + SovietIndex);
        //Debug.Log("German Index = " + GermanIndex);

        strength_index = SovietIndex / GermanIndex;
        return strength_index;
    }
    public void moveAIorderEmptyHex(Unit unit, HolyGrasslandHex destinationHex)
    {
        unit.Hex.Unit = null;
        unit.Hex = null;
        destinationHex.Unit = unit;
        unit.Hex = destinationHex;
        //unit.MovementValue += - 1;
    }

    public bool amINearAnEnemy(Unit unit, int distance)
    {

        for(int i = 0; i < unit.Hex.neighboors.Length; i++)
        {
            if (unit.Hex.neighboors[i].Unit != null)
            {
                if (unit.Hex.neighboors[i].Unit.typeOfDivision == 0)
                {
                    //Debug.Log("I AM NEAR AN ENEMY" + unit.divisionName);
                    return true;
                }
            }
        }
        if(distance != 0)
        {
            return amINearAnEnemy(unit, distance - 1);
        }
        else
        {
            return false;
        }
    }

    public bool amINearAVictoryPoint(Unit unit, int distance)
    {
        for (int i = 0; i < unit.Hex.neighboors.Length; i++)
        {
            if (unit.Hex.neighboors[i].isVictoryPoint == true)
            {
                //Debug.Log("I AM NEAR AN ENEMY" + unit.divisionName);
                return true;
            }
        }
        return false;
    }

    public bool amINearFriendlyUnits(Unit unit)
    {
        int counter = 0;
        for (int i = 0; i < unit.Hex.neighboors.Length; i++)
        {
            if (counter == 2)
            {
                return true;
            }
            else if (unit.Hex.neighboors[i].Unit.typeOfDivision == 1)
            {
                counter++;
            }
        }

        return false;
    }

    public bool isHexNearEnemies(HolyGrasslandHex hex, int distance)
    {
        for (int i = 0; i < hex.neighboors.Length; i++)
        {
            if (hex.neighboors[i].Unit != null)
            {
                if (hex.neighboors[i].Unit.typeOfDivision == 0)
                {
                    //Debug.Log("I AM NEAR AN ENEMY");
                    return true;
                }
            }
        }
        if (distance != 0)
        {
            return isHexNearEnemies(hex, distance - 1);
        }
        else
        {
            return false;
        }
    }
    public void moveDefensibleHex(Unit unit)
    {
        if (unit.Hex.neighboors != null && unit != null && unit.Hex != null && unit.status != 2)
        {
            for (int i = 0; i < unit.Hex.neighboors.Length; i++)
            {
                //Debug.Log("Division Becomes Reservist" + unit.divisionName);
                // i = neighboor count
                if (unit.Hex.isDefensible || unit.Hex.isVictoryPoint)
                {
                    unit.status = 2;
                }
                else if (unit.Hex.isVictoryPoint == true)
                {
                    unit.status = 2;
                }
                else if (amINearAnEnemy(unit, 2) == true)
                {
                    //Debug.Log("!!!!!!!!!!!!!!!!!!");
                    if (amINearAVictoryPoint(unit, 2) == false)
                    {
                        Debug.Log("I can start defending !");
                        for (int y = 0; y < unit.Hex.neighboors.Length; y++)
                        {
                            if (isHexNearEnemies(unit.Hex.neighboors[y], 1) != true && unit.Hex.neighboors[y].Unit == null && unit.Hex.neighboors[y].HexType == 0)
                            {
                                unit.Hex.neighboors[y].Unit = unit;
                                unit.Hex.Unit = null;
                                HolyGrasslandHex hexHolder;
                                hexHolder = unit.Hex.neighboors[y];
                                unit.Hex = hexHolder;
                                unit.status = 2;
                                //Debug.Log("Retreating becouse of Overwhelming Enemy Forces !");
                            }
                        }
                    }
                }
                else if (unit.Hex.neighboors[i].isVictoryPoint && unit.Hex.neighboors[i].Unit == null && unit.Hex.HexType == 0)
                {
                    unit.Hex.neighboors[i].Unit = unit;
                    unit.Hex.Unit = null;
                    HolyGrasslandHex hexHolder;
                    hexHolder = unit.Hex.neighboors[i];
                    unit.Hex = hexHolder;
                    unit.status = 2;
                }
                else if (unit.Hex.neighboors[i].isDefensible && unit.Hex.neighboors[i].Unit == null && unit.Hex.HexType == 0 && unit.status != 2)
                {
                    unit.Hex.neighboors[i].Unit = unit;
                    unit.Hex.Unit = null;
                    HolyGrasslandHex hexHolder;
                    hexHolder = unit.Hex.neighboors[i];
                    unit.Hex = hexHolder;
                    unit.status = 2;
                }
                /*                else if (amINearAnEnemy(unit, 2))
                                {

                                    unit.Hex.neighboors[i].Unit = unit;
                                    unit.Hex.Unit = null;
                                    HolyGrasslandHex hexHolder;
                                    hexHolder = unit.Hex.neighboors[i];
                                    unit.Hex = hexHolder;
                                    unit.status = 2;
                                }*/
            }
            /*            for (int i = 0; i < unit.Hex.neighboors.Length; i++)
                        {
                            if (unit.Hex.neighboors != null && unit != null)
                            {
                                if (unit.Hex.neighboors[i].isVictoryPoint && (unit.Hex.neighboors[i].Unit == null) && unit != null && unit.Hex.neighboors != null)
                                {
                                    if (unit.Hex.neighboors[i] != null && unit.Hex != null)
                                    {
                                        unit.Hex.Unit = null;
                                        unit.Hex.neighboors[i].Unit = unit;
                                        unit.Hex = unit.Hex.neighboors[i];
                                        unit.Hex = null;
                                        unit.status = 2;
                                    }
                                }
                                else if (unit.Hex.neighboors[i].isDefensible == true && (unit.Hex.neighboors[i].Unit == null) && unit != null && unit.Hex.neighboors != null)
                                {
                                    if (unit.Hex.neighboors[i] != null && unit.Hex != null)
                                    {
                                        unit.Hex.Unit = null;
                                        unit.Hex.neighboors[i].Unit = unit;

                                        if(unit == null && unit.Hex == null)
                                        {
                                            Debug.Log(" !! Null !!");
                                        }
                                        unit.Hex = unit.Hex.neighboors[i];
                                        unit.Hex = null;
                                        unit.status = 2;
                                    }
                                }
                            }
                        }*/
        }
        else if (unit.status != 2 && amINearAnEnemy(unit, 1) && (unit.Hex.isDefensible != true || unit.Hex.isVictoryPoint != true))
        {
            for (int i = 0; i < unit.Hex.neighboors.Length; i++)
            {
                if (unit.Hex.neighboors[i].Unit != null)
                {
                    if (unit.Hex.neighboors[i].Unit.typeOfDivision == 0)
                    {
                        for (int y = 0; y < unit.Hex.neighboors.Length; y++)
                        {
                            if (isHexNearEnemies(unit.Hex.neighboors[y], 1) != true && unit.Hex.neighboors[y].HexType == 0 && unit.Hex.neighboors[y].Unit == null)
                            {
                                Debug.Log("");
                                unit.Hex.neighboors[y].Unit = unit;
                                unit.Hex.Unit = null;
                                HolyGrasslandHex hexHolder;
                                hexHolder = unit.Hex.neighboors[y];
                                unit.Hex = hexHolder;
                                unit.status = 2;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (unit.Hex.isVictoryPoint || unit.Hex.isDefensible)
            {
                unit.status = 2;
            }
        }

        if (unit.Hex.isVictoryPoint || unit.Hex.isDefensible)
        {
            unit.status = 2;
        }
    }

    public bool AttackEnemy(Unit attacker, HolyGrasslandHex defenderHex)
    {
        Unit defenderUnit = defenderHex.Unit;

        if (battler.battleEvent(attacker, defenderUnit))
        {
            //attacker wins
            return true;
        }
        else
        {
            return false;
        }

    }

    public void process3(double strenth_index, Unit[] Soviet)
    {
        if (strenth_index > 1 && counterMAIN > 22)
        {
            Debug.Log("counter main IS " + counterMAIN);

            //Offansive Plan
            // Will attack if not overwhelmed 1(hex)
            Debug.Log("Attacking");
            for (int i = 0; i < Soviet.Length; i++)
            {
                Soviet[i].status = 0;
                bool isReady = Soviet[i].isReadyToAttackSovietAI();
                int counterLight = 0;
                if (isReady && Soviet[i].MovementValue > 0)
                {
                    moveDefensibleHex(Soviet[i]);
                    Debug.Log("Attacking");
                    for (int y = 0; y < Soviet[i].Hex.neighboors.Length; y++)
                    {
                        if (Soviet[i].Hex.neighboors[y].Unit != null)
                        {
                            if (Soviet[i].Hex.neighboors[y].Unit.typeOfDivision == 0)
                            {
                                if (AttackEnemy(Soviet[i], Soviet[i].Hex.neighboors[y]))
                                {
                                    //kazanmissa
                                    try
                                    {
                                        for (int z = 0; z < Soviet[i].Hex.neighboors[y].Unit.Hex.neighboors.Length; z++)
                                        {
                                            if (Soviet[i].Hex.neighboors[y].Unit.Hex.neighboors[z].Unit == null)
                                            {
                                                //geri cekilebiliyorsa
                                                // eski dusman unit hexini sifirla
                                                Soviet[i].Hex.neighboors[y].Unit.Hex.Unit = null;
                                                //yeni dusman unit hexini ata
                                                Soviet[i].Hex.neighboors[y].Unit.Hex.neighboors[z].Unit = Soviet[i].Hex.neighboors[y].Unit;
                                                //dusman hexini eskisi yap
                                                HolyGrasslandHex newHex;
                                                newHex = Soviet[i].Hex.neighboors[y].Unit.Hex.neighboors[z];
                                                Soviet[i].Hex.neighboors[y].Unit.Hex = newHex;
                                            }
                                            else
                                            {
                                                //geri cekilemiyorsa
                                                HolyGrasslandHex HexOfDefender = Soviet[i].Hex.neighboors[y].Unit.Hex;
                                                Unit dyingUnit = Soviet[i].Hex.neighboors[y].Unit;

                                                Soviet[i].Hex.neighboors[y].Unit.createDestroyedObject();
                                                Soviet[i].Hex.neighboors[y].Unit.Hex = null;

                                                HexOfDefender.Unit = null;
                                                dyingUnit.transform.position = new Vector3(100, 199, 199);
                                                dyingUnit.statusEffectObject.transform.position = new Vector3(23, 23, 23);
                                                dyingUnit.isDestroyed = true;
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                    }
                                }
                            }
                            else if (Soviet[i].Hex.neighboors[y].Unit == null && Soviet[i].Hex.neighboors[y].HexType == 1)
                            {
                                //bos bir dusman hexi varsa
                                Soviet[i].Hex.neighboors[y].Unit = Soviet[i];
                                Soviet[i].Hex.Unit = null;
                                HolyGrasslandHex hexHolder;
                                hexHolder = Soviet[i].Hex.neighboors[y];
                                Soviet[i].Hex = hexHolder;

                            }
                            else
                            {
                                moveDefensibleHex(Soviet[i]);
                                if(Soviet[i].Hex.isDefensible == true && amINearAnEnemy(Soviet[i], 1))
                                {
                                    //Soviet[i].status = 2;
                                }
                            }
                        }
                    }
                }

                // Will make an attack move order if enemy divisio n 2(hex)
                // Will go to reservist if not near an enemy 3(hex)
                // Will go to active if reservist count is bigger than 5
            }


            for(int i = 0; i < Soviet.Length; i++)
            {
                if(!amINearAnEnemy(Soviet[i], 2) && Soviet[i].Hex != null)
                {
                    for(int y = 0; y < Soviet[i].Hex.neighboors.Length; y++)
                    {
                        if(Soviet[i].Hex.neighboors[y].HexType == 1 && Soviet[i].Hex.neighboors[y].Unit == null)
                        {
                            Soviet[i].Hex.neighboors[y].Unit = Soviet[i];
                            Soviet[i].Hex.Unit = null;
                            HolyGrasslandHex hexHolder;
                            hexHolder = Soviet[i].Hex.neighboors[y];
                            Soviet[i].Hex = hexHolder;
                        }
                        else if(Soviet[i].Hex.neighboors[y].Unit == null)
                        {
                            Soviet[i].Hex.neighboors[y].Unit = Soviet[i];
                            Soviet[i].Hex.Unit = null;
                            HolyGrasslandHex hexHolder;
                            hexHolder = Soviet[i].Hex.neighboors[y];
                            Soviet[i].Hex = hexHolder;
                        }
                    }
                }
            }
            if (strenth_index > 1 && counterMAIN > 50)
            {
                for(int i = 0; i < Soviet.Length; i++)
                {
                    Soviet[i].status = 0;
                    Soviet[i].MovementValue = 3;
                }
            }
        }
        else
        {
            //Defensive Plan
            //Debug.Log("Retreating an Empty Hex");
            for (int i = 0; i < Soviet.Length; i++)
            {
                if (Soviet[i].MovementValue != 0 && Soviet[i] != null && Soviet[i].Hex != null && Soviet[i].Hex.neighboors != null && Soviet[i].status != 2)
                {
                    for (int j = 0; j < Soviet[i].Hex.neighboors.Length; j++)
                    {
                        if (Soviet[i].Hex.neighboors[j].Unit == null && Soviet[i].Hex.neighboors[j].HexType == 0 && (Soviet[i].Hex.isDefensible == false || Soviet[i].Hex.isVictoryPoint == false))
                        {
                            if (Soviet[i].phase == 0 || (Soviet[i].phase == 1 && counterMAIN > 3) || (Soviet[i].phase == 2 && counterMAIN > 5))
                            {
                                moveAIorderEmptyHex(Soviet[i], Soviet[i].Hex.neighboors[j]);
                                if (Soviet[i] != null && Soviet[i].Hex != null && Soviet[i].Hex.neighboors != null)
                                {
                                    moveDefensibleHex(Soviet[i]);
                                }

                                Soviet[i].MovementValue = 0;

                                break;
                            }

                        }
                    }
                }
            }
            for (int i = 0; i < Soviet.Length; i++)
            {
                if (Soviet[i] != null)
                {
                    if (Soviet[i].Hex != null)
                    {
                        if (Soviet[i].Hex.isDefensible == false || Soviet[i].Hex.isVictoryPoint == false)
                        {
                            Soviet[i].status = 0;
                        }
                    }
                }
            }
            // Will retreat if its srrounded and near an enemy

            // Retreats to a empty hex
            // Find a "Defensible Terrain"
            // Does not retreat if near victory point
            // Will transform to reservist division if not near enemy and near a friendly division
            for(int i = 0; i < Soviet.Length; i++)
            {
                moveDefensibleHex(Soviet[i]);
                if (Soviet[i].Hex.isDefensible == true && amINearAnEnemy(Soviet[i], 3))
                {
                    Soviet[i].status = 2;
                }
            }
        }

        COUNTERGIVER = 1;

        for (int i = 0; i < Soviet.Length; i++)
        {
            moveDefensibleHex(Soviet[i]);
            if (Soviet[i].Hex.isDefensible == true)
            {
                Soviet[i].status = 2;
            }
            else
            {
                Soviet[i].MovementValue = 1;
                Soviet[i].status = 0;
            }
        }
    }
}
