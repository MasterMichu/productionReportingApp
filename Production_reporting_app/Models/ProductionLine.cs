using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Windows.Input;
using System.Net.Http.Json;


namespace Production_reporting_app.Models
{
    public class ProductionLinesCollection
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<ProductionLines> Items { get; set; }
        
        public ProductionLinesCollection() 
        {
            _httpClient = new HttpClient(); 
            Items = new ObservableCollection<ProductionLines>();
            


        }
        public async Task<ObservableCollection<ProductionLines>> GetCollection()
        {
            await this.LoadData();
            return Items;
        
        }
        private async Task LoadData()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProductionLines>>("http://localhost:5000/api/line");
                if (response != null)
                {
                    foreach (var item in response)
                    { Items.Add(item); }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
        public class ProductionLines
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }
    }

}
