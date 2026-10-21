using UnityEngine;

public interface IDamageable
{
    float Health { get; }

    void ReceiveDamage(float damageAmmount, Vector3 hitPosition, GameAgent sender);
    void ReceiveHeal(float healAmmount, Vector3 hitPosition, GameAgent sender);

}
