using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;

public interface IWeapon 
{
    Vector3 FireWeapon(Vector3 targetPosition);
    void Damage(float damageAmount, Vector3 targetHitPosition, GameAgent sender);

    void VizualizeFiring(Vector3 targetPosition);
}
