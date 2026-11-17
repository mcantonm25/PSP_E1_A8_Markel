namespace PSP_E1_A8_Markel
{
    public class Fitxategia
    {
        public string Izena { get; set; }
        public int Tamaina { get; set; }

        public Fitxategia(string izena, int tamaina)
        {
            Izena = izena;
            Tamaina = tamaina;
        }
    }
}
