namespace Knab.CodingAssignment.Domain.Exchange
{
    public class ExchangeRates
    {
        public ExchangeRates(double usd, double eur, double brl, double gbp, double aud)
        {
            Usd = usd;
            Eur = eur;
            Brl = brl;
            Gbp = gbp;
            Aud = aud;
        }

        public static ExchangeRates Empty => new ExchangeRates(1,0,0,0,0);

        public double Usd { get; set; }
        public double Eur { get; set; }
        public double Brl { get; set; }
        public double Gbp { get; set; }
        public double Aud { get; set; }

    }
}