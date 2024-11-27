using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    public List<GameObject> CustomerPrefabs;
    public List<Transform> Positions;
    public CustomerMovement currentCustomer;

    private List<GameObject> availableCustomers;
    private List<GameObject> usedCustomers;

    private void Start()
    {
        availableCustomers = new List<GameObject>(CustomerPrefabs);
        usedCustomers = new List<GameObject>();
        GenerateCustomer();
    }

    private void GenerateCustomer()
    {
        if (availableCustomers.Count == 0)
        {
            availableCustomers = new List<GameObject>(usedCustomers);
            usedCustomers.Clear();
        }
        int randomIndex = Random.Range(0, availableCustomers.Count);
        GameObject randomCustomerPrefab = availableCustomers[randomIndex];
        availableCustomers.RemoveAt(randomIndex);
        usedCustomers.Add(randomCustomerPrefab);
        GameObject newCustomer = Instantiate(randomCustomerPrefab, Positions[0].position, Positions[0].rotation);
        currentCustomer = newCustomer.GetComponent<CustomerMovement>();
        currentCustomer.setPositions(Positions);
        currentCustomer.OnCustomerExited += HandleCustomerExit;
    }

    private void HandleCustomerExit()
    {
        currentCustomer.OnCustomerExited -= HandleCustomerExit;
        GenerateCustomer();
    }
}