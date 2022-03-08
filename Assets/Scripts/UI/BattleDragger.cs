using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BattleDragger : MonoBehaviour, IDragHandler
{


    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    #endregion
}
