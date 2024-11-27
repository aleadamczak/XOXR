using System.Collections;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    public float rayDistance = 5f;
    public float frontOffset = 0.5f;
    public float walkBackDistance = 1f;
    public float turnDuration = 0.5f;

    private IdleAnimalWalk walkScript;
    private bool isTurningAround;


    void Start()
    {
        walkScript = GetComponent<IdleAnimalWalk>();
    }


    void Update()
    {
        if (!IsGroundBelow() && !isTurningAround)
        {
            walkScript.SetShouldMove(false);
            StartCoroutine(TurnAroundAndWalkBack());
        }
    }

    bool IsGroundBelow()
    {
        Vector3 rayOrigin = transform.position + transform.forward * frontOffset;
        return Physics.Raycast(rayOrigin, Vector3.down, rayDistance);
    }

    private IEnumerator TurnAroundAndWalkBack()
    {
        isTurningAround = true;

        float elapsedTime = 0f;
        Quaternion originalRotation = transform.rotation;
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, 180f, 0);

        while (elapsedTime < turnDuration)
        {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedTime / turnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        Vector3 walkBackPosition = transform.position + transform.forward * walkBackDistance;
        elapsedTime = 0f;
        while (Vector3.Distance(transform.position, walkBackPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkBackPosition,
                walkScript.moveSpeed * Time.deltaTime);
            yield return null;
        }

        walkScript.SetShouldMove(true);
        isTurningAround = false;
    }
}