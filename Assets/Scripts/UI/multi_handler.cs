using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multi_handler : MonoBehaviour
{
    public turnCounter turnCounter;

    public ArmyHQ Axis;
    public ArmyHQ Soviet;

    public Selecter selecter;

    public Light lligth;

    
    //SUPPLYS
    [NonSerialized]
    public int AxisMobilization = 50;
    [NonSerialized]
    public int SovietMobilization = 80;
    [NonSerialized]
    public int AxisFrontCapacity = 40;
    [NonSerialized]
    public int SovietFrontCapacity = 40;
    
    //MANPOWER
    [NonSerialized]
    public int AxisMPSupply = 100;
    [NonSerialized]
    public int SovietMPSupply = 1500;
    //TANK
    [NonSerialized]
    public int AxisTankSupply = 3;
    [NonSerialized]
    public int SovietTankSupply = 2;
    //ARTILERY
    [NonSerialized]
    public int AxisARTSupply = 10;
    [NonSerialized]
    public int SovietARTSupply = 9;
    //AIRPLANE
    [NonSerialized]
    public int AxisAirPlaneSupply = 10;
    public int SovietAirPlaneSupply = 10;


    //TURN COUNT = REAL TURN COUNT
    [NonSerialized]
    public int turnCount;
    [NonSerialized]
    public int turnCountMinusOne;


    //WEATHER AND SEAZONS
    //
    // SEAZONS 
    //  0 = WINTER   1 = SPRING      2 = SUMMER    3 = FALL
    // WEATHER
    // 0 = CLEAR    1 = MUD     2 = SNOW     3 = BLIZZARD
    // SEAZONS AND WEATHER CHANGE
    //  WINTER = %60 BLIZZARD %30 SNOW %5 MUD %5 CLEAR
    //  SPRING = %90 CLEAR %10 MUD
    //  SUMMER = %100 CLEAR
    //  FALL = %5 CLEAR %65 MUD %20 SNOW %10BLIZZARD
    //
    public weatherTimePanel weathertimePanelC;
    [NonSerialized]
    int year = 1941;
    [NonSerialized]
    int month = 6;
    [NonSerialized]
    int day = 22;

    public void calculateTime()
    {
        /*day += (turnCounter.BaseTurn * 7) / ((turnCounter.BaseTurn * 7) - 30);
        if(turnCounter.BaseTurn < 52)
        {
            month = turnCounter.BaseTurn / 4;
        }
        else if(turnCounter.BaseTurn < 104)
        {
            
            month = (turnCounter.BaseTurn / 4) - 12;
        }

        int CounterYear = turnCounter.BaseTurn / (52);

        year = year + CounterYear;


        weathertimePanelC.DateText.text = day.ToString() + "." + month.ToString() + "." + year.ToString();*/


        if (turnCounter.BaseTurn == 2)
        {
            day = 29;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for(int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            SovietMPSupply += 500;
        }
        //Month 7
        else if (turnCounter.BaseTurn == 3) {
            day = 5;
            month = 7;
            SovietMPSupply += 500;
        }
        else if (turnCounter.BaseTurn == 4)
        {
            day = 11;
            SovietMPSupply += 500;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                if (Soviet.divisions[i].phase == 1)
                {
                    Soviet.divisions[i].status = 0;
                }
            }
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
        }
        else if (turnCounter.BaseTurn == 5)
        {
            day = 19;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            SovietMPSupply += 500;
        }
        else if (turnCounter.BaseTurn == 6)
        {
            day = 28;
            SovietMPSupply += 500;
        }
        //Month 8
        else if (turnCounter.BaseTurn == 7)
        {
            day = 4;
            month = 8;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                if (Soviet.divisions[i].phase == 2)
                {
                    Soviet.divisions[i].status = 0;
                }
            }
        }
        else if (turnCounter.BaseTurn == 8)
        {
            day = 9;
        }
        else if (turnCounter.BaseTurn == 9)
        {
            day = 17;
            SovietMPSupply += 500;
        }
        else if (turnCounter.BaseTurn == 10)
        {
            day = 25;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            SovietMPSupply += 500;
        }
        //Month 9
        else if (turnCounter.BaseTurn == 11)
        {
            day = 6;
            month = 9;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            SovietMPSupply += 500;
        }
        else if (turnCounter.BaseTurn == 12)
        {
            day = 18;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            SovietMPSupply += 500;
        }
        else if (turnCounter.BaseTurn == 13)
        {
            day = 24;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            SovietMPSupply += 500;
        }
        else if (turnCounter.BaseTurn == 14)
        {
            day = 29;
        }
        //Month 10
        else if (turnCounter.BaseTurn == 15)
        {
            day = 5;
            month = 10;
        }
        else if (turnCounter.BaseTurn == 16)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 17)
        {
            day = 19;
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {
                for (int x = 0; x < Soviet.divisions[i].battalions.Length; x++)
                {
                    Soviet.divisions[i].battalions[x].manpower += 1000;
                }
            }
            SovietMPSupply += 500;
        }
        else if (turnCounter.BaseTurn == 18)
        {
            day = 28;
        }
        //Month 11
        else if (turnCounter.BaseTurn == 19)
        {
            day = 5;
            month = 11;
        }
        else if (turnCounter.BaseTurn == 20)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 21)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 22)
        {
            day = 28;
        }
        //Month 12
        else if (turnCounter.BaseTurn == 23)
        {
            day = 5;
            month = 12;
        }
        else if (turnCounter.BaseTurn == 24)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 25)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 26)
        {
            day = 28;
        }




        //1942
        //Month 1else if (turnCounter.BaseTurn == 7)
        else if (turnCounter.BaseTurn == 27)
        {
            day = 5;
            month = 1;
        }
        else if (turnCounter.BaseTurn == 28)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 29)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 30)
        {
            day = 28;
        }
        //Month 2
        else if (turnCounter.BaseTurn == 31)
        {
            day = 5;
            month = 2;
        }
        else if (turnCounter.BaseTurn == 32)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 33)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 34)
        {
            day = 28;
        }
        //Month 3
        else if (turnCounter.BaseTurn == 35)
        {
            day = 5;
            month = 3;
        }
        else if (turnCounter.BaseTurn == 36)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 37)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 38)
        {
            day = 28;
        }
        //Month 4
        else if (turnCounter.BaseTurn == 39)
        {
            day = 5;
            month = 4;
        }
        else if (turnCounter.BaseTurn == 40)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 41)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 42)
        {
            day = 28;
        }
        //Month 5
        else if (turnCounter.BaseTurn == 43)
        {
            day = 5;
            month = 5;
        }
        else if (turnCounter.BaseTurn == 44)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 45)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 46)
        {
            day = 28;
        }
        //Month 6
        else if (turnCounter.BaseTurn == 47)
        {
            day = 5;
            month = 6;
        }
        else if (turnCounter.BaseTurn == 48)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 49)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 50)
        {
            day = 28;
        }
        //Month 7
        else if (turnCounter.BaseTurn == 51)
        {
            day = 5;
            month = 7;
        }
        else if (turnCounter.BaseTurn == 52)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 53)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 54)
        {
            day = 28;
        }
        //Month 8
        else if (turnCounter.BaseTurn == 55)
        {
            day = 5;
            month = 8;
        }
        else if (turnCounter.BaseTurn == 56)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 57)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 58)
        {
            day = 28;
        }
        //Month 9
        else if (turnCounter.BaseTurn == 59)
        {
            day = 5;
            month = 9;
        }
        else if (turnCounter.BaseTurn == 60)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 61)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 62)
        {
            day = 28;
        }
        //Month 10
        else if (turnCounter.BaseTurn == 63)
        {
            day = 5;
            month = 10;
        }
        else if (turnCounter.BaseTurn == 64)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 65)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 66)
        {
            day = 28;
        }
        //Month 11
        else if (turnCounter.BaseTurn == 67)
        {
            day = 5;
            month = 11;
        }
        else if (turnCounter.BaseTurn == 68)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 69)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 70)
        {
            day = 28;
        }
        //Month 12
        else if (turnCounter.BaseTurn == 71)
        {
            day = 5;
            month = 12;
        }
        else if (turnCounter.BaseTurn == 72)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 73)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 74)
        {
            day = 28;
        }
        //1943
        //Month 1
        else if (turnCounter.BaseTurn == 75)
        {
            year = 1943;
            day = 5;
            month = 1;
        }
        else if (turnCounter.BaseTurn == 76)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 77)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 78)
        {
            day = 28;
        }
        //Month 2
        else if (turnCounter.BaseTurn == 79)
        {
            day = 5;
            month = 2;
        }
        else if (turnCounter.BaseTurn == 80)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 81)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 82)
        {
            day = 28;
        }
        //Month 3
        else if (turnCounter.BaseTurn == 83)
        {
            day = 5;
            month = 3;
        }
        else if (turnCounter.BaseTurn == 84)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 85)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 86)
        {
            day = 28;
        }
        //Month 4
        else if (turnCounter.BaseTurn == 87)
        {
            day = 5;
            month = 4;
        }
        else if (turnCounter.BaseTurn == 88)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 89)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 90)
        {
            day = 28;
        }
        //Month 5
        else if (turnCounter.BaseTurn == 91)
        {
            day = 5;
            month = 5;
        }
        else if (turnCounter.BaseTurn == 92)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 93)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 94)
        {
            day = 28;
        }
        //Month 6
        else if (turnCounter.BaseTurn == 95)
        {
            day = 5;
            month = 6;
        }
        else if (turnCounter.BaseTurn == 96)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 97)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 98)
        {
            day = 28;
        }
        //Month 7
        else if (turnCounter.BaseTurn == 99)
        {
            day = 5;
            month = 7;
        }
        else if (turnCounter.BaseTurn == 100)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 101)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 102)
        {
            day = 28;
        }
        //Month 8
        else if (turnCounter.BaseTurn == 103)
        {
            day = 5;
            month = 8;
        }
        else if (turnCounter.BaseTurn == 104)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 105)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 106)
        {
            day = 28;
        }
        //Month 9
        else if (turnCounter.BaseTurn == 107)
        {
            day = 5;
            month = 9;
        }
        else if (turnCounter.BaseTurn == 108)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 109)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 110)
        {
            day = 28;
        }
        //Month 10
        else if (turnCounter.BaseTurn == 111)
        {
            day = 5;
            month = 10;
        }
        else if (turnCounter.BaseTurn == 112)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 113)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 114)
        {
            day = 28;
        }
        //Month 11
        else if (turnCounter.BaseTurn == 115)
        {
            day = 5;
            month = 11;
        }
        else if (turnCounter.BaseTurn == 116)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 117)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 118)
        {
            day = 28;
        }
        //Month 12
        else if (turnCounter.BaseTurn == 119)
        {
            day = 5;
            month = 12;
        }
        else if (turnCounter.BaseTurn == 120)
        {
            day = 11;
        }
        else if (turnCounter.BaseTurn == 121)
        {
            day = 19;
        }
        else if (turnCounter.BaseTurn == 122)
        {
            day = 28;
        }

        //1944
        //Month 1
        //Month 2
        //Month 3
        //Month 4
        //Month 5
        //Month 6
        //Month 7
        //Month 8
        //Month 9
        //Month 10
        //Month 11
        //Month 12

        weathertimePanelC.DateText.text = day.ToString() + "." + month.ToString() + "." + year.ToString();
    }
    [NonSerialized]
    public int seazon;
    // SEAZONS 
    //  0 = WINTER   1 = SPRING      2 = SUMMER    3 = FALL

    public void calculateSeazon()
    {
        if (month == 3 || month == 4 || month == 5)
        {
            //SPRING
            seazon = 1;
            weathertimePanelC.seazon.text = "Spring";
            weathertimePanelC.seazon.color = new Color32(0, 200, 100, 255);
        }
        if (month == 6 || month == 7 || month == 8)
        {
            //SUMMER
            seazon = 2;
            weathertimePanelC.seazon.text = "Summer";
            weathertimePanelC.seazon.color = new Color32(90, 200, 0, 255);
        }
        if (month == 9 || month == 10 || month == 11)
        {
            //AUTNUMM
            seazon = 3;
            weathertimePanelC.seazon.text = "Autnumn";
            weathertimePanelC.seazon.color = new Color32(200, 150, 0, 255);
        }
        if (month == 12 || month == 1 || month == 2)
        {
            //WINTER
            seazon = 0;
            weathertimePanelC.seazon.text = "Winter";
            weathertimePanelC.seazon.color = new Color32(100, 100, 100, 255);
        }
    }

    [NonSerialized]
    public int weather;
    // WEATHER
    // 0 = CLEAR    1 = MUD     2 = SNOW     3 = BLIZZARD
    [NonSerialized]
    public int degree;
    // SEAZONS AND WEATHER CHANGE
    //  WINTER = %60 BLIZZARD %30 SNOW %5 MUD %5 CLEAR
    //  SPRING = %90 CLEAR %10 MUD
    //  SUMMER = %100 CLEAR
    //  FALL = %5 CLEAR %80 MUD %15 SNOW 

    // SEAZONS 
    //  0 = WINTER   1 = SPRING      2 = SUMMER    3 = FALL
    //Degree
    //Summer = 10 - 30
    //Winter = -30 - 10
    //Fall = - 10 - 20
    //Spring = 5 - 30
    public void calculateWeather()
    {
        if (seazon == 0)
        {
            //WINTER
            //  WINTER = %60 BLIZZARD %30 SNOW %5 MUD %5 CLEAR

            int weatherCalculator = UnityEngine.Random.Range(0, 100);

            if (weatherCalculator > 0 && weatherCalculator < 60)
            {
                //BLIZZARD
                if (degree > -40)
                {
                    weather = 3;
                    degree -= UnityEngine.Random.Range(1, 4);
                    degree -= 4;
                    weathertimePanelC.DegreeText.text = degree.ToString() + "C";
                }
                if (degree < -40)
                {
                    degree += UnityEngine.Random.Range(1, 5);
                }
                else if (degree < -30)
                {
                    degree += UnityEngine.Random.Range(1, 3);
                }

            }
            else if (weatherCalculator > 60 && weatherCalculator < 90)
            {
                //SNOW
                if (degree > -40)
                {
                    weather = 2;
                    degree -= 2;
                    degree -= UnityEngine.Random.Range(1, 3);
                    weathertimePanelC.DegreeText.text = degree.ToString() + "C";

                }
                if (degree < -40)
                {
                    degree += UnityEngine.Random.Range(1, 5);
                }
                else if (degree < -30)
                {
                    degree += UnityEngine.Random.Range(1, 3);
                }

            }
            else if (weatherCalculator > 90 && weatherCalculator < 95)
            {
                //MUD
                if (weather > -40)
                {
                    weather = 1;
                    degree -= 3;
                    degree += UnityEngine.Random.Range(0, 4);
                    weathertimePanelC.DegreeText.text = degree.ToString() + "C";
                }
                weather += UnityEngine.Random.Range(1, 5);

            }
            else if (weatherCalculator > 95 && weatherCalculator < 100)
            {
                //CLEAR
                weather = 0;
                degree += 3;
                degree = UnityEngine.Random.Range(1, 3);
                weathertimePanelC.DegreeText.text = degree.ToString() + "C";
            }
        }
        else if (seazon == 1)
        {
            //SPRING
            //  SPRING = %90 CLEAR %10 MUD
            int weatherCalculator = UnityEngine.Random.Range(0, 100);

            if (weatherCalculator > 0 && weatherCalculator < 90)
            {
                //CLEAR
                weather = 0;
                //degree += 2;
                degree += UnityEngine.Random.Range(1, 3);
                if (weather < 5)
                {
                    degree += 3;
                    degree += UnityEngine.Random.Range(1, 4);
                }
                weathertimePanelC.DegreeText.text = degree.ToString() + "C";
                if (weather > 20)
                {
                    degree -= 3;
                }
            }
            else if (weatherCalculator > 90 && weatherCalculator < 100)
            {
                //MUD
                weather = 1;
                degree += 1;
                degree += UnityEngine.Random.Range(1, 5);

            }
            if (degree > 20)
            {
                degree = UnityEngine.Random.Range(7, 14);
                degree -= UnityEngine.Random.Range(1, 4);
            }
            weathertimePanelC.DegreeText.text = degree.ToString() + "C";
        }
        else if (seazon == 2)
        {
            //SUMMER
            //  SUMMER = %100 CLEAR
            weather = 0;
            if (degree > 30)
            {
                degree -= 3;
                degree -= UnityEngine.Random.Range(0, 2);
            } else if (degree > 10)
            {
                degree += UnityEngine.Random.Range(0, 4);
                weathertimePanelC.DegreeText.text = degree.ToString() + "C";
            }

            if (degree < 10)
            {
                degree = 10;
                degree += 3;
                degree += UnityEngine.Random.Range(0, 2);
            }

        }
        else if (seazon == 3)
        {
            //FALL

            //FALL = %5 CLEAR %80 MUD %15 SNOW 

            int weatherCalculator = UnityEngine.Random.Range(0, 100);


            if (weatherCalculator > 0 && weatherCalculator < 15)
            {
                //SNOW
                weather = 2;
                degree -= UnityEngine.Random.Range(1, 5);
                weathertimePanelC.DegreeText.text = degree.ToString() + "C";
            }
            else if (weatherCalculator > 15 && weatherCalculator < 95)
            {
                //MUD
                weather = 1;
                degree -= UnityEngine.Random.Range(1, 3);
                weathertimePanelC.DegreeText.text = degree.ToString() + "C";
            }
            else if (weatherCalculator > 95 && weatherCalculator < 100)
            {
                //CLEAR
                weather = 0;
                degree += UnityEngine.Random.Range(1, 3);
                weathertimePanelC.DegreeText.text = degree.ToString() + "C";
            }
        }
        if (weather == 0)
        {   // 0 = CLEAR    1 = MUD     2 = SNOW     3 = BLIZZARD
            weathertimePanelC.weatherImage.sprite = weathertimePanelC.sunny;
        }
        else if (weather == 1)
        {
            weathertimePanelC.weatherImage.sprite = weathertimePanelC.muddy;
        }
        else if (weather == 2)
        {
            weathertimePanelC.weatherImage.sprite = weathertimePanelC.snowy;
        }
        else if (weather == 3)
        {
            weathertimePanelC.weatherImage.sprite = weathertimePanelC.blizzard;
        }
    }







    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //




    // Start is called before the first frame update
    void Start()
    {

    }
    [SerializeField]
    GameObject Blizzard;
    [SerializeField]
    GameObject Snow;
    [SerializeField]
    GameObject Rain;
    // Update is called once per frame
    void Update()
    {

        turnCount = turnCounter.BaseTurn;
        if(turnCountMinusOne != turnCount)
        {
            //TIME
            calculateTime();
            //SEAZON ACCORDING TO MONTH
            calculateSeazon();
            //WEATHER AND DEGREE ACCORDING TO SEAZON
            calculateWeather();
            /////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////
            if (weather == 0)
            {
                lligth.color = new Color32(255, 244, 214, 50);

                Blizzard.transform.position = new Vector3(100, 2, 2);
                Snow.transform.position = new Vector3(100, 2, 2);
                Rain.transform.position = new Vector3(100, 2, 2);
            }
            else if (weather == 1)
            {
                lligth.color = new Color32(164, 117, 107, 80);

                Rain.transform.position = new Vector3(12, 10, 5);
                Blizzard.transform.position = new Vector3(100, 2, 2);
                Snow.transform.position = new Vector3(100, 2, 2);
            }
            else if (weather == 2)
            {
                lligth.color = new Color32(101, 136, 176, 50);

                Rain.transform.position = new Vector3(100, 2, 2);
                Snow.transform.position = new Vector3(12, 10, 5);
                Blizzard.transform.position = new Vector3(100, 2, 2);
            }
            else if (weather == 3)
            {
                lligth.color = new Color32(101, 136, 176, 100);
                Blizzard.transform.position = new Vector3(12, 10, 5);
                Snow.transform.position = new Vector3(100, 2, 2);
                Rain.transform.position = new Vector3(100, 2, 2);
            }


            //SUPPLY MECHANICS

            //SUPPLYS
            
            AxisMobilization = 50;
            
            SovietMobilization = 20;
            
            AxisFrontCapacity = 40;
            
            SovietFrontCapacity = 40;


            //MANPOWER
            
            AxisMPSupply = 100;
            
            SovietMPSupply = 70;
            //TANK
            
            AxisTankSupply = 3;
            
            SovietTankSupply = 2;

            //ARTILERY
            
            AxisARTSupply = 10;
            
            SovietARTSupply = 9;
            //AIRPLANE
            
            AxisAirPlaneSupply = 10;
            SovietAirPlaneSupply = 10;

             selecter.unit = null;
            selecter.hex = null;
            for (int i = 0; i < Axis.divisions.Length; i++)
            {
                
                if(Axis.divisions[i].status ==1)
                {
                    Axis.divisions[i].MovementValue += 1;
                    Axis.divisions[i].status = 0;
                }
                
                if(weather == 2 || weather == 3)
                {
                    Axis.divisions[i].MovementValue = 0;
                    Axis.divisions[i].CombatValue -= 0.2;
                    for(int xx = 0; xx < Axis.divisions[i].battalions.Length; xx++)
                    {
                        Axis.divisions[i].battalions[xx].manpower -= 1000;
                        if(Axis.divisions[i].battalions[xx].manpower < 0)
                        {
                            Axis.divisions[i].battalions[xx].manpower = 100;
                        }
                    }
                }
                Axis.divisions[i].reinforceBattalions(AxisMPSupply, AxisTankSupply , AxisARTSupply);
                Soviet.divisions[i].reinforceBattalions(SovietMPSupply, SovietTankSupply, SovietARTSupply);

            }
            for (int i = 0; i < Soviet.divisions.Length; i++)
            {

                if (Soviet.divisions[i].status == 1)
                {
                    Soviet.divisions[i].MovementValue += 1;
                    Soviet.divisions[i].status = 0;
                }
                    Soviet.divisions[i].reinforceBattalions(SovietMPSupply, SovietTankSupply, SovietARTSupply);
                    Soviet.divisions[i].CombatValue += 0.1;
                    Soviet.divisions[i].baseMoral += 1;
                    Soviet.divisions[i].cumMoral += 2;
                    for (int z = 0; z < Soviet.divisions[i].battalions.Length; z++)
                    {
                        Soviet.divisions[i].battalions[z].tankCount += 1;
                        Soviet.divisions[i].battalions[z].manpower += 50;
                        if (Soviet.divisions[i].battalions[z].baseManpower < Soviet.divisions[i].battalions[z].manpower)
                        {
                            Soviet.divisions[i].battalions[z].manpower += 500;
                        }
                        if (Soviet.divisions[i].battalions[z].moral < 80)
                        {
                            Soviet.divisions[i].battalions[z].moral += 2;
                        }
                        else if (Soviet.divisions[i].battalions[z].moral < 40)
                        {
                            Soviet.divisions[i].battalions[z].moral += 3;
                        }

                        Soviet.divisions[i].battalions[z].artileryCount += 2;
                        if (Soviet.divisions[i].battalions[z].baseArtilleryCount < Soviet.divisions[i].battalions[z].artileryCount)
                        {
                            Soviet.divisions[i].battalions[z].artileryCount += 5;
                        }
                    }

                //SUPPLY
                Soviet.divisions[i].reinforceBattalions(SovietMPSupply, SovietTankSupply, SovietARTSupply);
            }
            turnCountMinusOne += 1;
        }
    }
}
