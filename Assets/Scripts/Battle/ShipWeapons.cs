using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShipWeapons : MonoBehaviour
{
    public SpaceShip _SpaceShip;

    public List<IWeapon> Weapons = new List<IWeapon>();

    public float MaxDistanceToTarget = 250f;

    private void Awake()
    {
        if(_SpaceShip == null)
        {
            _SpaceShip = GetComponentInParent<SpaceShip>();
        }
        Weapons = GetComponentsInChildren<IWeapon>().ToList();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapons();
        }
    }

    public void FireWeapons()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, MaxDistanceToTarget)) 
        {
            foreach (var weapon in Weapons)
            {
                weapon.FireWeapon(hit.point);
            }
        }
        else
        {
            foreach (var weapon in Weapons)
            {
                weapon.FireWeapon(ray.origin + ray.direction * MaxDistanceToTarget);
            }
        }
        
    }

}
