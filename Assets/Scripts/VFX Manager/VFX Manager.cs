using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using System;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        VFX_2
    }

    public List<VFXManagerSetup> vfXSetup;

    public void PlayVFXByType(VFXType vFXType, Vector3 position)
    {
        foreach (var item in vfXSetup)
        {
            if (item.vfxType == vFXType)
            {
                var i = Instantiate(item.prefab);
                i.transform.position = position;
                break;
            }
        }
    }
}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab;
}