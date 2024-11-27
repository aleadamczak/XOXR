using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using RequestDisplay;
using UnityEngine;

public class CustomerRequest : MonoBehaviour
{
    private PizzaObject desiredPizza;
    private PizzaObject deliveredPizza;

    public bool isWaiting;
    public float timeWaiting;
    private int deliveryTimeScore;
    private int ingredientsScore;
    private int timeCookedScore;
    private int cutsScore;
    private bool pizzaWasDelivered;

    private CustomerMovement movement;

    private GameObject scoresParent;
    private ScoreDisplay _scoreDisplay;
    private ScoreCalculator scoreCalculator;
    private Vector3 scoresParentPosition = new Vector3(-1.26f, 1.4f, 3.2f);
    
    private UnifiedDisplayController speechBubbleController;
    private UnifiedDisplayController ticketController;
    private GameObject ticketParent;

    private DeliveryStationHandler deliveryStation;

    private void Awake()
    {
        DesiredPizza dp = GetComponent<DesiredPizza>();
        desiredPizza = dp.DesiredPizzaObject();
    }

    private void Start()
    {
        SetExternalDependencies();
        SetInternalDependencies();
        SubscribeToEvents();
        HideScores();
        StartCoroutine(DelayedStartDialogue());
    }

    void SetInternalDependencies()
    {
        movement = GetComponent<CustomerMovement>();
        _scoreDisplay = scoresParent.GetComponent<ScoreDisplay>();
        ticketController = ticketParent.GetComponent<UnifiedDisplayController>();
    }

    void SetExternalDependencies()
    {
        deliveryStation =
            GameObject.Find("DeliveryStation").GetComponent<DeliveryStationHandler>();
        scoresParent = GameObject.Find("ScoresParent");
        scoreCalculator = GameObject.Find("ScoreCalculator").GetComponent<ScoreCalculator>();
        speechBubbleController = GameObject.Find("SpeechBubbleParent").GetComponent<UnifiedDisplayController>();
        ticketParent = GameObject.Find("TicketParent");
    }

    void SubscribeToEvents()
    {
        if (deliveryStation != null)
        {
            deliveryStation.onPizzaDelivered.AddListener(OnPizzaDelivered);
        }
        if (speechBubbleController != null)
        {
            speechBubbleController.OnDialogueFinished += StartWaiting;
        }
        
    }

    private IEnumerator DelayedStartDialogue()
    {
        yield return new WaitForSeconds(6);
        speechBubbleController.StartDialogue(desiredPizza);
        ticketController.StartDialogue(desiredPizza);
    }

    public void ShowScores()
    {
        scoresParent.transform.position = scoresParentPosition;
    }

    public void HideScores()
    {
        scoresParent.transform.position = new Vector3(0, -10, 0);
    }

    private void Update()
    {
        if (isWaiting)
        {
            timeWaiting += Time.deltaTime;
        }
    }

    private void OnPizzaDelivered(GameObject pizza)
    {
        Pizza pizzaScript = pizza.GetComponent<Pizza>();
        PizzaObject pizzaObject = pizzaScript.pizza;
        if (pizzaObject != null && pizzaWasDelivered == false)
        {
            DeliverPizza(pizzaObject);
            pizzaWasDelivered = true;
            Destroy(pizza, 0.5f);
        }

        ticketController.HideEverything();
    }

    public void StartWaiting()
    {
        isWaiting = true;
        movement.MoveToWaitingPosition();
    }

    public void DeliverPizza(PizzaObject pizza)
    {
        deliveredPizza = pizza;
        CalculateScore();
        SetAndShowScoreDisplay();
        int totalScore = deliveryTimeScore + ingredientsScore + timeCookedScore + cutsScore;
        deliveryStation.PlayReactionAudio(totalScore);
        StartCoroutine(LeaveWithDelay());
    }

    private void SetAndShowScoreDisplay()
    {
        ShowScores();
        _scoreDisplay.SetTimeScore(deliveryTimeScore.ToString());
        _scoreDisplay.SetIngredientsScore(ingredientsScore.ToString());
        _scoreDisplay.SetOvenScore(timeCookedScore.ToString());
        _scoreDisplay.SetCutsScore(cutsScore.ToString());
    }

    private IEnumerator LeaveWithDelay()
    {
        yield return new WaitForSeconds(10f);
        HideScores();
        movement.Exit();
    }

    private void CalculateScore()
    {
        deliveryTimeScore = scoreCalculator.CalculateDeliveryTimeScore(timeWaiting);
        ingredientsScore = scoreCalculator.CalculateIngredientsScore(desiredPizza, deliveredPizza);
        timeCookedScore = scoreCalculator.CalculateTimeCookedScore(desiredPizza, deliveredPizza);
        cutsScore = scoreCalculator.CalculateCutsScore(desiredPizza, deliveredPizza);
    }

    private void OnDestroy()
    {
        if (deliveryStation != null)
        {
            deliveryStation.onPizzaDelivered.RemoveListener(OnPizzaDelivered);
        }

        if (speechBubbleController != null)
        {
            speechBubbleController.OnDialogueFinished -= StartWaiting;
        }
    }
}