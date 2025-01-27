using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    public class ColorsLegend
    {
        List<KoloryPrzypisanie> colors {  get; set; }
        private readonly HttpClient _httpClient;
       
        public ColorsLegend() 
        {
            
            _httpClient = new HttpClient();
            colors = new List<KoloryPrzypisanie>();
            
        }

        

        public async Task CreateLegend()
        {
            await LoadData();
     


        }

        private async Task LoadData()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<KoloryPrzypisanie>>("http://localhost:5000/api/stopagetype");
                if (response != null)
                {
                    foreach (var item in response)
                    { colors.Add(item); }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public class KoloryPrzypisanie
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ColorCode { get; set; }
        }
        public Color rozpoznajKategorie(string textdoRozpoznania)
        {
            foreach (KoloryPrzypisanie kolor in colors)
            {
                if (textdoRozpoznania==kolor.Name)
                {
                    
                    return  Color.FromArgb(kolor.ColorCode); 
                }
            
            
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
