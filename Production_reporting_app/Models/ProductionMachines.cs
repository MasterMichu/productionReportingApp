using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    
    public class ProductionMachinesCollection
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection <ProductionMachines> items {  get; set; }

        public ProductionMachinesCollection()
        { 
            _httpClient=new HttpClient();
            items= new ObservableCollection<ProductionMachines>();
        
        }

        public async Task<ObservableCollection<ProductionMachines>> GetCollection()
        {
            await this.LoadData();
            return items;
        
        
        }

        private async Task LoadData()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProductionMachines>>("http://localhost:5000/api/productionmachine");
                if (response != null)
                {
                    foreach (var item in response)
                    { items.Add(item); }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public class ProductionMachines 
        {
        public int Id {get; set;}
        public string Name { get; set; }
        public int ProductionLineId { get; set;}
        }

    }
}
