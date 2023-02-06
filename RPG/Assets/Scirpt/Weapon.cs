using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType Type;
}
public enum WeaponType { Axe, Sword, Hammer, Dagger, Shield }