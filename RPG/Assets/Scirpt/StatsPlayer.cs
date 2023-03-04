using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public QuickslotInventory _quickslotInventory;
    public PlayerMovement _playerMovement;
    public Transform quickslotParent;
    public GameObject particleDamage;
    public AudioSource takeDamageClip;

    public float healthPoint;
    public float manaPoint;
    public int strength;

    public void TakeDamage(int damage)
    {
        takeDamageClip.Play();
        StartCoroutine(ParticleDamagePlayer());
        healthPoint -= damage;
    }
    public void TakeStreng()
    {
        var CurrentItem = quickslotParent.GetChild(_quickslotInventory.currentQuickslotID).GetComponent<InventorySlot>().item;
        strength = CurrentItem.damageCount;
    }
    private void Drinking()
    {
        var CurrentItem = quickslotParent.GetChild(_quickslotInventory.currentQuickslotID).GetComponent<InventorySlot>().item;
        if (CurrentItem.itemName == "HealthPotion")
        {
            if (healthPoint + CurrentItem.healthCount <= 100)
            {
                healthPoint += CurrentItem.healthCount;
            }
            else
            {
                healthPoint = 100;
            }
        }
        else if (CurrentItem.itemName == "ManaPotion")
        {
            if (manaPoint + CurrentItem.manaCount <= 100)
            {
                manaPoint += CurrentItem.manaCount;
            }
            else
            {
                manaPoint = 100;
            }
        }
        else if (CurrentItem.itemName == "SpeedBaffPotion")
        {
            StartCoroutine(JumpBaffPotion());
        }
        else StartCoroutine(SpeedBaffPotion());

        _quickslotInventory.CheckAmountItem();
    }

    IEnumerator ParticleDamagePlayer()
    {
        particleDamage.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        particleDamage.SetActive(false);
    }
    IEnumerator SpeedBaffPotion()
    {
        if (_playerMovement.speed <= 7f)
        {
            _playerMovement.speed += 3f;
        }
        yield return new WaitForSeconds(10.0f);
        if (_playerMovement.speed >= 4f)
        {
            _playerMovement.speed -= 3f;
        }
        
    }
    IEnumerator JumpBaffPotion()
    {
        if (_playerMovement.jumpHeight == 0.7f)
        {
            _playerMovement.jumpHeight += 0.7f;
        }
        yield return new WaitForSeconds(10.0f);
        _playerMovement.jumpHeight = 0.7f;
    }
}
