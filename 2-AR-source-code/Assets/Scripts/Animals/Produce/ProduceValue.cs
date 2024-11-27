using System;
using System.Collections.Generic;

namespace Animals.Produce
{
    public enum ProduceType
    {
        Beef,
        Pork,
        Mutton,
        Chicken,
        Egg,
        Wool,
        Milk,
        Nothing
    }
    public class ProduceValue
    {
        public static int GetProduceValue(ProduceType produceType, int quality)
        {
            int baseValue = produceType switch
            {
                ProduceType.Beef => 50,
                ProduceType.Pork => 25,
                ProduceType.Mutton => 25,
                ProduceType.Chicken => 20,
                ProduceType.Egg => 10,
                ProduceType.Wool => 10,
                ProduceType.Milk => 10,
                _ => 0
            };

            return quality switch
            {
                1 => (int)Math.Round(baseValue / 2f),
                2 => baseValue,
                3 => baseValue * 2,
                _ => 0
            };
        }
    }
}