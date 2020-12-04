namespace Payslip
{
    public class TaxBracket
    {
        private readonly int _upperBound;
        public readonly int LowerBound;
        public readonly int FlatFee;
        public readonly double Rate;

        public TaxBracket(int lower, int upper,int flatFee, double rate )
        {
            _upperBound = upper;
            LowerBound = lower;
            FlatFee = flatFee;
            Rate = rate;
        }

        public bool InBracket(int amount)
        {
            return amount >= LowerBound && amount <= _upperBound;
        }
    }
}