namespace SchoolClassesSorting
{
    public class Trida
    {
        public readonly Zak[,] Rozlozeni;
        public readonly int LengthX;
        public readonly int LengthY;
        public Trida(int pocetZakuMax)
        {
            var pocetZakuVRade = pocetZakuMax / Konstanty.PocetRadVeTride;
            var delkaRady = pocetZakuVRade / 2;
            Rozlozeni = new Zak[Konstanty.PocetRadVeTride * 2, delkaRady];
            LengthX = Rozlozeni.GetLength(0);
            LengthY = Rozlozeni.GetLength(1);
        }

        public bool AcceptablePlace(Zak zak, int x, int y)
        {
            foreach (var zakazane in Konstanty.ZakazanePozice)
            {
                var localX = x + zakazane.Item1;
                var localY = y + zakazane.Item2;
                if (localX < 0 || localY < 0 || localX >= LengthX || localY >= LengthY)
                    continue;

                var kontrolovanyZak = Rozlozeni[localX, localY];
                if (kontrolovanyZak == null)
                    continue;
                if (kontrolovanyZak.Kategorie == zak.Kategorie || kontrolovanyZak.Skola == zak.Skola)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
