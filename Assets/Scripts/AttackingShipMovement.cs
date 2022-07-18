using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class AttackingShipMovement : EnemyUnitMovement
{
    void Start()
    {
        PrepareAttackingShip();
    }

    private void PrepareAttackingShip()
    {
        shipMaxPosition = 8.0f;
        shipHealth = 100;
    }
}
