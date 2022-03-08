using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class DivisionTileUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Text ArmyHQ;
    public Text ArmyGeneral;
    public Text DivisionName;
    public Text DivisionCom;
    public Text AFV;
    public Text Artilery;
    public Text CV; 
    public Text status;
    public Text Manpower;
    public Text MoralValue;
    public Text Brigades;
    public Text Equipment;
    public Text AACount;
    public Text ATCount;
    public Text MovementValue;

    public UnityEngine.UI.Image divImage;

    public TextMeshProUGUI supportCompanyNames;
    // Update is called once per frame
    void Update()
    {

    }
}
