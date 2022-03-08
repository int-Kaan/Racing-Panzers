using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyGrasslandHex : MonoBehaviour
{
    public Unit Unit = null;
    public HolyGrasslandHex[] neighboors;
    public Selecter selecter;
    public Material[] material;

    [Header("Is Hex Defensible ?")]
    [SerializeField]
    public bool isDefensible = false;

    [Header("Hex Type 0-> Soviet  //  1-> German")]
    [SerializeField]
    public int HexType = 0;

    [Header("Is Victory Point ?")]
    [SerializeField]
    public bool isVictoryPoint = false;
    public int tileType;

    public multi_handler multihandle;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }
    /*    [Header("Glow")]
        [SerializeField]*/

    // Update is called once per frame
    void Update()
    {
        if (multihandle.weather == 2 || multihandle.weather == 3)
        {
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = material[1];
        }
        else
        {
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            rend.sharedMaterial = material[0];
        }


    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        { selecter.hex = this; }
       
    }
    public Vector3 getPosition()
    {
        return this.transform.position;
    }

}
