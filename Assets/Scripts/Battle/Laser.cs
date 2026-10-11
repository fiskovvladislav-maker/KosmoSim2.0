using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public bool CanFire;
    public float MaxDistance = 100f;

    private Coroutine _coroutineFiring;
    private WaitForSeconds _waitForFiring;
    [SerializeField] private float _waitTimeFiring = 1f;

    [Header("Links")]
    [SerializeField] private LineRenderer _lineRenderer;
   
    private void Start()
    {
        if (_lineRenderer == null)
        {
            _lineRenderer = GetComponent<LineRenderer>();

            _waitForFiring = new WaitForSeconds(_waitTimeFiring);

            _lineRenderer.enabled = false;
            CanFire = true;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPosition = FireWeapon(transform.position + transform.forward * MaxDistance);
            VizualizeFiringWeapon(targetPosition);
        }
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
                //Destroy(targetHit.gameObject);
                return targetHit.position;
            }

        }

        return targetPosition;
    }

    public void VizualizeFiringWeapon(Vector3 targetPosition)
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
