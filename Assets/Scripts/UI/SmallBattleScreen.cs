using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SmallBattleScreen : MonoBehaviour
{
    public TextMeshProUGUI ATTACKERDIVNAME;
    public TextMeshProUGUI DEFENDERDIVNAME;
    public TextMeshProUGUI PARTICIPATINGATTACKEROVERLAY;
    public TextMeshProUGUI PARTICIPATINGDEFENDEROVERLAY;
    public TextMeshProUGUI ATTACKERLOSSES;
    public TextMeshProUGUI DEFENDERLOSSES;

    public Button biggerWiewButton;

    public Button exitbutton;
    public Canvas biggerWiew;
    public Canvas smallWiew;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        biggerWiewButton.onClick.AddListener(delegate () { biggerWiew.renderMode = RenderMode.ScreenSpaceCamera; });
        exitbutton.onClick.AddListener(delegate () { smallWiew.renderMode = RenderMode.WorldSpace; });
    }
}
