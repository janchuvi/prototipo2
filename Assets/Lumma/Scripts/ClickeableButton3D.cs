using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickeableButton3D : KinectButton3D
{

    protected override void OnClick()
    {
        Debug.Log("NEW Click detected!: " + name);
    }

}
