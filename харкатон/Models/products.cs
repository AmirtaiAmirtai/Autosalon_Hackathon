namespace харкатон.Controllers.models;



    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public double GeneralRating { get; set; }
        public List<string> Review { get; set; } = new();
        public List<int> Rate { get; set; } = new();

    }

