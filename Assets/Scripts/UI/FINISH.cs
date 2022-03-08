using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FINISH : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI whowins;
    public Button exitbutton;
    public Canvas wiew;
    public multi_handler multihandler1;

    public HolyGrasslandHex[] Victoryhexes;
    // Update is called once per frame
    void Start()
    {
        wiew.renderMode = RenderMode.WorldSpace;
    }
    int maincouner = 0;
    void Update()
    {
        if (multihandler1.turnCounter.BaseTurn > 30)
        {
            wiew.renderMode = RenderMode.ScreenSpaceCamera;
            whowins.text = "Defender Victory";
        }
        if (isattackerwin() && maincouner == 0)
        {
            maincouner = 1;
            wiew.renderMode = RenderMode.ScreenSpaceCamera;
            whowins.text = "Attacker Victory";
        }

        //biggerWiewButton.onClick.AddListener(delegate () { biggerWiew.renderMode = RenderMode.ScreenSpaceCamera; });
        //exitbutton.onClick.AddListener(delegate () { smallWiew.renderMode = RenderMode.WorldSpace; });

        exitbutton.onClick.AddListener(delegate () { Application.Quit(); });
    }

    public bool isattackerwin()
    {
        if(Victoryhexes[0].HexType == 1 && Victoryhexes[1].HexType == 1 && Victoryhexes[2].HexType == 1)
        {
            return true;
        }
        return false;
    }
}
