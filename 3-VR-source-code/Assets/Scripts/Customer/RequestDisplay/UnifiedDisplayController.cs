using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace RequestDisplay
{
    public class UnifiedDisplayController : MonoBehaviour
    {
        public enum DisplayMode
        {
            MultiDisplay, // For TicketController functionality
            SingleDisplay // For SpeechBubbleController functionality
        }

        [Header("Settings")] public DisplayMode displayMode;

        [Header("Ingredient Prefabs")] public GameObject pepperPrefab;
        public GameObject pepperoniPrefab;
        public GameObject fishPrefab;
        public GameObject mushroomPrefab;
        public GameObject olivePrefab;

        [Header("Quarters")] public List<QuarterDisplay> quarterDisplays;

        [Header("Circles GameObjects")] public GameObject clockGameObject;
        public GameObject cutsGameObject;
        private Clock ticketClock;
        private CutsDisplay cutsDisplay;

        private Dictionary<Ingredient, GameObject> ingredientPrefabs;
        private PizzaObject pizzaObject;

        private int currentStep = 0;

        public event Action OnDialogueFinished;

        void Awake()
        {
            ingredientPrefabs = new Dictionary<Ingredient, GameObject>
            {
                { Ingredient.Pepperoni, pepperoniPrefab },
                { Ingredient.Pepper, pepperPrefab },
                { Ingredient.Olive, olivePrefab },
                { Ingredient.Fish, fishPrefab },
                { Ingredient.Mushroom, mushroomPrefab }
            };
        }

        void Start()
        {
            cutsDisplay = cutsGameObject.GetComponent<CutsDisplay>();
            ticketClock = clockGameObject.GetComponent<Clock>();
            ResetEverything();
        }

        void ResetEverything()
        {
            foreach (var quarter in quarterDisplays)
            {
                quarter.ResetQuarter();
            }

            currentStep = 0;
            ticketClock.Reset();
            HideEverything();
        }

        public void StartDialogue(PizzaObject desiredPizza)
        {
            StopAllCoroutines();
            currentStep = 0;
            SetPizza(desiredPizza);
            if (displayMode == DisplayMode.SingleDisplay)
                gameObject.SetActive(true);
            StartCoroutine(DialogueRoutine());
        }

        private IEnumerator DialogueRoutine()
        {
            while (currentStep <= 6)
            {
                ShowNextStep();
                yield return new WaitForSeconds(3f);
            }

            if (displayMode == DisplayMode.SingleDisplay)
                gameObject.SetActive(false);
        }

        public void ShowNextStep()
        {
            if (currentStep < 4)
            {
                while (!HasIngredients(currentStep))
                {
                    SetQuarterIngredients(currentStep + 1, GetQuarterIngredients(currentStep));
                    currentStep++;
                }

                if (HasIngredients(currentStep))
                {
                    if (displayMode == DisplayMode.SingleDisplay)
                        HideEverything();

                    SetQuarterIngredients(currentStep + 1, GetQuarterIngredients(currentStep));
                }

                currentStep++;
                return;
            }

            if (currentStep == 4)
            {
                if (displayMode == DisplayMode.SingleDisplay)
                    HideEverything();

                ShowTime();
                currentStep++;
                return;
            }

            if (currentStep == 5)
            {
                if (displayMode == DisplayMode.SingleDisplay)
                    HideEverything();

                ShowCuts();
                currentStep++;
            }

            if (currentStep == 6)
            {
                if (displayMode == DisplayMode.SingleDisplay) StartCoroutine(HideEverythingWithDelay());
                OnDialogueFinished?.Invoke();
            }
        }

        private bool HasIngredients(int quarterIndex)
        {
            return quarterIndex switch
            {
                0 => pizzaObject.ingredientsQuarter1.Count > 0,
                1 => pizzaObject.ingredientsQuarter2.Count > 0,
                2 => pizzaObject.ingredientsQuarter3.Count > 0,
                3 => pizzaObject.ingredientsQuarter4.Count > 0,
                _ => false
            };
        }

        private List<Ingredient> GetQuarterIngredients(int quarterIndex)
        {
            return quarterIndex switch
            {
                0 => pizzaObject.ingredientsQuarter1,
                1 => pizzaObject.ingredientsQuarter2,
                2 => pizzaObject.ingredientsQuarter3,
                3 => pizzaObject.ingredientsQuarter4,
                _ => new List<Ingredient>()
            };
        }

        public void SetQuarterIngredients(int quarterNumber, List<Ingredient> ingredients)
        {
            var quarter = quarterDisplays[quarterNumber - 1];

            if (displayMode == DisplayMode.SingleDisplay)
                HideEverything();

            quarter.gameObject.SetActive(true);

            var groupedIngredients = ingredients.GroupBy(i => i).ToList();
            if (groupedIngredients.Count > 2)
            {
                Debug.LogError($"Too many ingredient types in quarter {quarterNumber}. Maximum allowed is 2.");
                return;
            }

            var ingredientCounts = groupedIngredients.ToDictionary(g => g.Key, g => g.Count());
            quarter.SetIngredients(ingredientCounts, ingredientPrefabs);
        }

        public void HideEverything()
        {
            foreach (var quarter in quarterDisplays)
                quarter.gameObject.SetActive(false);

            clockGameObject.SetActive(false);
            cutsGameObject.SetActive(false);
        }

        private IEnumerator HideEverythingWithDelay()
        {
            yield return new WaitForSeconds(3f);
            HideEverything();
        }

        private void ShowTime()
        {
            clockGameObject.SetActive(true);
            ticketClock.SetTime(0);
            ticketClock.SetTime(pizzaObject.timeToCook);
        }

        private void ShowCuts()
        {
            cutsGameObject.SetActive(true);
            cutsDisplay.SetActive(pizzaObject.verticalCut, pizzaObject.horizontalCut, pizzaObject.diagonalCut1,
                pizzaObject.diagonalCut2);
        }

        public void SetPizza(PizzaObject pizzaObj)
        {
            pizzaObject = pizzaObj;
            currentStep = 0;
            ResetEverything();
        }
    }
}