namespace Animals.Produce
{
    public class ProduceItem
    {
        public ProduceType Type { get; private set; }
        public int Quality { get; private set; }
        public int Value { get; private set; }

        public ProduceItem(ProduceType type, int quality)
        {
            Type = type;
            Quality = quality;
            Value = ProduceValue.GetProduceValue(Type, Quality);
        }
        public override string ToString()
        {
            return $"Type: {Type}, Quality: {Quality}, Value: {Value}";
        }
    }
}