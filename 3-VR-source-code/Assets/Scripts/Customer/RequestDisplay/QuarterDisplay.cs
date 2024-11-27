using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace RequestDisplay
{
    [Serializable]
    public class QuarterDisplay : MonoBehaviour
    {
        private Transform ingredient1Transform;
        private TMP_Text ingredient1Text;
        private Transform ingredient2Transform;
        private TMP_Text ingredient2Text;

        private void Start()
        {
            ingredient1Transform = gameObject.transform.FindChildByName("FirstIngredientTransform");
            ingredient1Text = gameObject.transform.FindChildByName("FirstIngredientText").GetComponent<TMP_Text>();
            ingredient2Transform = gameObject.transform.FindChildByName("SecondIngredientTransform");
            ingredient2Text = gameObject.transform.FindChildByName("SecondIngredientText").GetComponent<TMP_Text>();
        }

        public void SetIngredients(Dictionary<Ingredient, int> ingredientCounts, Dictionary<Ingredient, GameObject> ingredientPrefabs)
        {
            var ingredients = ingredientCounts.Keys.ToList();

            if (ingredients.Count > 0)
            {
                var prefab = ingredientPrefabs[ingredients[0]];
                Instantiate(prefab, ingredient1Transform.position, ingredient1Transform.rotation, ingredient1Transform);
                ingredient1Text.text = $"x{ingredientCounts[ingredients[0]]}";
            }

            if (ingredients.Count > 1)
            {
                var prefab = ingredientPrefabs[ingredients[1]];
                Instantiate(prefab, ingredient2Transform.position, ingredient2Transform.rotation, ingredient2Transform);
                ingredient2Text.text = $"x{ingredientCounts[ingredients[1]]}";
            }
        }
        public void ResetQuarter()
        {
            foreach (Transform child in ingredient1Transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in ingredient2Transform)
            {
                Destroy(child.gameObject);
            }

            ingredient1Text.text = string.Empty;
            ingredient2Text.text = string.Empty;
        }
    }
}