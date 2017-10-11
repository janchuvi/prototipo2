using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickeableButton : KinectButton
{

    protected override void OnClick()
    {
        Debug.Log("NEW Click detected!: " + name);
    }

}
