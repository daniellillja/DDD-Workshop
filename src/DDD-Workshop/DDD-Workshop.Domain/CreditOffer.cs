namespace DDD_Workshop.Domain
{
    public class CreditOffer
    {
        public CreditOffer DefaultOffer()
        {
            APR = 29.99;
            CreditLimit = 5000;

            return this;
        }

        public int CreditLimit { get; private set; }

        public double APR { get; private set; }
    }
}