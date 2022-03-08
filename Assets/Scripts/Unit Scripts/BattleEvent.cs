using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleEvent : MonoBehaviour
{
    // Calculates the battle event , returns true if attacker sucseeds
    public Canvas battleCanvas;
    public GameObject battleInterface;

    public Text AttackerDivisionName;
    public Text DefenderDivisionName;

    public SmallBattleScreen smallbattlescreen;
    
    


    //ATTACKER INFORMATION HOLDERS
    public Text AttackerDivisionTURN1_INFO;
    public Text AttackerDivisionTURN2_INFO;
    public Text AttackerDivisionTURN3_INFO;
    public Text AttackerDivisionTURN4_INFO;
    public Text AttackerDivisionTURN5_INFO;

    public Text AttackerDivisionTURN1_LOSSES;
    public Text AttackerDivisionTURN2_LOSSES;
    public Text AttackerDivisionTURN3_LOSSES;
    public Text AttackerDivisionTURN4_LOSSES;
    public Text AttackerDivisionTURN5_LOSSES;


    //DEFENDER INFORMATION HOLDERS
    public Text DefenderDivisionTURN1_INFO;
    public Text DefenderDivisionTURN2_INFO;
    public Text DefenderDivisionTURN3_INFO;
    public Text DefenderDivisionTURN4_INFO;
    public Text DefenderDivisionTURN5_INFO;

    public Text DefenderDivisionTURN1_LOSSES;
    public Text DefenderDivisionTURN2_LOSSES;
    public Text DefenderDivisionTURN3_LOSSES;
    public Text DefenderDivisionTURN4_LOSSES;
    public Text DefenderDivisionTURN5_LOSSES;

    //DIVISION THINKS
    /*UiTileDivision.ArmyGeneral.text = this.armyHQ.General;
                UiTileDivision.DivisionCom.text = this.divisionComName;
                UiTileDivision.Equipment.text = new string((this.equipmentRatio() * 100).ToString().Take(4).ToArray()) + "%";
                UiTileDivision.AFV.text = "AFV : " + this.tankCount.ToString();
                UiTileDivision.Artilery.text = "Artilery : " + this.artileryCount.ToString();
                UiTileDivision.DivisionName.text = divisionName;
                UiTileDivision.CV.text = "QUALITY : " + returnCV().ToString();*/



    public Text AttackerDivisionGeneralName;
    public Text DefenderDivisionGeneralName;

    
    public Button exitButton;

    int turnCounter = 0;
    public bool battleEvent(Unit attacker , Unit defender)
    {
        fixUnit(attacker);
        fixUnit(defender);
        int attackerStartingManpowerCount = attacker.returnManpower();
        int attackerStartingTankCount = attacker.returnTank();
        int attackerStartingArtCount = attacker.returnART();
        int defenderStartingManpowerCount = defender.returnManpower();
        int defenderStartingTankCount = defender.returnTank();
        int defenderStartingArtCount = defender.returnART();


        this.AttackerDivisionGeneralName.text = attacker.divisionComName;
        this.DefenderDivisionGeneralName.text = defender.divisionComName;

        this.AttackerDivisionName.text = attacker.divisionName;
        this.DefenderDivisionName.text = defender.divisionName;
        if (attacker != null && defender != null)
        {


            if (battleInterface != null)
            {
                battleInterface.SetActive(true);
                //battleCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            }

            /*
             Firepower deals damage to brigade
             
              Firepower deals full damage to manpower
                        deals 1/3 damage to artilery
                        deals 1/5 damage to AFV
             
             
             */
            
            int ATTmanpowercalculation;
            int ATTafvcalculation;
            int ATTartcalculation;

            int DEFmanpowercalculation;
            int DEFafvcalculation;
            int DEFartcalculation;

            //FIRST TURN
            int randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            int randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            fixUnit(attacker);
            fixUnit(defender);

            int PREattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            int PREattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            int PREattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            int PREdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            int PREdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            int PREdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            

            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);
            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);

            fixUnit(attacker);
            fixUnit(defender);

            int AFTERattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            int AFTERattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            int AFTERattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            int AFTERdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            int AFTERdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            int AFTERdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            AttackerDivisionTURN1_INFO.text = attacker.battalions[randomAttackerBat].name;
            DefenderDivisionTURN1_INFO.text = defender.battalions[randomDefenderBat].name;
            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation = 0;
            }
            else
            {
                ATTmanpowercalculation = PREattackerManpower - AFTERattackerManpower;
            }
            if(PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0 )
            {
                ATTafvcalculation = 0;
            }
            else { 
                ATTafvcalculation = PREattackerAFV - AFTERattackerAFV;
            }
            if(PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation = 0;
            }
            else
            {
                ATTartcalculation = PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation = 0;
            }
            else
            {
                DEFmanpowercalculation = PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV< 0)
            {
                DEFafvcalculation = 0;
            }
            else
            {
                DEFafvcalculation = PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation = 0;
            }
            else
            {
                DEFartcalculation = PREdefenderART - AFTERdefenderART;
            }
            AttackerDivisionTURN1_LOSSES.text = "-"+ (ATTmanpowercalculation).ToString() + "\n -" + (ATTafvcalculation).ToString() + "\n -" + (ATTartcalculation).ToString();
            AttackerDivisionTURN1_LOSSES.color = new Color32(200, 10, 0, 255);

            DefenderDivisionTURN1_LOSSES.text = "-" + (DEFmanpowercalculation).ToString() + "\n -" + (DEFafvcalculation).ToString() + "\n -" + (DEFartcalculation).ToString();
            DefenderDivisionTURN1_LOSSES.color = new Color32(200, 10, 0, 255);


            //TURN 2

            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            fixUnit(attacker);
            fixUnit(defender);

            PREattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            PREdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);
            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);
 
             AttackerDivisionTURN2_INFO.text = attacker.battalions[randomAttackerBat].name;
             DefenderDivisionTURN2_INFO.text = defender.battalions[randomDefenderBat].name;

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation = 0;
            }
            else
            {
                ATTmanpowercalculation = PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation = 0;
            }
            else
            {
                ATTafvcalculation = PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation = 0;
            }
            else
            {
                ATTartcalculation = PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation = 0;
            }
            else
            {
                DEFmanpowercalculation = PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation = 0;
            }
            else
            {
                DEFafvcalculation = PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation = 0;
            }
            else
            {
                DEFartcalculation = PREdefenderART - AFTERdefenderART;
            }
            AttackerDivisionTURN2_LOSSES.text = "-" + (ATTmanpowercalculation).ToString() + "\n -" + (ATTafvcalculation).ToString() + "\n -" + (ATTartcalculation).ToString();
            AttackerDivisionTURN2_LOSSES.color = new Color32(200, 10, 0, 255);

            DefenderDivisionTURN2_LOSSES.text = "-" + (DEFmanpowercalculation).ToString() + "\n -" + (DEFafvcalculation).ToString() + "\n -" + (DEFartcalculation).ToString();
            DefenderDivisionTURN2_LOSSES.color = new Color32(200, 10, 0, 255);




            //3

            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            fixUnit(attacker);
            fixUnit(defender);

            PREattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            PREdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);
            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART = defender.battalions[randomDefenderBat].artileryCount;
            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation = 0;
            }
            else
            {
                ATTmanpowercalculation = PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation = 0;
            }
            else
            {
                ATTafvcalculation = PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation = 0;
            }
            else
            {
                ATTartcalculation = PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation = 0;
            }
            else
            {
                DEFmanpowercalculation = PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation = 0;
            }
            else
            {
                DEFafvcalculation = PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation = 0;
            }
            else
            {
                DEFartcalculation = PREdefenderART - AFTERdefenderART;
            }
            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            PREattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            fixUnit(attacker);
            fixUnit(defender);

            PREdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART += defender.battalions[randomDefenderBat].artileryCount;

            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);
            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART += defender.battalions[randomDefenderBat].artileryCount;
            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation = 0;
            }
            else
            {
                ATTmanpowercalculation += PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation += 0;
            }
            else
            {
                ATTafvcalculation += PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation += 0;
            }
            else
            {
                ATTartcalculation += PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation += 0;
            }
            else
            {
                DEFmanpowercalculation += PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation += 0;
            }
            else
            {
                DEFafvcalculation += PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation += 0;
            }
            else
            {
                DEFartcalculation += PREdefenderART - AFTERdefenderART;
            }
            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            fixUnit(attacker);
            fixUnit(defender);

            PREattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            PREdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART += defender.battalions[randomDefenderBat].artileryCount;

            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);
            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART += defender.battalions[randomDefenderBat].artileryCount;


            AttackerDivisionTURN3_INFO.text = attacker.divisionName;
            DefenderDivisionTURN3_INFO.text = defender.divisionName;


            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation += 0;
            }
            else
            {
                ATTmanpowercalculation += PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation += 0;
            }
            else
            {
                ATTafvcalculation += PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation += 0;
            }
            else
            {
                ATTartcalculation += PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation += 0;
            }
            else
            {
                DEFmanpowercalculation += PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation += 0;
            }
            else
            {
                DEFafvcalculation += PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation += 0;
            }
            else
            {
                DEFartcalculation += PREdefenderART - AFTERdefenderART;

            }
            fixUnit(attacker);
            fixUnit(defender);

            AttackerDivisionTURN3_LOSSES.text = "-" + (ATTmanpowercalculation).ToString() + "\n -" + (ATTafvcalculation).ToString() + "\n -" + (ATTartcalculation).ToString();
            AttackerDivisionTURN3_LOSSES.color = new Color32(200, 10, 0, 255);

            DefenderDivisionTURN3_LOSSES.text = "-" + (DEFmanpowercalculation).ToString() + "\n -" + (DEFafvcalculation).ToString() + "\n -" + (DEFartcalculation).ToString();
            DefenderDivisionTURN3_LOSSES.color = new Color32(200, 10, 0, 255);

            fixUnit(attacker);
            fixUnit(defender);
            //4
            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            PREattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            PREdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            fixUnit(attacker);
            fixUnit(defender);

            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);
            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART = defender.battalions[randomDefenderBat].artileryCount;
            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation = 0;
            }
            else
            {
                ATTmanpowercalculation = PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation = 0;
            }
            else
            {
                ATTafvcalculation = PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation = 0;
            }
            else
            {
                ATTartcalculation = PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation = 0;
            }
            else
            {
                DEFmanpowercalculation = PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation = 0;
            }
            else
            {
                DEFafvcalculation = PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation = 0;
            }
            else
            {
                DEFartcalculation = PREdefenderART - AFTERdefenderART;
            }
            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            fixUnit(attacker);
            fixUnit(defender);

            PREattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            PREdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART += defender.battalions[randomDefenderBat].artileryCount;

            
            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);
            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART += defender.battalions[randomDefenderBat].artileryCount;
            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation = 0;
            }
            else
            {
                ATTmanpowercalculation += PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation += 0;
            }
            else
            {
                ATTafvcalculation += PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation += 0;
            }
            else
            {
                ATTartcalculation += PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation += 0;
            }
            else
            {
                DEFmanpowercalculation += PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation += 0;
            }
            else
            {
                DEFafvcalculation += PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation += 0;
            }
            else
            {
                DEFartcalculation += PREdefenderART - AFTERdefenderART;
            }
            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            fixUnit(attacker);
            fixUnit(defender);

            PREattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            PREdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART += defender.battalions[randomDefenderBat].artileryCount;

            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);
            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower += attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV += attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART += attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower += defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV += defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART += defender.battalions[randomDefenderBat].artileryCount;


            AttackerDivisionTURN4_INFO.text = attacker.divisionName;
            DefenderDivisionTURN4_INFO.text = defender.divisionName;


            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation += 0;
            }
            else
            {
                ATTmanpowercalculation += PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation += 0;
            }
            else
            {
                ATTafvcalculation += PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation += 0;
            }
            else
            {
                ATTartcalculation += PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation += 0;
            }
            else
            {
                DEFmanpowercalculation += PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation += 0;
            }
            else
            {
                DEFafvcalculation += PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation += 0;
            }
            else
            {
                DEFartcalculation += PREdefenderART - AFTERdefenderART;
            }
            AttackerDivisionTURN4_LOSSES.text = "-" + (ATTmanpowercalculation).ToString() + "\n -" + (ATTafvcalculation).ToString() + "\n -" + (ATTartcalculation).ToString();
            AttackerDivisionTURN4_LOSSES.color = new Color32(200, 10, 0, 255);

            DefenderDivisionTURN4_LOSSES.text = "-" + (DEFmanpowercalculation).ToString() + "\n -" + (DEFafvcalculation).ToString() + "\n -" + (DEFartcalculation).ToString();
            DefenderDivisionTURN4_LOSSES.color = new Color32(200, 10, 0, 255);

            //5

            randomAttackerBat = (int)Random.Range(0, attacker.battalions.Length);
            randomDefenderBat = (int)Random.Range(0, defender.battalions.Length);

            fixUnit(attacker);
            fixUnit(defender);

            PREattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            PREattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            PREattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            PREdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            PREdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            PREdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            defender.battalions[randomDefenderBat].takeDamage(attacker.battalions[randomAttackerBat]);
            attacker.battalions[randomAttackerBat].takeDamage(defender.battalions[randomDefenderBat]);

            fixUnit(attacker);
            fixUnit(defender);

            AFTERattackerManpower = attacker.battalions[randomAttackerBat].manpower;
            AFTERattackerAFV = attacker.battalions[randomAttackerBat].tankCount;
            AFTERattackerART = attacker.battalions[randomAttackerBat].artileryCount;

            AFTERdefenderManpower = defender.battalions[randomDefenderBat].manpower;
            AFTERdefenderAFV = defender.battalions[randomDefenderBat].tankCount;
            AFTERdefenderART = defender.battalions[randomDefenderBat].artileryCount;

            //ATT
            if (PREattackerManpower - AFTERattackerManpower < 0 || AFTERattackerManpower < 0)
            {
                ATTmanpowercalculation = 0;
            }
            else
            {
                ATTmanpowercalculation = PREattackerManpower - AFTERattackerManpower;
            }
            if (PREattackerAFV - AFTERattackerAFV < 0 || AFTERattackerAFV < 0)
            {
                ATTafvcalculation = 0;
            }
            else
            {
                ATTafvcalculation = PREattackerAFV - AFTERattackerAFV;
            }
            if (PREattackerART - AFTERattackerART < 0 || AFTERattackerART < 0)
            {
                ATTartcalculation = 0;
            }
            else
            {
                ATTartcalculation = PREattackerART - AFTERattackerART;
            }
            //DEF
            if (PREdefenderManpower - AFTERdefenderManpower < 0 || AFTERdefenderManpower < 0)
            {
                DEFmanpowercalculation = 0;
            }
            else
            {
                DEFmanpowercalculation = PREdefenderManpower - AFTERdefenderManpower;
            }
            if (PREdefenderAFV - AFTERdefenderAFV < 0 || AFTERdefenderAFV < 0)
            {
                DEFafvcalculation = 0;
            }
            else
            {
                DEFafvcalculation = PREdefenderAFV - AFTERdefenderAFV;
            }
            if (PREdefenderART - AFTERdefenderART < 0 || AFTERdefenderART < 0)
            {
                DEFartcalculation = 0;
            }
            else
            {
                DEFartcalculation = PREdefenderART - AFTERdefenderART;
            }
            fixUnit(attacker);
            fixUnit(defender);

            AttackerDivisionTURN5_LOSSES.text = "-" + (ATTmanpowercalculation).ToString() + "\n -" + (ATTafvcalculation).ToString() + "\n -" + (ATTartcalculation).ToString();
            AttackerDivisionTURN5_LOSSES.color = new Color32(200, 10, 0, 255);

            DefenderDivisionTURN5_LOSSES.text = "-" + (DEFmanpowercalculation).ToString() + "\n -"  + (DEFafvcalculation).ToString() + "\n -" + (DEFartcalculation).ToString();
            DefenderDivisionTURN5_LOSSES.color = new Color32(200, 10, 0, 255);


            AttackerDivisionTURN5_INFO.text = attacker.battalions[randomAttackerBat].name;
            DefenderDivisionTURN5_INFO.text = defender.battalions[randomDefenderBat].name;

            defender.calculateCombatValue();
            attacker.calculateCombatValue();

            fixUnit(attacker);
            fixUnit(defender);

            for (int i = 0; i < defender.battalions.Length; i++)
            {
                defender.battalions[i].moral++;
            }
            for (int i = 0; i < attacker.battalions.Length; i++)
            {
                attacker.battalions[i].moral++;
            }

            int attackerRemainingManpowerCount = attacker.returnManpower();
            int attackerRemainingTankCount = attacker.returnTank();
            int attackerRemainingArtCount = attacker.returnART();
            int defenderRemainingManpowerCount = defender.returnManpower();
            int defenderRemainingTankCount = defender.returnTank();
            int defenderRemainingArtCount = defender.returnART();

            fixUnit(attacker);
            fixUnit(defender);

            smallbattlescreen.ATTACKERLOSSES.text = (attackerStartingManpowerCount - attackerRemainingManpowerCount).ToString() + "/" + (attackerStartingTankCount-attackerRemainingTankCount).ToString() + "/" + (attackerStartingArtCount - attackerRemainingArtCount).ToString();
            
            smallbattlescreen.DEFENDERLOSSES.text = (defenderStartingManpowerCount - defenderRemainingManpowerCount).ToString() + "/" + (defenderStartingTankCount - defenderRemainingTankCount).ToString() + "/" + (defenderStartingArtCount - defenderRemainingArtCount).ToString();
            //attacker.status = 1;
            fixUnit(attacker);
            fixUnit(defender);

            defender.calculateDivisionSate();
            defender.calculateCombatValue();
            if ((defender.status == 3 || defender.status == 4)||defender.CombatValue < 0.50)
            {
                if(defender.CombatValue < 0.25)
                {
                    int prob1 = UnityEngine.Random.Range(0, 1);
                    if(prob1 == 1)
                    {
                        defender.isDestroyed = true;
                        defender.createDestroyedObject();
                    }
                }
                defender.MovementValue -= 1;
                attacker.MovementValue -= 1;
                //defender.status = 1;
                return true;
            }
            else
            {
                defender.MovementValue -= 1;
                attacker.MovementValue -= 1;
                //defender.status = 1;
                return false;
            }


            
            

            
        }
        return false;

    }
    private void Start()
    {
        battleInterface.SetActive(false);
        
    }
    private void Update()
    {
        exitButton.onClick.AddListener(delegate () { emptyEverything(); });

        
    }
    public void emptyEverything()
    {
        if (battleInterface != null)
        {
            battleInterface.SetActive(true);
            battleCanvas.renderMode = RenderMode.WorldSpace;
        }
        this.turnCounter = 0;
        //ATTACKER INFORMATION HOLDERS
        AttackerDivisionTURN1_INFO.text = "";
        AttackerDivisionTURN2_INFO.text = "";
        AttackerDivisionTURN3_INFO.text = "";
        AttackerDivisionTURN4_INFO.text = "";
        AttackerDivisionTURN5_INFO.text = "";

        AttackerDivisionTURN1_LOSSES.text = "";
        AttackerDivisionTURN2_LOSSES.text = "";
        AttackerDivisionTURN3_LOSSES.text = "";
        AttackerDivisionTURN4_LOSSES.text = "";
        AttackerDivisionTURN5_LOSSES.text = "";


        //DEFENDER INFORMATION HOLDERS
        DefenderDivisionTURN1_INFO.text = "";
        DefenderDivisionTURN2_INFO.text = "";
        DefenderDivisionTURN3_INFO.text = "";
        DefenderDivisionTURN4_INFO.text = "";
        DefenderDivisionTURN5_INFO.text = "";

        DefenderDivisionTURN1_LOSSES.text = "";
        DefenderDivisionTURN2_LOSSES.text = "";
        DefenderDivisionTURN3_LOSSES.text = "";
        DefenderDivisionTURN4_LOSSES.text = "";
        DefenderDivisionTURN5_LOSSES.text = "";

        AttackerDivisionName.text ="";
        DefenderDivisionName.text ="";

}
    public void fixUnit(Unit unit)
    {
        for (int i = 0; i < unit.battalions.Length; i++)
        {
            if(unit.battalions[i].manpower < 0)
            {
                unit.battalions[i].manpower = 0;
            }
            if (unit.battalions[i].tankCount < 0)
            {
                unit.battalions[i].tankCount = 0;
            }
            if (unit.battalions[i].artileryCount < 0)
            {
                unit.battalions[i].artileryCount = 0;
            }
        }
    }
}

