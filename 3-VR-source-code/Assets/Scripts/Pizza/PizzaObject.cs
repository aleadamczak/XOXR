using System.Collections.Generic;
using System.Text;

namespace DefaultNamespace
{
    public class PizzaObject
    {
        public float timeToCook { get; set; }
        public List<Ingredient> ingredientsQuarter1 { get; set; }
        public List<Ingredient> ingredientsQuarter2 { get; set; }
        public List<Ingredient> ingredientsQuarter3 { get; set; }
        public List<Ingredient> ingredientsQuarter4 { get; set; }
        public bool verticalCut { get; set; }
        public bool horizontalCut { get; set; }
        public bool diagonalCut1 { get; set; }
        public bool diagonalCut2 { get; set; }
        public bool sauce { get; set; }
        public bool cheese { get; set; }

        public static PizzaObject EmptyPizza()
        {
            return new PizzaObject(0, new List<Ingredient>(), new List<Ingredient>(), new List<Ingredient>(),
                new List<Ingredient>(), false, false, false, false, false, false);
        }

        public PizzaObject(int timeToCook, List<Ingredient> ingredientsQuarter1, List<Ingredient> ingredientsQuarter2,
            List<Ingredient> ingredientsQuarter3, List<Ingredient> ingredientsQuarter4, bool verticalCut,
            bool horizontalCut, bool diagonalCut1, bool diagonalCut2, bool sauce, bool cheese)
        {
            this.timeToCook = timeToCook;
            this.ingredientsQuarter1 = ingredientsQuarter1;
            this.ingredientsQuarter2 = ingredientsQuarter2;
            this.ingredientsQuarter3 = ingredientsQuarter3;
            this.ingredientsQuarter4 = ingredientsQuarter4;
            this.verticalCut = verticalCut;
            this.horizontalCut = horizontalCut;
            this.diagonalCut1 = diagonalCut1;
            this.diagonalCut2 = diagonalCut2;
            this.sauce = sauce;
            this.cheese = cheese;
        }

        public void SetTimeCooked(float timeCooked)
        {
            timeToCook = timeCooked;
        }

        public float GetTimeCooked()
        {
            return timeToCook;
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Time to Cook: {timeToCook}");
            sb.AppendLine($"Ingredients Quarter 1: {string.Join(", ", ingredientsQuarter1)}");
            sb.AppendLine($"Ingredients Quarter 2: {string.Join(", ", ingredientsQuarter2)}");
            sb.AppendLine($"Ingredients Quarter 3: {string.Join(", ", ingredientsQuarter3)}");
            sb.AppendLine($"Ingredients Quarter 4: {string.Join(", ", ingredientsQuarter4)}");
            sb.AppendLine($"Cuts - Vertical: {verticalCut}, Horizontal: {horizontalCut}, " +
                          $"Diagonal 1: {diagonalCut1}, Diagonal 2: {diagonalCut2}");
            sb.AppendLine($"Sauce: {sauce}");
            sb.AppendLine($"Cheese: {cheese}");

            return sb.ToString();
        }
    }
}