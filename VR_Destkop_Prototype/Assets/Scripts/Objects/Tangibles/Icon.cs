﻿using UnityEngine;
using System.Collections;
using System;

public class Icon : TangibleObject
{
    public override void OnDeselect()
    {
        throw new NotImplementedException();
    }

    public override void OnGrab()
    {
        throw new NotImplementedException();
    }

    public override void OnRelease()
    {
        throw new NotImplementedException();
    }

    public override void OnSelect()
    {
		SetEmission(new Color(100,100,100));
    }
}


