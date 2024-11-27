using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleAnimalWalk : MonoBehaviour
{
    public float walkRange = 5f;
    public float moveSpeed = 2f;
    public float idleTime = 2;
    public float turnSpeed = 10f;
    private bool isMoving;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Coroutine movementCoroutine;
    private float movementTimeout = 8f;
    private float timeSpent;


    void Start()
    {
        startPosition = transform.position;
        movementCoroutine = StartCoroutine(MoveRandomly());
    }

    void Update()
    {
        if (isMoving)
        {
            timeSpent += Time.deltaTime;
        }

        if (timeSpent > movementTimeout)
        {
            SetShouldMove(false);
            isMoving = false;
            SetShouldMove(true);
            timeSpent = 0;
        }
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            {
                if (!isMoving)
                {
                    timeSpent = 0;
                    Vector3 currPos = transform.position;
                    targetPosition = RandomPosition();
                    isMoving = true;
                    while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                    {
                        FaceTarget(targetPosition);
                        transform.position =
                            Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                        yield return null;
                    }
                }

                isMoving = false;
                yield return new WaitForSeconds(idleTime);
            }
        }
    }


    public void SetShouldMove(bool shouldMove)
    {
        if (!shouldMove && movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
        else if (shouldMove && movementCoroutine == null)
        {
            movementCoroutine = StartCoroutine(MoveRandomly());
        }
    }

    Vector3 RandomPosition()
    {
        float randomX = Random.Range(startPosition.x - walkRange, startPosition.x + walkRange);
        float randomZ = Random.Range(startPosition.z - walkRange, startPosition.z + walkRange);
        return new Vector3(randomX, startPosition.y, randomZ);
    }

    void FaceTarget(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }
}