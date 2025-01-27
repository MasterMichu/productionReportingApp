using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Production_reporting_app.Models
{
    public class DataCollectionsForDashboardView
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<DashboardData> DaneDoWyswietleniaCollection;

        public DataCollectionsForDashboardView()
        {
            
            DaneDoWyswietleniaCollection = new ObservableCollection<DashboardData>();
            _httpClient = new HttpClient();
            

        }

        public async Task<ObservableCollection<DashboardData>> wczytajDaneZPliku()
        {
            await ObrobkaDanychK1();
            await ObrobkaDanychK2();
            await ObrobkaDanychK3();

            return DaneDoWyswietleniaCollection;
        }
        
        internal async Task<bool> ObrobkaDanychK1()
        { 
            await GetDataFromApiNoColors();
            return true;
        }
        internal async Task<bool> ObrobkaDanychK2()
        { 
            await pokoloruj();
            return true;
        }
        internal async Task<bool> ObrobkaDanychK3()
        { 
            await strinifyDates();
            return true;
        }
        internal async Task GetDataFromApiNoColors()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ObservableCollection<DashboardData>>("http://localhost:5000/api/reportedstopage");
                if (response != null)
                {
                    foreach (var item in response)
                    { DaneDoWyswietleniaCollection.Add(item); }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async Task pokoloruj()
        {
            ColorsLegend legenda = new ColorsLegend();
            await legenda.CreateLegend();
            foreach (DashboardData dane in DaneDoWyswietleniaCollection)
            {
                dane.SkutekPostojuKolor = legenda.rozpoznajKategorie(dane.SkutekPostoju);
                dane.PrzyczynaPostojuKolor = legenda.rozpoznajKategorie(dane.PrzyczynaPostoju);
                dane.SczegulyPostojuKolor = legenda.rozpoznajKategorie((bool)dane.CzyPostojZakonczony);
            
            
            }
        
        }
        private async Task strinifyDates()
        {
            foreach (DashboardData dane in DaneDoWyswietleniaCollection)
            {
                dane.CzasRozpoczeciaPostojustring = dane.CzasRozpoczeciaPostoju.ToString("dd.MM.yy HH:mm");
                if (dane.CzyPostojZakonczony)
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
