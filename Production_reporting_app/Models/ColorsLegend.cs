using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    public class ColorsLegend
    {
        List<KoloryPrzypisanie> colors;
        public ColorsLegend() 
        {
            colors = new List<KoloryPrzypisanie>();
            colors.Add(new KoloryPrzypisanie{kategoria="Awaria", color=Colors.Red });
            colors.Add(new KoloryPrzypisanie { kategoria = "Regulacja", color = Colors.Blue });
            colors.Add(new KoloryPrzypisanie { kategoria = "Problemy Jakościowe", color = Colors.LightBlue });
            colors.Add(new KoloryPrzypisanie { kategoria = "Braki Materiałów", color = Colors.GreenYellow });



            colors.Add(new KoloryPrzypisanie { kategoria = "Postój Linii", color = Colors.Red });
            colors.Add(new KoloryPrzypisanie { kategoria = "Mikroprzestoje", color = Colors.LightBlue });
            colors.Add(new KoloryPrzypisanie { kategoria = "Problemy z Jakością", color = Colors.GreenYellow });
            colors.Add(new KoloryPrzypisanie { kategoria = "Spowolnienie Linii", color = Colors.Blue });
        }




        internal class KoloryPrzypisanie
        {
         internal string kategoria;
           internal Color color;
        }
        public Color rozpoznajKategorie(string textdoRozpoznania)
        {
            foreach (KoloryPrzypisanie kolor in colors)
            {
                if (textdoRozpoznania==kolor.kategoria)
                {  return kolor.color; }
            
            
            }
            return Colors.Black;
        
        }
        public Color rozpoznajKategorie(bool czyZakonczony)
        {
            if (czyZakonczony)
            { return Colors.Green; }
            else { return Colors.White; }
        
        }
    }
}
