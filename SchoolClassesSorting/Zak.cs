namespace SchoolClassesSorting
{
    public class Zak
    {
        public Zak(string jmeno, int kategorie, char skola)
        {
            Id = -1;
            Jmeno = jmeno;
            Skola = skola;
            Kategorie = kategorie;
        }
        public int Id { get; set; }
        public string Jmeno { get; set; }
        public int Kategorie { get; set; }
        public char Skola { get; set; }
    }
}
