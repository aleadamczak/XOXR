using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public int CalculateDeliveryTimeScore(float timeWaiting)
    {
        const float maxTimeBeforePenalty = 180f;
        const float penaltyInterval = 10f;
        const int initialScore = 100;

        if (timeWaiting <= maxTimeBeforePenalty)
        {
            return initialScore;
        }

        float excessTime = timeWaiting - maxTimeBeforePenalty;
        int penalty = Mathf.FloorToInt(excessTime / penaltyInterval);
        int finalScore = Mathf.Max(initialScore - penalty, 0);
        return finalScore;
    }

    public int CalculateIngredientsScore(PizzaObject desiredPizza, PizzaObject deliveredPizza)
    {
        int maxScore = 0;

        var desiredQuarters = new List<List<Ingredient>>
        {
            desiredPizza.ingredientsQuarter1,
            desiredPizza.ingredientsQuarter2,
            desiredPizza.ingredientsQuarter3,
            desiredPizza.ingredientsQuarter4
        };

        var deliveredQuarters = new List<List<Ingredient>>
        {
            deliveredPizza.ingredientsQuarter1,
            deliveredPizza.ingredientsQuarter2,
            deliveredPizza.ingredientsQuarter3,
            deliveredPizza.ingredientsQuarter4
        };

        for (int rotation = 0; rotation < 4; rotation++)
        {
            int currentScore = 0;
            for (int i = 0; i < 4; i++)
            {
                int rotatedIndex = (i + rotation) % 4;
                currentScore += CalculateQuarterScore(desiredQuarters[i], deliveredQuarters[rotatedIndex]);
            }

            maxScore = Mathf.Max(maxScore, currentScore);
        }

        return Mathf.RoundToInt(maxScore);
    }

    public int CalculateTimeCookedScore(PizzaObject desiredPizza, PizzaObject deliveredPizza)
    {
        // To get the perfect time cooked store, the delivered pizza's
        // time cooked must be within a half second of the desired pizza's
        // time to cook. If you go above that, you will be deducted 2 points
        // for every second the pizza spent too long or too little in the oven.
        const int maxScore = 100;
        const float perfectThreshold = 0.5f;
        const int deductionPerSecond = 2;

        float timeDifference = Mathf.Abs(desiredPizza.timeToCook - deliveredPizza.timeToCook);

        if (timeDifference <= perfectThreshold)
        {
            return maxScore;
        }

        int score = maxScore - Mathf.RoundToInt(timeDifference) * deductionPerSecond;
        return Mathf.Max(score, 0);
    }

    public int CalculateCutsScore(PizzaObject desiredPizza, PizzaObject deliveredPizza)
    {
        // To calculate the cuts score, you start out with 100
        // points and it checks for mismatches between the cuts
        // you made and the cuts that the customer wanted.
        // Each mismatch (extra cut or missing cut) deducts 20 points.
        const int maxScore = 100;
        const int deductionPerMismatch = 20;

        int mismatchCount = 0;

        if (desiredPizza.verticalCut != deliveredPizza.verticalCut) mismatchCount++;
        if (desiredPizza.horizontalCut != deliveredPizza.horizontalCut) mismatchCount++;
        if (desiredPizza.diagonalCut1 != deliveredPizza.diagonalCut1) mismatchCount++;
        if (desiredPizza.diagonalCut2 != deliveredPizza.diagonalCut2) mismatchCount++;

        int score = maxScore - mismatchCount * deductionPerMismatch;

        return Mathf.Max(score, 0);
    }

    private int CalculateQuarterScore(List<Ingredient> desiredIngredients, List<Ingredient> deliveredIngredients)
    {
        // Each quarter starts out with 25 points
        // and gets deductions if you are missing ingredients
        // or if you put the wrong ones.
        // If you put the right ingredient but the wrong amount, you also get a deduction, although smaller.
        const int perfectMatchScore = 25;
        int score = perfectMatchScore;

        Dictionary<Ingredient, int> desiredCounts = GetIngredientCounts(desiredIngredients);
        Dictionary<Ingredient, int> deliveredCounts = GetIngredientCounts(deliveredIngredients);

        foreach (var ingredient in desiredCounts.Keys)
        {
            if (deliveredCounts.TryGetValue(ingredient, out int deliveredCount))
            {
                int difference = Math.Abs(desiredCounts[ingredient] - deliveredCount);
                score -= difference * 5;
            }
            else
            {
                score -= desiredCounts[ingredient] * 10;
            }
        }

        foreach (var ingredient in deliveredCounts.Keys)
        {
            if (!desiredCounts.ContainsKey(ingredient))
            {
                score -= deliveredCounts[ingredient] * 10;
            }
        }

        return Mathf.Max(score, 0);
    }

    private Dictionary<Ingredient, int> GetIngredientCounts(List<Ingredient> ingredients)
    {
        Dictionary<Ingredient, int> counts = new Dictionary<Ingredient, int>();

        foreach (var ingredient in ingredients)
        {
            if (counts.ContainsKey(ingredient))
            {
                counts[ingredient]++;
            }
            else
            {
                counts[ingredient] = 1;
            }
        }

        return counts;
    }
}