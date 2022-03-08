using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
public class turnCounter : MonoBehaviour
{
    public int BaseTurn;
    
    public int SovietTurn;
    public int GermanTurn;
    public bool SovietReady;
    public bool GermanReady;
    public Text turnCount;
    public Text whoseTurn;

    public Text MPSupply;
    public Text TankSupply;
    public Text ARTSupply;
    [SerializeField]
    public GameObject NextTurnpanel;
    public GameObject DivisionInfopanel;

    public UnityEngine.UI.Button b;

    public multi_handler suplier;
    

    public int returnTurn()
    {
        return BaseTurn;
    }
    // Start is called before the first frame update
    void Start()
    {
        BaseTurn = 0;
        GermanReady = true;
        SovietReady = false;
        GermanTurn = 1;

        
        
        b.onClick.AddListener(delegate () { NextTurn(); });
        if (BaseTurn == 0)
        {
            NextTurnpanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(245, 245, 245, 235);
            DivisionInfopanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(245, 245, 245, 235);
        }
    }

    public void NextTurn()
    {
        if(GermanTurn > SovietTurn)
        {
            //GERMAN TURN
            SovietReady = false;
            GermanReady = true;
            SovietTurn += 1;
            BaseTurn += 1;

            MPSupply.text = suplier.AxisMPSupply.ToString() + "k";
            TankSupply.text = suplier.AxisTankSupply.ToString() + "0";
            ARTSupply.text = suplier.AxisARTSupply.ToString() + "0";

            NextTurnpanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(245, 245, 245, 240);
            DivisionInfopanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(245, 245, 245, 240);
        }
        else
        {
            //SOVIET TURN
            SovietReady = true;
            GermanReady = false;
            GermanTurn += 1;

            MPSupply.text = suplier.SovietMPSupply.ToString() + "k";
            TankSupply.text = suplier.SovietTankSupply.ToString() + "0";
            ARTSupply.text = suplier.SovietARTSupply.ToString() + "0";

            NextTurnpanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 235, 235, 240);
            DivisionInfopanel.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 235, 235, 240);
        }
    }

    // Update is called once per frame
    void Update()
    {
        turnCount.text = "Turn : " + BaseTurn.ToString();
        if (GermanReady)
        {
            whoseTurn.color = new Color32(0, 64, 255 , 255);
            whoseTurn.text = "AXIS";
        }
        else
        {
            whoseTurn.color = new Color32(230, 25, 25 , 255);
            whoseTurn.text = "SOVIET";
        }
    }
}
