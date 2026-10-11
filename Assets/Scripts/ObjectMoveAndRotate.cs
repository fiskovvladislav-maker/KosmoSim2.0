using UnityEngine;
using System.Collections;

public class ObjectMoveAndRotate : MonoBehaviour
{
    public Transform target; // the object to rotate around
	public float speedRotation;
    public float speedMoving = 2f; // the speed of rotation

    private IEnumerator coroutineMoveSun;
    private WaitForSeconds waitForMoveSun;
    private float waitForMoveSunDelay = 0.1f;

	
	private void Start() 
    {
		if (target == null) 
		{
			target = transform;
			Debug.Log ("RotateAround target not specified. Defaulting to parent GameObject");
		}
        waitForMoveSun = new WaitForSeconds(waitForMoveSunDelay);
        StartCoroutineMoveSun();
	}

	private void Update () 
    {

		RotateAround();

	}


    private void RotateAround()
    {
        transform.RotateAround(target.transform.position,target.transform.up,speedRotation * Time.deltaTime);
    }

    private void MoveSun()
    {
        if(gameObject.name == "Sun")
        {
            transform.Translate(Vector3.forward * speedMoving * Time.deltaTime);
        }
    }

    [ContextMenu("StartCoroutineMoveSun")]
    public void StartCoroutineMoveSun()
    {
        coroutineMoveSun = MoveSunCor();
        StartCoroutine(coroutineMoveSun);
    }

    [ContextMenu("StopCoroutineMoveSun")]
    public void StopCoroutineMoveSun()
    {
        StopCoroutine(coroutineMoveSun);
    }

    private IEnumerator MoveSunCor()
    {
        while(true)
        {
            yield return waitForMoveSun;
            MoveSun();
        }
    }
}
