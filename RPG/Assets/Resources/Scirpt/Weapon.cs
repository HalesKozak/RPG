using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType Type;
    public Material MaterialType;

    public void DeleteObj()
    {
        Destroy(gameObject);
    }
}
public enum WeaponType { Axe, Sword, Elite, Shield }
public enum Material { None, Stone, Bone, Wood }