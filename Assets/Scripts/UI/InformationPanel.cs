using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    [SerializeField]
    public multi_handler multihandle;
    [SerializeField]
    public turnCounter tC;

    public Button armybutton;
    public Button industrybutton;
    public Button logisticsbutton;

    public Canvas armycanvas;
    public Canvas industrycanvas;
    public Canvas logisticscanvas;

    public Button EXITarmy1;
    public Button EXITindustry2;
    public Button EXITlogistics3;

    public TextMeshProUGUI army;
    public TextMeshProUGUI production;
    public TextMeshProUGUI logistics;
    void Start()
    {
        armybutton.onClick.AddListener(ArmyButtonAction);
        industrybutton.onClick.AddListener(IndustryButtonAction);
        logisticsbutton.onClick.AddListener(LogisticsButtonAction);

        EXITarmy1.onClick.AddListener(exitbutton1);
        EXITindustry2.onClick.AddListener(exitbutton2);
        EXITlogistics3.onClick.AddListener(exitbutton3);
    }
    //biggerWiewButton.onClick.AddListener(delegate () { biggerWiew.renderMode = RenderMode.ScreenSpaceCamera; });
    //exitbutton.onClick.AddListener(delegate () { smallWiew.renderMode = RenderMode.WorldSpace; });
    void ArmyButtonAction()
    {
        armycanvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (tC.GermanReady == true)
        {
            string txt = "";
            
            for(int i = 0; i < multihandle.Axis.divisions.Length; i++)
            {
                if(multihandle.Axis.divisions[i].styleofDivision == 1)
                {
                    txt += multihandle.Axis.divisions[i].divisionName + "\n";
                    var attacheddiv = multihandle.Axis.divisions[i].returnAttachedDivisions();
                    for (int t = 0; t < multihandle.Axis.divisions[i].returnAttachedDivisions().Length; t++)
                    {
                        txt += "   >" + attacheddiv[t].divisionName + " | Status: " + calculateStatus(attacheddiv[t].status) + " | EQ: "+ (int)(attacheddiv[t].equipmentRatio()*100) +  "% \n" ;
                    }
                    var spcompanies = multihandle.Axis.divisions[i].returnSupportCompanies();
                    for (int z = 0; z < multihandle.Axis.divisions[i].returnSupportCompanies().Length; z++)
                    {
                        txt += "      -" + spcompanies[z].Name + "\n";
                    }
                }
                
            }
            
            army.text = txt;
        }
        if (tC.SovietReady == true)
        {
            string txt = "";

            for (int i = 0; i < multihandle.Soviet.divisions.Length; i++)
            {
                if (multihandle.Soviet.divisions[i].styleofDivision == 1)
                {
                    txt += multihandle.Soviet.divisions[i].divisionName + "\n";
                    var attacheddiv = multihandle.Soviet.divisions[i].returnAttachedDivisions();
                    for (int t = 0; t < multihandle.Soviet.divisions[i].returnAttachedDivisions().Length; t++)
                    {
                        txt += "   >" + attacheddiv[t].divisionName + " -> " + calculateStatus(attacheddiv[t].status) + "\n";
                    }
                    var spcompanies = multihandle.Soviet.divisions[i].returnSupportCompanies();
                    for (int z = 0; z < multihandle.Soviet.divisions[i].returnSupportCompanies().Length; z++)
                    {
                        txt += "      -" + spcompanies[z].Name + "\n";
                    }
                }

            }

            army.text = txt;
        }
    }
    string calculateStatus(int i)
    {
        if (i == 0)
        {
            return "R";
        }
        else if (i == 1)
        {
            return "F";
        }
        else if (i == 2)
        {
            return "R";
        }
        else if (i == 3)
        {
            return "B";
        }
        else
        {
            return "S";
        }
    }

    void IndustryButtonAction()
    {
        industrycanvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (tC.GermanReady == true)
        {
            string txt = "";
            var divisions = multihandle.Axis.divisions;
            var mpreinforcements = multihandle.AxisMPSupply;
            var artreinforcements = multihandle.AxisARTSupply;
            var tankreinforcements = multihandle.AxisTankSupply;
            var airplanereinforcements = multihandle.AxisAirPlaneSupply;
            var mobilization = multihandle.AxisMobilization;
            var frontcapacity = multihandle.AxisFrontCapacity;

            var randomizedmpvalue = Random.Range(-100, 100);
            var randomizedartvalue1 = Random.Range(-10, 10);
            var randomizedartvalue2 = Random.Range(-10, 10);
            var randomizedartvalue3 = Random.Range(-10, 10);

            var randomairplane = Random.Range(-3, 3);
            txt += "Industry" + "\n";
            txt += "> Our motherland provides us with " + (mpreinforcements*1000 + randomizedmpvalue) + " manpower each week" + ".\n";
            txt += "> Our industry produces " + (int)(((double)artreinforcements) * 100 + randomizedartvalue1) + " heavy artilery pieces" + ".\n";
            txt += "> Our industry produces " + (int)(((double)artreinforcements/6) * 100 + randomizedartvalue2)+ " anti-tank gun pieces" + ".\n";
            txt += "> Our industry produces " + (int)(((double)artreinforcements /7) * 100 + randomizedartvalue3 )+ " anti-air gun pieces" + ".\n";
            txt += "> Our industry produces " + (int)((double)tankreinforcements) * 10 + " tank pieces" + ".\n";
            txt += "> Our industry produces " + (int)(((double)airplanereinforcements)+ randomairplane) + " airplane pieces" + ".\n\n";

            txt += "Front Coverage" + "\n";
            txt += "> Becouse of the war, our front rechieves "+ frontcapacity + "% of the production" + ".\n\n";

            txt += "Army Income" + "\n";
            var artcount1 = artreinforcements * ((double)frontcapacity / 100);
            var manpowerincrease = ((int)((double)mpreinforcements * ((double)frontcapacity / 100)));
            if(manpowerincrease <= 0)
            {
                manpowerincrease = 1;
            }
            var heavyartincrease = (int)((double)artreinforcements * ((double)frontcapacity / 100));
            if (heavyartincrease <= 0)
            {
                heavyartincrease = 1;
            }
            var atincrease = ((int)((double)artcount1 / 6));
            var aaincrease = ((int)((double)artcount1 / 7));
            var tankincrease = ((int)((double)tankreinforcements * ((double)frontcapacity / 100)));
            if (atincrease <= 0)
            {
                atincrease = 1;
            }
            if (aaincrease <= 0)
            {
                aaincrease = 1;
            }
            if (tankincrease <= 0)
            {
                tankincrease = 1;
            }
            var airplaneincrease = airplanereinforcements * ((double)frontcapacity / 100);

            randomizedmpvalue = Random.Range(-1000, 1000);
            randomizedartvalue1 = Random.Range(-10, 10);
            randomizedartvalue2 = Random.Range(-10, 10);
            randomizedartvalue3 = Random.Range(-10, 10);

            txt += ">> Army's weekly Manpower Increase is " + (manpowerincrease*1000 + randomizedmpvalue) +  ". \n";
            txt += ">> Army's weekly Heavy Artilery Increase is " + (heavyartincrease * 100 + randomizedartvalue1) + ". \n";
            txt += ">> Army's weekly Anti-Tank Gun Increase is " + (atincrease*100 + randomizedartvalue2) + ". \n";
            txt += ">> Army's weekly Anti-Air Gun Increase is " + (aaincrease * 100 + randomizedartvalue3) + ". \n";
            txt += ">> Army's weekly Tank Increase per division is " + (tankincrease*10) + ". \n";
            txt += ">> Army's weekly Airplane Increase per air squadron is " + (airplaneincrease) + ". \n\n";

            txt += "Mobilization" + "\n";
            txt += "> Our country mobilized by " + mobilization + "% \n";

            production.text = txt;
        }
        if (tC.SovietReady == true)
        {
            string txt = "";
            var divisions = multihandle.Soviet.divisions;
            var mpreinforcements = multihandle.SovietMPSupply;
            var artreinforcements = multihandle.SovietARTSupply;
            var tankreinforcements = multihandle.SovietTankSupply;
            var airplanereinforcements = multihandle.SovietAirPlaneSupply;
            var mobilization = multihandle.SovietMobilization;
            var frontcapacity = multihandle.SovietFrontCapacity;

            var randomizedmpvalue = Random.Range(-100, 100);
            var randomizedartvalue1 = Random.Range(-10, 10);
            var randomizedartvalue2 = Random.Range(-10, 10);
            var randomizedartvalue3 = Random.Range(-10, 10);

            var randomairplane = Random.Range(-3, 3);
            txt += "Industry" + "\n";
            txt += "> Our motherland provides us with " + (mpreinforcements * 1000 + randomizedmpvalue) + " manpower each week" + ".\n";
            txt += "> Our industry produces " + (int)(((double)artreinforcements) * 100 + randomizedartvalue1) + " heavy artilery pieces" + ".\n";
            txt += "> Our industry produces " + (int)(((double)artreinforcements / 6) * 100 + randomizedartvalue2) + " anti-tank gun pieces" + ".\n";
            txt += "> Our industry produces " + (int)(((double)artreinforcements / 7) * 100 + randomizedartvalue3) + " anti-air gun pieces" + ".\n";
            txt += "> Our industry produces " + (int)((double)tankreinforcements) * 10 + " tank pieces" + ".\n";
            txt += "> Our industry produces " + (int)(((double)airplanereinforcements) + randomairplane) + " airplane pieces" + ".\n\n";

            txt += "Front Coverage" + "\n";
            txt += "> Becouse of the war, our front rechieves " + frontcapacity + "% of the production" + ".\n\n";

            txt += "Army Income" + "\n";
            var artcount1 = artreinforcements * ((double)frontcapacity / 100);
            var manpowerincrease = ((int)((double)mpreinforcements * ((double)frontcapacity / 100)));
            if (manpowerincrease <= 0)
            {
                manpowerincrease = 1;
            }
            var heavyartincrease = (int)((double)artreinforcements * ((double)frontcapacity / 100));
            if (heavyartincrease <= 0)
            {
                heavyartincrease = 1;
            }
            var atincrease = ((int)((double)artcount1 / 6));
            var aaincrease = ((int)((double)artcount1 / 7));
            var tankincrease = ((int)((double)tankreinforcements * ((double)frontcapacity / 100)));
            if (atincrease <= 0)
            {
                atincrease = 1;
            }
            if (aaincrease <= 0)
            {
                aaincrease = 1;
            }
            if (tankincrease <= 0)
            {
                tankincrease = 1;
            }
            var airplaneincrease = airplanereinforcements * ((double)frontcapacity / 100);

            randomizedmpvalue = Random.Range(-1000, 1000);
            randomizedartvalue1 = Random.Range(-10, 10);
            randomizedartvalue2 = Random.Range(-10, 10);
            randomizedartvalue3 = Random.Range(-10, 10);

            txt += ">> Army's weekly Manpower Increase is " + (manpowerincrease * 1000 + randomizedmpvalue) + ". \n";
            txt += ">> Army's weekly Heavy Artilery Increase is " + (heavyartincrease * 100 + randomizedartvalue1) + ". \n";
            txt += ">> Army's weekly Anti-Tank Gun Increase is " + (atincrease * 100 + randomizedartvalue2) + ". \n";
            txt += ">> Army's weekly Anti-Air Gun Increase is " + (aaincrease * 100 + randomizedartvalue3) + ". \n";
            txt += ">> Army's weekly Tank Increase per division is " + (tankincrease * 10) + ". \n";
            txt += ">> Army's weekly Airplane Increase per air squadron is " + (airplaneincrease) + ". \n\n";

            txt += "Mobilization" + "\n";
            txt += "> Our country mobilized by " + mobilization + "% \n";

            production.text = txt;
        }
    }
    void LogisticsButtonAction()
    {
        logisticscanvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (tC.GermanReady == true)
        {
            string txt = "";


            logistics.text = txt;
        }
        if (tC.SovietReady == true)
        {
            string txt = "";
        }
    }

    void exitbutton1()
    {
        armycanvas.renderMode = RenderMode.WorldSpace;
    }
    void exitbutton2()
    {
        industrycanvas.renderMode = RenderMode.WorldSpace;
    }
    void exitbutton3()
    {
        logisticscanvas.renderMode = RenderMode.WorldSpace;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
