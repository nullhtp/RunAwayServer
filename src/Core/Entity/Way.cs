namespace Core
{
    public class Way
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public City City { get; set; }
        public Point[] Points { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
    }
}
