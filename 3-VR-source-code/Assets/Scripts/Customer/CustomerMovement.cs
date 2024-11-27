using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CustomerMovement : MonoBehaviour
{
    public List<Transform> Positions;
    public float moveSpeed = 2f;
    private bool canMoveToWait = false;
    private bool canMoveToExit = false;
    public event System.Action OnCustomerExited;
    private Animator anim;
    private Rigidbody rb;

    public void setPositions(List<Transform> positions)
    {
        canMoveToWait = false; 
        canMoveToExit = false;
        Positions = positions;
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(MoveCustomer());
    }

    private IEnumerator MoveCustomer()
    {
        while (Positions.Count == 0)
        {
            yield return null;
        }

        for (int i = 1; i < 3; i++)
        {
            anim.SetBool("walking", true);
            Transform targetPosition = Positions[i];
            while (Vector3.Distance(gameObject.transform.position, targetPosition.position) > 0.1f)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                    targetPosition.position, moveSpeed * Time.deltaTime);

                yield return null;
            }

            anim.SetBool("walking", false);
            gameObject.transform.rotation = targetPosition.rotation;

            yield return new WaitForSeconds(1f);
        }

        anim.SetBool("walking", false);
        while (!canMoveToWait)
        {
            yield return null;
        }

        gameObject.transform.Rotate(new Vector3(0, 90, 0));
        anim.SetBool("walking", true);
        Transform fourthPosition = Positions[3];
        while (Vector3.Distance(gameObject.transform.position, fourthPosition.position) > 0.1f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                fourthPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        anim.SetBool("walking", false);
        gameObject.transform.rotation = fourthPosition.rotation;
        while (!canMoveToExit)
        {
            yield return null;
        }

        gameObject.transform.Rotate(new Vector3(0, 180, 0));
        for (int i = 4; i < 6; i++)
        {
            Transform targetPosition = Positions[i];
            anim.SetBool("walking", true);
            while (Vector3.Distance(gameObject.transform.position, targetPosition.position) > 0.1f)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                    targetPosition.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            gameObject.transform.rotation = targetPosition.rotation;
            anim.SetBool("walking", false);
            yield return new WaitForSeconds(1f);
        }

        OnCustomerExited?.Invoke();
        Destroy(gameObject);
    }

    public void MoveToWaitingPosition()
    {
        canMoveToWait = true;
    }

    public void Exit()
    {
        canMoveToExit = true;
    }
}