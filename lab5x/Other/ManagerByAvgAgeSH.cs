namespace lab5.Other
{
    public class ManagerByAvgAgeSH
    {
        public int ManagerId { get; set; }
        public int AvgAgeOfSuperHero { get; set; }

        public ManagerByAvgAgeSH(int managerId, int avgAgeOfSuperHero)
        {
            ManagerId = managerId;
            AvgAgeOfSuperHero = avgAgeOfSuperHero;
        }
    }
}
