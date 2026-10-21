using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour, IWeapon
{
    public bool CanFire;
    public float MaxDistance = 100f;
    public float damageAmount = 5f;

    private Coroutine _coroutineFiring;
    private WaitForSeconds _waitForFiring;
    [SerializeField] private float _waitTimeFiring = 1f;

    [Header("Links")]
    [SerializeField] private LineRenderer _lineRenderer;
    private ShipWeapons _ShipWeapons;

    public List<IDamageable> TargetsHit = new List<IDamageable>();

    private void Awake()
    {
        if (_ShipWeapons == null)
        {
            _ShipWeapons = GetComponentInParent<ShipWeapons>();
        }
        if (_lineRenderer == null)
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }
    }

    private void Start()
    {

        _waitForFiring = new WaitForSeconds(_waitTimeFiring);

        _lineRenderer.enabled = false;
        CanFire = true;

    }


    public Vector3 FireWeapon(Vector3 targetPosition)
    {
        RaycastHit hitInfo;
        var direction = targetPosition - transform.position;
        if (Physics.Raycast(transform.position, direction, out hitInfo, MaxDistance))
        {
            var targetHit = hitInfo.transform;
            if (targetHit != null)
            {
                Debug.Log($"targetHit: {targetHit.name}");
                var damageableHit = targetHit.GetComponent<IDamageable>();
                if (damageableHit != null)
                {
                    TargetsHit.Add(damageableHit);
                    Damage(damageAmount, targetHit.position, _ShipWeapons._SpaceShip.ShipAgent);
                }
                VizualizeFiring(targetHit.position);
                return targetHit.position;
            }
        }
        VizualizeFiring(transform.position + direction.normalized * MaxDistance);
        return targetPosition;
    }

    public void Damage(float damageAmount, Vector3 targetHitPosition, GameAgent sender)
    {
        foreach (var targetHit in TargetsHit)
        {
            targetHit.ReceiveDamage(damageAmount, targetHitPosition, sender);
        }
        TargetsHit.Clear();
    }

    public void VizualizeFiring(Vector3 targetPosition)
    {
        if (CanFire)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, targetPosition);

            CanFire = false;

            _coroutineFiring = StartCoroutine(FiringCor());
        }
    }

    private IEnumerator FiringCor()
    {
        yield return _waitForFiring;

        CanFire = true;
        _lineRenderer.enabled = false;
    }

}