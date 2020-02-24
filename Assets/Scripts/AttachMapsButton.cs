using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachMapsButton : MonoBehaviour
{
    MoveToMaps mtm;
    public MovemetType movemetType;
    public enum MovemetType { ToMap, FromMap };

    // Start is called before the first frame update
    void Start()
    {
        mtm = GameObject.FindGameObjectWithTag("BookParent").GetComponent<MoveToMaps>();
        this.GetComponent<Button>().onClick.AddListener(delegate
        {
            switch (movemetType)
            {
                case MovemetType.FromMap:
                    mtm.MoveFromMaps();
                    break;
                case MovemetType.ToMap:
                    mtm.MoveTowardsMaps();
                    break;
                
            }
        });
    }
}
