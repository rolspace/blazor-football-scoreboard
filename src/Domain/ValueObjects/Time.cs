namespace Football.Domain.ValueObjects
{
    public class Time : ValueObject
    {
        public int Quarter { get; set; }

        public int QuarterSecondsRemaining { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Quarter;
            yield return QuarterSecondsRemaining;
        }
    }
}
