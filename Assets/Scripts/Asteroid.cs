using UnityEngine;

public class Asteroid : MonoBehaviour, IDamageable
{

    public float Health {  get; }

    void Start()
    {
        
    }

    public void ReceiveDamage(float damageAmount, Vector3 hitPosition, GameAgent sender)
    {
        Destroy(gameObject);
    }

    public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender)
    {
        throw new System.NotImplementedException();
    }


}
