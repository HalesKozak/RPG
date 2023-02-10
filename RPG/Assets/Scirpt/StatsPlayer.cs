using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private DataBase _dataBase;
    public int HP;
    public int MP;
    public int Strength;
}
