namespace AnimalWorld.Web.Models.Home
{
    public class RandomAnimalViewModel
    {
        public string Image { get; set; }

        public string Fact { get; set; }

        private const int SHORT_FACT_LENGTH = 100;

        public string ShortFact
        {
            get
            {
                if (string.IsNullOrEmpty(Fact))
                {
                    return Fact;
                }

                return Fact.Length <= SHORT_FACT_LENGTH
                    ? Fact
                    : Fact.Substring(0, SHORT_FACT_LENGTH) + "...";
            }
        }
    }
}
