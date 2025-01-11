using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Production_reporting_app.Models
{
    public class DataCollectionsForDashboardView
    {
        List<DashboardData> DaneDoWyswietlenia;
        public ObservableCollection<DashboardData> DaneDoWyswietleniaCollection;

        public DataCollectionsForDashboardView()
        {
            DaneDoWyswietlenia = new List<DashboardData>();
            DaneDoWyswietleniaCollection = new ObservableCollection<DashboardData>();
            

        }

        public void wczytajDaneZPliku()
        {
            string path = "C:\\Users\\Myszogin i Martynisz\\Documents\\Playground\\wczytywaniedanych\\wczytywaniedanych\\dane.txt";

            string line;
            line = File.ReadAllText(path);

            Console.WriteLine(line);

            DaneDoWyswietleniaCollection = JsonConvert.DeserializeObject<ObservableCollection<DashboardData>>(line);
            pokoloruj();
            strinifyDates();
        }
        private void pokoloruj()
        {
            ColorsLegend legenda = new ColorsLegend();
            foreach (DashboardData dane in DaneDoWyswietleniaCollection)
            {
                dane.SkutekPostojuKolor = legenda.rozpoznajKategorie(dane.SkutekPostoju);
                dane.PrzyczynaPostojuKolor = legenda.rozpoznajKategorie(dane.PrzyczynaPostoju);
                dane.SczegulyPostojuKolor = legenda.rozpoznajKategorie(dane.CzyPostojZakonczony);
            
            
            }
        
        }
        private void strinifyDates()
        {
            foreach (DashboardData dane in DaneDoWyswietleniaCollection)
            {
                dane.CzasRozpoczeciaPostojustring = dane.CzasRozpoczeciaPostoju.ToString("dd.MM.yy HH:mm");
                if (dane.CzasZakonczeniaPostoju != DateTime.UnixEpoch)
                {
                    dane.CzasZakonczeniaPostojustring = dane.CzasZakonczeniaPostoju.ToString("dd.MM.yy HH:mm");
                }
                else {
                    dane.CzasZakonczeniaPostojustring = "-------";


                }
            
            
            }
        }
    }
}
