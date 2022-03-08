using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Selecter : MonoBehaviour
{

    public Unit unit;

    public Unit defenderUnit;

    public HolyGrasslandHex hex;

    public HolyGrasslandHex oldHex;

    public BattleEvent battleEvent;

    public HolyGrasslandHex indexHex;

    public int counterEscape;

    public bool defenderUnitAvaliable;

    public Canvas smallBattleCabvas;

    public SmallBattleScreen smallBattleScreen;

    public turnCounter turnCounterBro;
    [Header("Movement Sound")]
    public AudioSource MoveSound;
    [Header("Firefight Sounds")]
    public AudioSource FireSound;
    public AudioSource FireSound2;
    public AudioSource FireSound3;
    public void updateUnit()
    {
        if (unit != null)
        {
            defenderUnitAvaliable = true;
        }
        else
        {
            defenderUnitAvaliable = false;
        }
    }
    public void advanceToEmptyHex()
    {

        //for grassland
        if (unit == null && this.hex != null)
        {
            this.hex = null;
        }
        if (unit != null && hex != null && hex.Unit == null && unit.status != 1 && unit.status != 2)
        {
            oldHex = unit.Hex;
            if (oldHex.neighboors.Contains(hex) && hex.Unit == null && ((unit.typeOfDivision == 0 && turnCounterBro.GermanReady == true) || (unit.typeOfDivision == 1 && turnCounterBro.SovietReady == true)))
            {
                MoveSound.mute = true;
                MoveSound.mute = false;
                Vector3 audioPosition = new Vector3(unit.transform.position.x, unit.transform.position.y + 2, unit.transform.position.z);
                MoveSound.transform.position = audioPosition;
                MoveSound.Play();

                oldHex.Unit = null;
                unit.Hex = hex;
                hex.Unit = unit;
                unit.updatePosition();
                /*Vector3 vector = new Vector3();
                for(float x = 0.05f; x < hex.getPosition().x; x++)
                {
                    for(float y = 0; y < hex.getPosition().y; y++)
                    {
                        for(float z = 0; z < hex.getPosition().z; z++)
                        {
                            vector.x = x;
                            vector.y = y;
                            vector.z = z;

                            unit.transform.position = vector;

                            StartCoroutine(Sleeper());
                        }
                    }
                }*/

                if (unit.MovementValue == 0)
                {
                    this.unit.status = 1;

                }
                else
                {
                    this.unit.MovementValue -= 1;
                }

                this.unit = null;
                this.hex = null;
                this.oldHex = null;


            }
        }
    }







    public void commenceBattle()
    {
        if (defenderUnit != null && unit != null && defenderUnit.typeOfDivision != unit.typeOfDivision && unit.status != 1 & unit.status != 2 && unit.status != 3 && unit.status != 4 && ((unit.typeOfDivision == 0 && turnCounterBro.GermanReady == true) || (unit.typeOfDivision == 1 && turnCounterBro.SovietReady == true)) && doesNeighboorHaveThatDivision(unit, defenderUnit))
        {

            smallBattleCabvas.renderMode = RenderMode.ScreenSpaceCamera;
            int randomSounder = (int)UnityEngine.Random.Range(0, 2);
            if (randomSounder == 0)
            {
                FireSound.mute = true;
                FireSound.mute = false;
                Vector3 audioPosition = new Vector3(unit.transform.position.x, unit.transform.position.y + 2, unit.transform.position.z);
                FireSound.transform.position = audioPosition;
                FireSound.Play();
                FireSound3.mute = true;
                FireSound3.mute = false;
                FireSound3.transform.position = audioPosition;
                FireSound3.Play();
            }
            else if (randomSounder == 1)
            {
                FireSound2.mute = true;
                FireSound2.mute = false;
                Vector3 audioPosition = new Vector3(unit.transform.position.x, unit.transform.position.y + 2, unit.transform.position.z);
                FireSound2.transform.position = audioPosition;
                FireSound2.Play();
            }
            else if (randomSounder == 2)
            {
                FireSound3.mute = true;
                FireSound3.mute = false;
                Vector3 audioPosition = new Vector3(unit.transform.position.x, unit.transform.position.y + 2, unit.transform.position.z);
                FireSound3.transform.position = audioPosition;
                FireSound3.Play();
                FireSound2.mute = true;
                FireSound2.mute = false;
                FireSound2.transform.position = audioPosition;
                FireSound2.Play();
            }

            if (battleEvent.battleEvent(unit, defenderUnit))
            {

                //this.unit.status = 1;
                //this.defenderUnit.status = 1;



                HolyGrasslandHex escapeHex = findNeighboor(defenderUnit.Hex);
                //Kacabilmisse
                if (escapeHex != defenderUnit.Hex)
                {
                    if (defenderUnit.cumManpower == 0 && defenderUnit.cumManpower < 500)
                    {
                        defenderUnit.createDestroyedObject();
                        defenderUnit.Hex.Unit = null;
                        defenderUnit.Hex = null;
                        defenderUnit.transform.position = new Vector3(100, 199, 199);
                        defenderUnit.statusEffectObject.transform.position = new Vector3(23, 23, 23);
                        defenderUnit.isDestroyed = true;
                        //Debug.Log("Cant retreat");
                    }
                    else
                    {
                        //attackerin bosalttigi yer
                        unit.Hex.Unit = null;
                        //attackerin ilerledigi yer
                        defenderUnit.Hex.Unit = unit;
                        unit.Hex = defenderUnit.Hex;
                        //defenderin kaciti yer
                        defenderUnit.Hex = escapeHex;
                        escapeHex.Unit = defenderUnit;
                        defenderUnit.status = 0;
                    }


                }//Kacamamissa
                else
                {
                    defenderUnit.createDestroyedObject();
                    defenderUnit.Hex.Unit = null;
                    defenderUnit.Hex = null;
                    defenderUnit.transform.position = new Vector3(100, 199, 199);
                    defenderUnit.statusEffectObject.transform.position = new Vector3(23, 23, 23);
                    defenderUnit.isDestroyed = true;
                    //Debug.Log("Cant retreat");
                }



            }
            this.defenderUnit = null;
            this.unit = null;
            this.hex = null;
        }
        else
        {
            this.defenderUnit = null;
        }
        indexHex = null;


    }
    public HolyGrasslandHex findNeighboor(HolyGrasslandHex hexer)
    {

        for (int i = 0; i < hexer.neighboors.Length; i++)
        {
            if (hexer.neighboors[i].Unit == null && hexer.neighboors[i].HexType == hexer.HexType)
            {
                return hexer.neighboors[i];
            }
        }
        return hexer;
    }

    public bool doesNeighboorHaveThatDivision(Unit attacker, Unit defender)
    {
        for (int i = 0; i < attacker.Hex.neighboors.Length; i++)
        {
            if (attacker.Hex.neighboors[i].Unit == defender)
            {
                return true;
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        updateUnit();
        advanceToEmptyHex();
        if (unit != null && defenderUnit != null)
        {
            string elements = "";
            for (int i = 0; i < unit.battalions.Length; i++)
            {

                smallBattleScreen.ATTACKERDIVNAME.text = this.unit.name;
                elements += unit.battalions[i].name + " : " + unit.battalions[i].manpower + "/" + unit.battalions[i].tankCount.ToString() + "/" + unit.battalions[i].artileryCount.ToString() + "\n";

            }
            if (unit.returnSupportCompanies() != null)
            {
                var x = unit.returnSupportCompanies();
                for (int i1 = 0; i1 < x.Length; i1++)
                {
                    elements += "   " + x[i1].Name + "\n";
                }
            }

            smallBattleScreen.PARTICIPATINGATTACKEROVERLAY.text = elements;
            elements = "";
            for (int ii = 0; ii < defenderUnit.battalions.Length; ii++)
            {

                smallBattleScreen.DEFENDERDIVNAME.text = this.defenderUnit.name;
                elements += defenderUnit.battalions[ii].name + " : " + defenderUnit.battalions[ii].manpower + "/" + defenderUnit.battalions[ii].tankCount.ToString() + "/" + defenderUnit.battalions[ii].artileryCount.ToString() + "\n";

            }
            if (defenderUnit.returnSupportCompanies() != null)
            {
                var x = defenderUnit.returnSupportCompanies();
                for (int i2 = 0; i2 < x.Length; i2++)
                {
                    elements += "   " + x[i2].Name + "\n";
                }
            }
            smallBattleScreen.PARTICIPATINGDEFENDEROVERLAY.text = elements;
            elements = null;
        }


        commenceBattle();
        if (unit == null && hex != null)
        {
            hex = null;
        }
    }

}

