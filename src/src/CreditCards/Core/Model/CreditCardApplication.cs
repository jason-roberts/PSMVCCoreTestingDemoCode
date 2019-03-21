namespace CreditCards.Core.Model
{
    public class CreditCardApplication
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal GrossAnnualIncome { get; set; }
        public string FrequentFlyerNumber { get; set; }
        public CreditCardApplicationDecision Decision { get; set; }
    }
}
