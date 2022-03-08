 anusing System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
public class Unit : MonoBehaviour
{
    /***************************************/
    //        DIVISION PROPERTIES          //
    /***************************************/

    [Header("HQ Settings")]
    //Army Group HQ
    public ArmyHQ armyHQ;
    [Header("Division Name")]
    public string divisionName;
    [Header("Commander Name")]
    //Division Commander
    public string divisionComName;
    [Header("Hex")]
    //Present Hex
    public HolyGrasslandHex Hex = null;

    //Division Type
    //0 german
    //1 soviet
    [Header("Type 0-> German 1->Soviet")]
    public int typeOfDivision;

    [Header("Division Image")]
    public Sprite divImage;
    public int returnTypeOfDivision()
    {
        return this.typeOfDivision;
    }
    [Header("Army")]
    //Army
    public Unit Army;

    [Header("Type 0-> Normal 1->HQ 2->Air Command")]
    //Style of Division
    public int styleofDivision;
    /***************************************/
    //           ARMY ATTRIBUTES           //
    /***************************************/
    [Header("Support Battalions")]
    [SerializeField]
    SupportCompanies[] supportCompanies;
    public SupportCompanies[] returnSupportCompanies()
    {
        if (this.Army != null && isUnitNear(this.Army))
        {
            if (this.styleofDivision == 0)
            {
                return Army.supportCompanies;
            }
            else
            {
                return supportCompanies;
            }
        }
        else if (this.Army == null && this.supportCompanies != null)
        {
            return this.supportCompanies;
        }
        else
        {
            return null;
        }


    }
    [Header("PHASE")]
    [SerializeField]
    public int phase = 0;

    [Header("Attached Divisions")]
    [SerializeField]
    Unit[] attachedDivisions;
    public Unit[] returnAttachedDivisions()
    {
        if (this.styleofDivision == 1)
        {
            return this.attachedDivisions;
        }
        else
        {
            return null;
        }
    }
    /***************************************/
    //       AIR COMMAND ATTRIBUTES         //
    /***************************************/





    /***************************************/
    //        DIVISION ATTRIBUTES          //
    /***************************************/

    //Combat Value
    //0-2

    public double BaseCombatValue;
    [NonSerialized]
    public double CombatValue;


    //Cumulative Moral
    [NonSerialized]
    public int cumMoral;

    public void calculateMoral()
    {
        this.cumMoral = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {
            cumMoral += this.battalions[i].moral;
        }
    }
    //Cumulative Manpower
    [NonSerialized]
    public int cumManpower;
    public void calculateManpower()
    {
        this.cumManpower = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            cumManpower += this.battalions[i].manpower;
        }
    }

    //Base Moral
    [NonSerialized]
    public int baseMoral;

    private void baseStartingMoral()
    {
        baseMoral = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {


            baseMoral += this.battalions[i].baseMoral;

        }
    }
    //Base Manpower
    [NonSerialized]
    public int baseManpower;
    private void baseStartingManpower()
    {
        baseManpower = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            baseManpower += this.battalions[i].baseManpower;

        }
    }
    /***************************************/
    //     DIVISION MOVEMENT MECHANICS     //
    /***************************************/
    //Unit Movement Value
    [Header("Movement Value")]
    [SerializeField]
    public int BaseMovementValue;
    [NonSerialized]
    public int MovementValue;
    public void movementUpdater()
    {
        if (MovementValue < 0 || MovementValue == 0)
        {
            if (this.status != 2)
            {
                this.status = 1;
                MovementValue = 0;
            }

        }
        if (MovementValue > BaseMovementValue)
        {
            MovementValue = BaseMovementValue;
        }

    }


    /***************************************/
    //     DIVISION ON MAP EQUIPMENT       //
    /***************************************/
    //Tank Count
    [NonSerialized]
    public int tankCount;

    private void calculateTankCount()
    {
        tankCount = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            tankCount += this.battalions[i].tankCount;

        }
    }

    [NonSerialized]
    public int baseTankCount;
    private void calculateBaseTankCount()
    {
        baseTankCount = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            baseTankCount += this.battalions[i].baseTankCount;

        }
    }

    //Artilery Count
    [NonSerialized]
    public int artileryCount;
    public void calculateArtileryCount()
    {
        artileryCount = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            artileryCount += this.battalions[i].artileryCount;

        }
    }
    [NonSerialized]
    public int artileryBaseCount;
    public void calculateBaseArtileryCount()
    {
        artileryBaseCount = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            artileryBaseCount += this.battalions[i].baseArtilleryCount;

        }
    }

    /***************************************/
    //          DIVISION EQUIPMENT         //
    /***************************************/
    [NonSerialized]
    public double baseEquipment;
    [NonSerialized]
    public double cumEquipment;
    [NonSerialized]
    public double equipmentRate;
    private void calculatebaseEq()
    {
        baseEquipment = 0;
        for (int i = 0; i < battalions.Length; i++)
        {
            baseEquipment += this.battalions[i].baseEquipment;
        }

    }
    private void calculateCumulativeEquipment()
    {
        cumEquipment = 0;
        for (int i = 0; i < battalions.Length; i++)
        {
            cumEquipment += this.battalions[i].cumulativeEquipment;
        }
    }
    public double equipmentRatio()
    {
        this.equipmentRate = cumEquipment / baseEquipment;
        return equipmentRate;
    }
    /***************************************/
    //           DIVISION STATUS           //
    /***************************************/
    //Status

    [Header("Status 0->Active 1->Forming 2->Reserve 3->Rout 4->Shattered")]
    public int status = 0;

    //  -- Outside Factors --

    //Entrenchment Bonus
    public bool isEntrenched = false;
    public int entrenchment = 0;

    //Weather Bonus
    private int weatherTYPE = 0;

    private int weatherBONUS = 0;
    public void changeWeather(int typeOfWeather)
    {
        weatherBONUS = typeOfWeather;
    }
    public void updateWeatherBonus()
    {

    }
    /***************************************/
    //        STATUS CALCULATIONS          //
    /***************************************/

    // CALCULATING COMBAT VALUE

    /*Base Combat Value = (Combat Value * (Current Morale/Cumulative Morale) * (Current Manpower/Cumulative
    Manpower)) + Entrenchment Bonus – Weather Penalty(0.04)(Only German, Blizzard)*/

    public void calculateCombatValue()
    {

        CombatValue = (BaseCombatValue * (((double)cumMoral / (double)baseMoral) * ((double)cumManpower / (double)baseManpower))) + entrenchment - weatherBONUS;

        /*if(CombatValue < 0)
        {
            CombatValue = 0;
        }*/

        /* else
         {
             CombatValue = 0;
         }*/
    }
    private double returnCV()
    {
        return CombatValue;
    }

    //Calculating Division Presence and Type

    //Status
    // 0 -> Active
    // 1 -> Refit
    // 2 -> RESERVE
    // 3 -> Broken
    // 4 -> Shattred
    public void calculateDivisionSate()
    {
        if (CombatValue > 0.6 && status != 2 && status != 1)
        {
            status = 0;
        }
        else if (CombatValue > 0.3 && status != 2 && status != 1)
        {
            status = 3;

        }
        else if (status != 2 && status != 1)
        {
            status = 4;

        }
    }
    [Header("Brigades")]
    //Brigades
    public Battalion[] battalions;

    //Battalion Reinforcements

    public void reinforceBattalions(int MPreinforcements, int AFVreinforcements, int ARTreinforcements)
    {
        for (int i = 0; i < battalions.Length; i++)
        {
            battalions[i].increaseAFVCount(AFVreinforcements);
            battalions[i].increaseARTCount(ARTreinforcements);
            battalions[i].increaseMPCount(MPreinforcements);
        }
        if (this.status == 2)
        {
            for (int i = 0; i < battalions.Length; i++)
            {
                battalions[i].increaseAFVCount(AFVreinforcements);
                battalions[i].increaseARTCount(ARTreinforcements);
                battalions[i].increaseMPCount(MPreinforcements);
            }
        }
        //Debug.Log("Reinforcements Completed");
    }

    //
    //RETURN STATEMENTS
    public int returnManpower()
    {
        int count = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            count += this.battalions[i].manpower;
        }
        return count;
    }
    public int returnTank()
    {
        int count = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            count += this.battalions[i].tankCount;
        }
        return count;
    }
    public int returnART()
    {
        int count = 0;
        for (int i = 0; i < this.battalions.Length; i++)
        {

            count += this.battalions[i].artileryCount;
        }
        return count;
    }


    [Header("UI")]
    public DivisionTileUI UiTileDivision;

    [Header("Division Material")]
    public Material[] material;
    Renderer rend;
    [Header("Selecter")]
    public Selecter selecter;
    // Start is called before the first frame update
    void Start()
    {
        this.MovementValue = BaseMovementValue;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

        calculateBaseArtileryCount();
        baseStartingManpower();
        baseStartingMoral();
        calculateBaseTankCount();

        updatePosition();

        CombatValue = BaseCombatValue;
        createStatusObject();
        //notHighlight();

    }
    private Vector3 velocity = Vector3.zero;

    public void updatePosition()
    {
        if (this.Hex != null)
        {

            Vector3 position = new Vector3(Hex.getPosition().x, Hex.getPosition().y + 0.05f + 0.05f, Hex.getPosition().z);
            this.transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, 1);

        }
    }

    // Update is called once per frame
    void Update()
    {

        calculatebaseEq();
        changeHexType();

        calculateManpower();
        calculateMoral();

        calculateTankCount();
        calculateArtileryCount();

        calculateCumulativeEquipment();
        calculateDivisionSate();
        updatePosition();
        calculateCombatValue();
        statusObjectCordinateUpdater();
        updateStatusMaterial();
        movementUpdater();
        if (Army != null)
        {
            bool isNear = isUnitNear(Army);

        }


    }

    string s;
    private void OnMouseOver()
    {
        renderLine();
        if (Input.GetMouseButtonDown(1) && selecter.defenderUnitAvaliable && selecter.unit != null)
        {
            selecter.defenderUnit = this;
        }
        //Debug.Log("COROLATION CHANGED");
        highlightGreen();
        createDivisionNameShower();
    }

    [Obsolete]
    private void OnMouseExit()
    {
        destroyDivisionName();
        notHighlight();
        destroyLine();
        armyOutlineBroker();
    }
    [Header("Click Sound")]
    public AudioSource sound;

    /* [Header("Voice Sound")]
     public AudioSource v1;
     public AudioSource v2;
     public AudioSource v3;
     public AudioSource v4;*/

    [Obsolete]
    void OnMouseDown()
    {

        //Debug.Log("COROLATION CHANGED");
        //highlightGreen();
        if (Input.GetMouseButtonDown(0))
        {
            /*int randomSounder = (int)UnityEngine.Random.Range(0, 3);
            if (randomSounder == 0)
            {
                v1.mute = true;
                v1.mute = false;
                Vector3 audioPosition1 = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
                v1.transform.position = audioPosition1;
                v1.Play();
                
            }
            else if (randomSounder == 1)
            {
                v2.mute = true;
                v2.mute = false;
                Vector3 audioPosition1 = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
                v2.transform.position = audioPosition1;
                v2.Play();
            }
            else if (randomSounder == 2)
            {
                v3.mute = true;
                v3.mute = false;
                Vector3 audioPosition1 = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
                v3.transform.position = audioPosition1;
                v3.Play();
            }
            else if (randomSounder == 3)
            {
                v4.mute = true;
                v4.mute = false;
                Vector3 audioPosition1 = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
                v4.transform.position = audioPosition1;
                v4.Play();
            }*/

            Vector3 audioPosition = new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
            sound.transform.position = audioPosition;
            sound.Play();


            if ((typeOfDivision == 0 && selecter.turnCounterBro.GermanReady == true) || (typeOfDivision == 1 && selecter.turnCounterBro.SovietReady == true))
            {
                this.selecter.unit = this;

                UiTileDivision.divImage.sprite = this.divImage;
                UiTileDivision.ArmyGeneral.text = this.armyHQ.General;
                UiTileDivision.DivisionCom.text = this.divisionComName;
                UiTileDivision.Equipment.text = new string((this.equipmentRatio() * 100).ToString().Take(4).ToArray()) + "%";
                UiTileDivision.AFV.text = "" + this.tankCount.ToString();
                UiTileDivision.Artilery.text = "" + this.artileryCount.ToString();
                UiTileDivision.DivisionName.text = divisionName;
                UiTileDivision.CV.text = "QUALITY " + new string((this.CombatValue).ToString().Take(4).ToArray());
                UiTileDivision.Manpower.text = "Manpower  " + cumManpower.ToString() + " / " + baseManpower.ToString();
                UiTileDivision.MoralValue.text = "" + cumMoral.ToString();
                UiTileDivision.AACount.text = (artileryCount / 4).ToString();
                UiTileDivision.ATCount.text = (artileryCount / 6).ToString();
                UiTileDivision.MovementValue.text = this.MovementValue.ToString();
                UiTileDivision.supportCompanyNames.text = "";
                if (this.returnSupportCompanies() != null)
                {
                    var xx = this.returnSupportCompanies();
                    for (int y = 0; y < xx.Length; y++)
                    {
                        UiTileDivision.supportCompanyNames.text += xx[y].Name + "\n";
                    }

                }



                Color32 cl;
                if (returnCV() > 1)
                {
                    cl = new Color32(64, 128, 192, 255);
                    UiTileDivision.CV.color = cl;
                }
                else if (returnCV() > 0.9)
                {
                    cl = new Color32(0, 200, 0, 255);
                    UiTileDivision.CV.color = cl;
                }
                else if (returnCV() > 0.6)
                {
                    cl = new Color32(255, 255, 0, 255);
                    UiTileDivision.CV.color = cl;
                }
                else if (returnCV() > 0.3)
                {
                    cl = new Color32(220, 51, 0, 255);
                    UiTileDivision.CV.color = cl;
                }
                else
                {
                    cl = new Color32(153, 0, 0, 255);
                    UiTileDivision.CV.color = cl;
                }





                for (int i = 0; i < this.battalions.Length; i++)
                {
                    s = s + battalions[i].Name + " - " + battalions[i].manpower.ToString() + "/" + battalions[i].tankCount + "/" + battalions[i].artileryCount + "\n";
                }
                if (battalions[0] != null)
                {
                    UiTileDivision.Brigades.text = s;
                }
                s = "";
                Renderer rend2;
                rend2 = GetComponent<Renderer>();
                rend2.enabled = true;
                rend2.sharedMaterial = material[0];

                //UiTileDivision.divisionImage.sprite =  rend2;
                string ss;
                //Status
                // 0 -> Active
                // 1 -> Refit
                // 2 -> Static
                // 3 -> Reserve
                // 4 -> Shattred
                Color32 color;
                UiTileDivision.ArmyHQ.text = this.armyHQ.Name;
                if (this.status == 0)
                {

                    ss = "ACTIVE";
                    color = new Color32(0, 200, 0, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 1)
                {
                    ss = "FORMING";
                    color = new Color32(0, 128, 200, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 2)
                {
                    ss = "RESERVE";
                    color = new Color32(200, 51, 200, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 3)
                {
                    ss = "BROKEN";
                    color = new Color32(200, 128, 0, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 4)
                {
                    ss = "SHATTERED";
                    color = new Color32(200, 75, 75, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }

            }
            else if ((typeOfDivision == 0 && selecter.turnCounterBro.GermanReady == false) || (typeOfDivision == 1 && selecter.turnCounterBro.SovietReady == false))
            {
                UiTileDivision.divImage.sprite = this.divImage;
                UiTileDivision.ArmyGeneral.text = this.armyHQ.General;
                UiTileDivision.DivisionCom.text = this.divisionComName;
                UiTileDivision.Equipment.text = "%?";
                UiTileDivision.AFV.text = "" + "?";
                UiTileDivision.Artilery.text = "" + "?";
                UiTileDivision.DivisionName.text = divisionName;

                UiTileDivision.Manpower.text = "Manpower  " + cumManpower.ToString() + "";
                UiTileDivision.MoralValue.text = "" + cumMoral.ToString();
                UiTileDivision.AACount.text = ("?");
                UiTileDivision.ATCount.text = ("?");

                UiTileDivision.ArmyHQ.text = this.armyHQ.Name;
                UiTileDivision.supportCompanyNames.text = "";
                if (this.returnSupportCompanies() != null)
                {
                    var xx = this.returnSupportCompanies();
                    for (int y = 0; y < xx.Length; y++)
                    {
                        UiTileDivision.supportCompanyNames.text += xx[y].Name + "\n";
                    }

                }



                int randomizedValue = (int)UnityEngine.Random.Range(1, 3);
                if (this.MovementValue - randomizedValue < 0)
                {
                    UiTileDivision.MovementValue.text = "0" + "-" + (this.MovementValue + randomizedValue).ToString();
                }
                else
                {
                    UiTileDivision.MovementValue.text = (this.MovementValue - randomizedValue).ToString() + "-" + (this.MovementValue + randomizedValue).ToString();
                }

                for (int i = 0; i < this.battalions.Length; i++)
                {
                    s = s + battalions[i].Name + "\n";
                }
                if (battalions[0] != null)
                {
                    UiTileDivision.Brigades.text = s;
                }
                s = "";

                Color32 cl;
                if (returnCV() > 1)
                {
                    cl = new Color32(64, 128, 192, 255);
                    UiTileDivision.CV.color = cl;
                    UiTileDivision.CV.text = "VERY GOOD";
                }
                else if (returnCV() > 0.9)
                {
                    cl = new Color32(0, 200, 0, 255);
                    UiTileDivision.CV.color = cl;
                    UiTileDivision.CV.text = "GOOD";
                }
                else if (returnCV() > 0.6)
                {
                    cl = new Color32(255, 255, 0, 255);
                    UiTileDivision.CV.color = cl;
                    UiTileDivision.CV.text = "WEARY";
                }
                else if (returnCV() > 0.3)
                {
                    cl = new Color32(220, 51, 0, 255);
                    UiTileDivision.CV.color = cl;
                    UiTileDivision.CV.text = "WINDED";
                }
                else
                {
                    cl = new Color32(153, 0, 0, 255);
                    UiTileDivision.CV.color = cl;
                    UiTileDivision.CV.text = "FLEETING";
                }
                string ss = "";
                Color32 color = new Color32();
                if (this.status == 0)
                {

                    ss = "ACTIVE";
                    color = new Color32(0, 200, 0, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 1)
                {
                    ss = "FORMING";
                    color = new Color32(0, 128, 200, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 2)
                {
                    ss = "RESERVE";
                    color = new Color32(200, 51, 200, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 3)
                {
                    ss = "BROKEN";
                    color = new Color32(200, 128, 0, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
                else if (status == 4)
                {
                    ss = "SHATTERED";
                    color = new Color32(200, 75, 75, 255);
                    UiTileDivision.status.text = ss;
                    UiTileDivision.status.color = color;
                }
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            selecter.defenderUnit = this;
        }


    }


    [Header("Status Effect Object")]
    public GameObject statusEffectObject;
    [Header("Status Materials->Active Refic Reserve Broken Shattered")]
    public Material[] statusMaterials;
    Renderer statusRenderer;

    GameObject statusObject;

    void createStatusObject()
    {

        statusObject = Instantiate(statusEffectObject, transform.position, transform.rotation);
        //statusObject.AddComponent<Halo>(); ;
    }
    void updateStatusMaterial()
    {
        statusRenderer = statusEffectObject.GetComponent<Renderer>();
        statusRenderer.enabled = true;
        // 0 -> Active
        // 1 -> Refit
        // 2 -> Static
        // 3 -> Reserve
        // 4 -> Shattred
        if (this.status == 0)
        {
            statusObject.GetComponent<MeshRenderer>().material = statusMaterials[0];
        }
        else if (this.status == 1)
        {
            statusObject.GetComponent<MeshRenderer>().material = statusMaterials[1];
        }
        else if (this.status == 2)
        {
            statusObject.GetComponent<MeshRenderer>().material = statusMaterials[2];
        }
        else if (this.status == 3)
        {
            statusObject.GetComponent<MeshRenderer>().material = statusMaterials[3];
        }
        else if (this.status == 4)
        {
            statusObject.GetComponent<MeshRenderer>().material = statusMaterials[4];
        }

    }

    void statusObjectCordinateUpdater()
    {
        Vector3 statusPlaceVector = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 0.1f, this.transform.position.z + 0.3f);
        statusObject.transform.position = statusPlaceVector;
    }
    [Header("Show Name on over UI (Division Updater)")]
    public showDivNameOnMause showNameAll;
    void createDivisionNameShower()
    {

        showNameAll.nameTextField.SetActive(true);
        //showNameAll.nameCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        showNameAll.nameText.text = this.divisionName;
        Vector3 calculatorVector = new Vector3(this.transform.position.x + 1.2f, this.transform.position.y + 0.3f, this.transform.position.z + 0.5f);
        showNameAll.nameText.transform.position = calculatorVector;
        if (styleofDivision == 0)
        {
            showNameAll.divisionTextXX.transform.position = new Vector3(this.transform.position.x - 0.8f, this.transform.position.y + 0.2f, this.transform.position.z + 0.3f);
        }

    }
    void destroyDivisionName()
    {

        showNameAll.nameTextField.SetActive(false);
        //showNameAll.nameCanvas.renderMode = RenderMode.WorldSpace;
        showNameAll.nameText.text = "";
        showNameAll.divisionTextXX.transform.position = new Vector3(this.transform.position.x - 10000f, this.transform.position.y + 10000f, this.transform.position.z + 10000f);
    }

    //is Destroyed
    [NonSerialized]
    public bool isDestroyed = false;

    [Header("Division Destroyed Object")]
    public GameObject destroyedObject;
    GameObject destroyedObjectHolder;
    Renderer DeadObjectRend;
    public void createDestroyedObject()
    {
        Vector3 calculatorVector2 = new Vector3(this.transform.position.x, this.transform.position.y + 0.02f, this.transform.position.z);

        float rotatedz = (float)UnityEngine.Random.Range(-50f, 50f);
        Vector3 calculatorVector3 = new Vector3(this.transform.rotation.x, this.transform.rotation.y + rotatedz, this.transform.rotation.z);
        destroyedObjectHolder = Instantiate(destroyedObject, calculatorVector2, transform.rotation);
        destroyedObjectHolder.transform.Rotate(calculatorVector3, Space.World);
        Renderer rend2;
        rend2 = destroyedObjectHolder.GetComponent<Renderer>();
        rend2.enabled = true;
        rend2.sharedMaterial = material[0];

        //DeadObjectRend = destroyedObjectHolder.GetComponent<Renderer>();
        //DeadObjectRend.enabled = true;
        //destroyedObject.GetComponent<MeshRenderer>().material = this.material[0];
    }
    public void fixUnit(Unit unit)
    {
        for (int i = 0; i < unit.battalions.Length; i++)
        {
            if (unit.battalions[i].manpower < 0)
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
    private Outline outliner;
    void highlightRed()
    {
        outliner = this.GetComponent<Outline>();
        outliner.OutlineMode = Outline.Mode.OutlineAll;
        outliner.OutlineColor = Color.red;
    }
    void highlightGreen()
    {
        outliner = this.GetComponent<Outline>();
        outliner.OutlineMode = Outline.Mode.OutlineAll;
        outliner.OutlineColor = Color.green;
    }
    void highlightBlue()
    {
        outliner = this.GetComponent<Outline>();
        outliner.OutlineMode = Outline.Mode.OutlineAll;
        outliner.OutlineColor = Color.blue;
    }
    void notHighlight()
    {
        outliner = this.GetComponent<Outline>();
        outliner.OutlineMode = Outline.Mode.OutlineHidden;

    }
    [SerializeField]
    [Header("Line Renderer")]
    LineRenderer x;

    [Obsolete]
    void renderLine()
    {
        if (this.Army != null)
        {
            if (isUnitNear(this.Army) == true)
            {
                float distanceBetweenPoints = Vector2.Distance(new Vector2(), new Vector2());
                x.positionCount = 4;
                x.SetPosition(0, this.transform.position);
                x.SetPosition(1, this.transform.position + new Vector3(0, 0.3f, 0));
                x.SetPosition(2, Army.transform.position + new Vector3(0, 0.3f, 0));
                x.SetPosition(3, Army.transform.position);
                //x.sharedMaterial.SetColor("_Color", Color.blue);
                x.SetColors(Color.cyan, Color.cyan);
            }
            else
            {
                x.positionCount = 4;
                x.SetPosition(0, this.transform.position);
                x.SetPosition(1, this.transform.position + new Vector3(0, 0.3f, 0));
                x.SetPosition(2, Army.transform.position + new Vector3(0, 0.3f, 0));
                x.SetPosition(3, Army.transform.position);
                x.SetColors(Color.magenta, Color.magenta);

            }
        }
        else if (this.styleofDivision == 1 && this.attachedDivisions != null)
        {
            for (int i = 0; i < attachedDivisions.Length; i++)
            {
                if (attachedDivisions[i].isUnitNear(attachedDivisions[i].Army))
                {
                    attachedDivisions[i].outliner = this.GetComponent<Outline>();
                    attachedDivisions[i].outliner.OutlineMode = Outline.Mode.OutlineAll;
                    attachedDivisions[i].outliner.OutlineColor = Color.green;
                }
                else
                {
                    x.positionCount = 4;
                    x.SetPosition(0, this.transform.position);
                    x.SetPosition(1, this.transform.position + new Vector3(0, 0.3f, 0));
                    x.SetPosition(2, attachedDivisions[i].transform.position + new Vector3(0, 0.3f, 0));
                    x.SetPosition(3, attachedDivisions[i].transform.position);
                    x.SetColors(Color.magenta, Color.magenta);
                }
            }
        }

    }
    void armyOutlineBroker()
    {
        if (this.styleofDivision == 1 && this.attachedDivisions != null)
        {
            for (int i = 0; i < attachedDivisions.Length; i++)
            {

                attachedDivisions[i].outliner = this.GetComponent<Outline>();
                attachedDivisions[i].outliner.OutlineMode = Outline.Mode.OutlineHidden;

            }
        }
    }

    [Obsolete]
    void destroyLine()
    {
        //Debug.Log("Hello World");
        Color transparent = new Color(0, 0, 0, 0);

        x.SetColors(transparent, transparent);
    }
    public bool isUnitNear(Unit unit)
    {
        if (Hex != null)
        {
            for (int i = 0; i < this.Hex.neighboors.Length; i++)
            {
                if (this.Hex.neighboors[i].Unit == unit)
                {
                    //Debug.Log("true 1");
                    return true;
                }
                else
                {
                    for (int ii = 0; ii < this.Hex.neighboors[i].neighboors.Length; ii++)
                    {
                        if (this.Hex.neighboors[i].neighboors[ii].Unit == unit)
                        {
                            //Debug.Log("true 2");
                            return true;
                        }
                        else
                        {
                            for (int iii = 0; iii < this.Hex.neighboors[i].neighboors[ii].neighboors.Length; iii++)
                            {
                                if (this.Hex.neighboors[i].neighboors[ii].neighboors[iii].Unit == unit)
                                {
                                    //Debug.Log("true 3");
                                    return true;
                                }

                            }
                        }
                    }

                }
            }
        }

        // Debug.Log("false");
        return false;
    }

    public bool isReadyToAttackSovietAI()
    {
        double[] holder = { 0, 0 };
        holder[0] = look_my_sourounding(this)[0];
        holder[1] = look_my_sourounding(this)[1];

        if (holder[0] * 5 > holder[1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public double[] look_my_sourounding(Unit unit)
    {
        double[] axixdividedsoviet = { 0, 0 };

        double str_ger = 0;
        double str_sov = 0;

        if (Hex != null)
        {
            for (int i = 0; i < this.Hex.neighboors.Length; i++)
            {
                if (this.Hex.neighboors[i].Unit == unit)
                {
                    if (this.Hex.neighboors[i].Unit.typeOfDivision == 0)
                    {
                        str_ger += this.Hex.neighboors[i].Unit.cumEquipment * 50 + this.Hex.neighboors[i].Unit.cumManpower + this.Hex.neighboors[i].Unit.cumMoral * 100;
                    }
                    if (this.Hex.neighboors[i].Unit.typeOfDivision == 1)
                    {
                        str_sov += this.Hex.neighboors[i].Unit.cumEquipment * 50 + this.Hex.neighboors[i].Unit.cumManpower + this.Hex.neighboors[i].Unit.cumMoral * 100;

                    }
                }
                else
                {
                    for (int ii = 0; ii < this.Hex.neighboors[i].neighboors.Length; ii++)
                    {
                        if (this.Hex.neighboors[i].neighboors[ii].Unit == unit)
                        {
                            if (this.Hex.neighboors[i].neighboors[ii].Unit.typeOfDivision == 0)
                            {
                                str_ger += this.Hex.neighboors[i].neighboors[ii].Unit.cumEquipment * 50 + this.Hex.neighboors[i].neighboors[ii].Unit.cumManpower + this.Hex.neighboors[i].neighboors[ii].Unit.cumMoral * 100;
                            }
                            if (this.Hex.neighboors[i].neighboors[ii].Unit.typeOfDivision == 1)
                            {
                                str_sov += this.Hex.neighboors[i].neighboors[ii].Unit.cumEquipment * 50 + this.Hex.neighboors[i].neighboors[ii].Unit.cumManpower + this.Hex.neighboors[i].neighboors[ii].Unit.cumMoral * 100;
                            }
                        }
                        else
                        {
                            for (int iii = 0; iii < this.Hex.neighboors[i].neighboors[ii].neighboors.Length; iii++)
                            {
                                if (this.Hex.neighboors[i].neighboors[ii].neighboors[iii].Unit == unit)
                                {
                                    //Debug.Log("true 3");
                                }

                            }
                        }
                    }

                }
            }
        }

        axixdividedsoviet[1] = str_ger;
        axixdividedsoviet[0] = str_sov;

        return axixdividedsoviet;

    }


    public void changeHexType()
    {
        if(this.Hex != null && this != null)
        {
            if (this.typeOfDivision == 0)
            {
                this.Hex.HexType = 1;
            }
            else
            {
                this.Hex.HexType = 0;
            }
        }
    }
}
