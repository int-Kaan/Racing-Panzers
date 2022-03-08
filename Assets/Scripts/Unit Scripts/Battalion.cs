using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Battalion : MonoBehaviour
{
    //Brigade - Battalion - Reggiment - Air Squadron

    //Division HQ
    public Unit HQDivision;
    //Name
    public string Name;
    //Moral
    [NonSerialized]
    public int moral;
    public int baseMoral;
    //Manpower
    [NonSerialized]
    public int manpower;
    public int baseManpower;

   
    public void increaseMPCount(int MPreinforcement)
    {
        manpower += MPreinforcement;
        if(manpower > baseManpower)
        {
            manpower = baseManpower;
        }
    }

    //Tank Count
    [NonSerialized]
    public int tankCount;
    public int baseTankCount;

    public void increaseAFVCount(int AFVreinforcement)
    {
        tankCount += AFVreinforcement;
        if(tankCount > baseTankCount)
        {
            tankCount = baseTankCount;
        }
    }
    //Artilery Count
    [NonSerialized]
    public int artileryCount;
    public int baseArtilleryCount;

    public void increaseARTCount(int ARTreinforcement)
    {
        artileryCount += ARTreinforcement;
        if (artileryCount > baseArtilleryCount)
        {
            artileryCount = baseArtilleryCount;
        }
    }
    [NonSerialized]
    public int AACount;

    public void updateAACount()
    {
        this.AACount = artileryCount / 4;
    }
    [NonSerialized]
    public int ATCount;

    public void updateATCount()
    {
        this.ATCount = artileryCount / 6;
    }
    //Armor
    [NonSerialized]
    public int armor;

    //Eqipment 
    // 0 - 100
    
    [NonSerialized]
    public double baseEquipment;
    [NonSerialized]
    public double cumulativeEquipment;
    public void initialEquipment()
    {
        this.baseEquipment = artileryCount + tankCount + manpower / 5;
    }
    public void cumulativeEquip()
    {
        this.cumulativeEquipment = artileryCount + tankCount + manpower / 5;
    }
    public double equipmentDegree()
    {
        return this.cumulativeEquipment / this.baseEquipment;
    }

    public void calculateArmorValue(){
        this.armor = tankCount / 3;
    }
    //Armor Penetration
    [NonSerialized]
    public int armorpen;

    public void calculateArmorPenetrationValue()
    {
        int counter = 0;
        this.armorpen = artileryCount / 3 + tankCount; // Base Armor Penetration
        if (HQDivision.styleofDivision == 0 && HQDivision.returnSupportCompanies() != null && counter == 0)
        {
            SupportCompanies[] supportingCompanies = HQDivision.returnSupportCompanies();

            for (int i = 0; i < supportingCompanies.Length; i++)
            {
                this.armorpen = armorpen + supportingCompanies[i].AT;
            }
            counter = 1;
        }
        else
        {
            counter = 0;
        }
    }
    //Firepower
    [NonSerialized]
    public int firepower;


    //takeDamage 
    /*
             Firepower deals damage to brigade
             
              Firepower deals full damage to manpower
                        deals 1/3 damage to artilery
                        deals 1/5 damage to AFV
             CV reduces damage taken
             If enemy cant penetrates armor take HALF damage
             */

    public void takeDamage(Battalion enemyBattalion)
    {
        enemyBattalion.calculatefirepower();
        //takes half damage if not penetrated 
        double EntrenchmentBonus = 1;
        if (this.HQDivision.isEntrenched == true)
        {
            if (this.HQDivision.entrenchment == 1)
            {
                EntrenchmentBonus = 1.5;
            }
            if (this.HQDivision.entrenchment == 2)
            {
                EntrenchmentBonus = 2;
            }
            if (this.HQDivision.entrenchment == 3)
            {
                EntrenchmentBonus = 2.5;
            }
            if (this.HQDivision.entrenchment == 4)
            {
                EntrenchmentBonus = 3;
            }
            if (this.HQDivision.entrenchment == 5)
            {
                EntrenchmentBonus = 3.5;
            }
        }

        if (enemyBattalion.HQDivision.CombatValue > HQDivision.CombatValue)
        {
            if (this.armor > enemyBattalion.armorpen)
            {
                int manpowerdamage = (int)(((enemyBattalion.firepower) + (enemyBattalion.firepower) * ((int)(enemyBattalion.HQDivision.CombatValue * 100) - ((int)(HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);
                int artilerydamage = (int)((((enemyBattalion.firepower / 10) + (enemyBattalion.firepower / 10) * ((int)(enemyBattalion.HQDivision.CombatValue * 100) - ((int)(HQDivision.CombatValue * 100))) / 100) / 6) / EntrenchmentBonus);
                int tankdamage = (int)(((enemyBattalion.firepower / 20) + (enemyBattalion.firepower / 20) * ((int)(enemyBattalion.HQDivision.CombatValue * 100) - ((int)(HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);
                if (HQDivision.isEntrenched == true)// If defender is entreched gain support company defensive bonus
                {
                    int damageReduction = 0;
                    if (enemyBattalion.HQDivision.styleofDivision == 0 && enemyBattalion.HQDivision.returnSupportCompanies() != null)
                    {
                        SupportCompanies[] supportingCompanies = enemyBattalion.HQDivision.returnSupportCompanies();

                        for (int i = 0; i < supportingCompanies.Length; i++)
                        {
                            damageReduction += supportingCompanies[i].EntrenchmentDefence;
                        }
                    }
                    manpowerdamage = manpowerdamage - damageReduction;
                    artilerydamage = artilerydamage - damageReduction / 5;
                    tankdamage = tankdamage - damageReduction / 10;
                    damageReduction = 0;
                }
                if (manpowerdamage < 0)
                {
                    manpowerdamage = 0;
                }
                if(artilerydamage < 0)
                {
                    artilerydamage = 0;
                }
                if(tankdamage < 0)
                {
                    tankdamage = 0;
                }
                if(HQDivision.styleofDivision == 0 && HQDivision.returnSupportCompanies() != null)
                {

                }

                this.manpower = this.manpower - manpowerdamage;
                this.artileryCount = this.artileryCount - artilerydamage;
                this.tankCount = this.tankCount - tankdamage;
            }
            else
            {
                int manpowerdamage = (int)((enemyBattalion.firepower + enemyBattalion.firepower * ((int)(enemyBattalion.HQDivision.CombatValue * 100) - ((int)(HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);
                int artilerydamage = (int)((((enemyBattalion.firepower / 5) + (enemyBattalion.firepower / 5) * ((int)(enemyBattalion.HQDivision.CombatValue * 100) - ((int)(HQDivision.CombatValue * 100))) / 100) / 6) / EntrenchmentBonus);
                int tankdamage = (int)(((enemyBattalion.firepower / 10) + (enemyBattalion.firepower / 10) * ((int)(enemyBattalion.HQDivision.CombatValue * 100) - ((int)(HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);
                if (HQDivision.isEntrenched == true)// If defender is entreched gain support company defensive bonus
                {
                    int damageReduction = 0;
                    if (enemyBattalion.HQDivision.styleofDivision == 0 && enemyBattalion.HQDivision.returnSupportCompanies() != null)
                    {
                        SupportCompanies[] supportingCompanies = enemyBattalion.HQDivision.returnSupportCompanies();

                        for (int i = 0; i < supportingCompanies.Length; i++)
                        {
                            damageReduction += supportingCompanies[i].EntrenchmentDefence;
                        }
                    }
                    manpowerdamage = manpowerdamage - damageReduction;
                    artilerydamage = artilerydamage - damageReduction / 5;
                    tankdamage = tankdamage - damageReduction / 10;
                    damageReduction = 0;
                }
                if (manpowerdamage < 0)
                {
                    manpowerdamage = 0;
                }
                if (artilerydamage < 0)
                {
                    artilerydamage = 0;
                }
                if (tankdamage < 0)
                {
                    tankdamage = 0;
                }
                this.manpower = this.manpower - manpowerdamage;
                this.artileryCount = this.artileryCount - artilerydamage;
                this.tankCount = this.tankCount - tankdamage;
            }
        }
        else
        {
            if (this.armor > enemyBattalion.armorpen)
            {
                int manpowerdamage = (int)(((enemyBattalion.firepower) + (enemyBattalion.firepower) * ((int)(HQDivision.CombatValue * 100) - ((int)(enemyBattalion.HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);
                int artilerydamage = (int)((((enemyBattalion.firepower / 10) + (enemyBattalion.firepower / 10) * ((int)(HQDivision.CombatValue * 100) - ((int)(enemyBattalion.HQDivision.CombatValue * 100))) / 100) / 6) / EntrenchmentBonus);
                int tankdamage = (int)(((enemyBattalion.firepower / 20) + (enemyBattalion.firepower / 20) * ((int)(HQDivision.CombatValue * 100) - ((int)(enemyBattalion.HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);
                if (HQDivision.isEntrenched == true)// If defender is entreched gain support company defensive bonus
                {
                    int damageReduction = 0;
                    if (enemyBattalion.HQDivision.styleofDivision == 0 && enemyBattalion.HQDivision.returnSupportCompanies() != null)
                    {
                        SupportCompanies[] supportingCompanies = enemyBattalion.HQDivision.returnSupportCompanies();

                        for (int i = 0; i < supportingCompanies.Length; i++)
                        {
                            damageReduction += supportingCompanies[i].EntrenchmentDefence;
                        }
                    }
                    manpowerdamage = manpowerdamage - damageReduction;
                    artilerydamage = artilerydamage - damageReduction / 5;
                    tankdamage = tankdamage - damageReduction / 10;
                    damageReduction = 0;
                }

                if (manpowerdamage < 0)
                {
                    manpowerdamage = 0;
                }
                if (artilerydamage < 0)
                {
                    artilerydamage = 0;
                }
                if (tankdamage < 0)
                {
                    tankdamage = 0;
                }
                this.manpower = this.manpower - manpowerdamage;
                this.artileryCount = this.artileryCount - artilerydamage;
                this.tankCount = this.tankCount - tankdamage;

            }
            else
            {
                int manpowerdamage = (int)((enemyBattalion.firepower + enemyBattalion.firepower * 2 * ((int)(HQDivision.CombatValue * 100) - ((int)(enemyBattalion.HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);
                int artilerydamage = (int)((((enemyBattalion.firepower / 5) + (enemyBattalion.firepower / 5) * ((int)(HQDivision.CombatValue * 100) - ((int)(enemyBattalion.HQDivision.CombatValue * 100))) / 100) / 6) / EntrenchmentBonus);
                int tankdamage = (int)(((enemyBattalion.firepower / 10) + (enemyBattalion.firepower / 10) * ((int)(HQDivision.CombatValue * 100) - ((int)(enemyBattalion.HQDivision.CombatValue * 100))) / 100) / EntrenchmentBonus);

                int damageReduction = 0;
                if(HQDivision.isEntrenched == true)// If defender is entreched gain support company defensive bonus
                {
                    if (enemyBattalion.HQDivision.styleofDivision == 0 && enemyBattalion.HQDivision.returnSupportCompanies() != null)
                    {
                        SupportCompanies[] supportingCompanies = enemyBattalion.HQDivision.returnSupportCompanies();

                        for (int i = 0; i < supportingCompanies.Length; i++)
                        {
                            damageReduction += supportingCompanies[i].EntrenchmentDefence;
                        }
                    }
                    manpowerdamage = manpowerdamage - damageReduction;
                    artilerydamage = artilerydamage - damageReduction / 5;
                    tankdamage = tankdamage - damageReduction / 10;
                    damageReduction = 0;
                }

                if (manpowerdamage < 0)
                {
                    manpowerdamage = 0;
                }
                if (artilerydamage < 0)
                {
                    artilerydamage = 0;
                }
                if (tankdamage < 0)
                {
                    tankdamage = 0;
                }
                this.manpower = this.manpower - manpowerdamage;
                this.artileryCount = this.artileryCount - artilerydamage;
                this.tankCount = this.tankCount - tankdamage;
            }
        }
        
    }
    public void calculatefirepower()
    {
        int counter = 0;
        double h;
        h = (tankCount + artileryCount / 3 + moral / 2); // Base Attack
        double x =  (h * HQDivision.CombatValue) * equipmentDegree(); // Attack With Combat Value and Equipment Loss
        int fatigueDamage = (int)UnityEngine.Random.Range(1, 100); // Random Fatigue Damage 
        this.firepower = (int)x +  fatigueDamage + manpower/1000; // Final Firepower with Manpower
        
        if(this.HQDivision.isEntrenched == true)
        {
            if(this.HQDivision.entrenchment == 1)
            {
                firepower = firepower + 150;
            }
            if (this.HQDivision.entrenchment == 2)
            {
                firepower = firepower + 250;
            }
            if (this.HQDivision.entrenchment == 3)
            {
                firepower = firepower + 350;
            }
            if (this.HQDivision.entrenchment == 4)
            {
                firepower = firepower + 450;
            }
            if (this.HQDivision.entrenchment == 5)
            {
                firepower = firepower + 600;
            }
        }
        if (HQDivision.styleofDivision == 0 && HQDivision.returnSupportCompanies() != null && counter == 0)
        {
            SupportCompanies[] supportingCompanies = HQDivision.returnSupportCompanies();

            for(int i = 0; i < supportingCompanies.Length; i++)
            {
                this.firepower = this.firepower + supportingCompanies[i].GroudAttack;
            }
            counter = 1;
        }
        else
        {
            counter = 0;
        }
        if (this.firepower < 0)
        {
            firepower = 1;
        }
        
    }
    public void calculateAAFirepower()
    { 

    }
        private void Start()
    {
        this.moral = baseMoral;
        this.manpower = baseManpower;
        this.tankCount = baseTankCount;
        this.artileryCount = baseArtilleryCount;
        initialEquipment();
        cumulativeEquip();
        calculatefirepower();
        calculateArmorPenetrationValue();
        calculateArmorValue();
        
        
        
   
    }
    private void Update()
    {

        cumulativeEquip();
        calculatefirepower();
        calculateArmorPenetrationValue();
        calculateArmorValue();
        updateATCount();
        updateAACount();

        

        if (cumulativeEquipment < 0)
        {
            cumulativeEquipment = 0;
        }
        if(moral < 0)
        {
            moral = 0;
        }
        if(tankCount < 0)
        {
            tankCount = 0;

        }
        if (manpower < 0)
        {
            manpower = 0;

        }
        if (artileryCount < 0)
        {
            artileryCount = 0;

        }
        if (armor < 0)
        {
            armor = 0;

        }
        if (armorpen < 0)
        {
            armorpen = 0;

        }
        if (firepower < 0)
        {
            firepower = 0;

        }
    }

}
