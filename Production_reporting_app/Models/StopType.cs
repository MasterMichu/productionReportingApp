using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    public class StopTypeCollection
    {

        private readonly HttpClient _httpClient;

        public ObservableCollection<StopsType> items { get; set; }

        public StopTypeCollection()
        {
            _httpClient = new HttpClient();
            items = new ObservableCollection<StopsType>();

        }

        public async Task<ObservableCollection<StopsType>> GetCollection()
        {
            await this.LoadData();
            return items;


        }

        private async Task LoadData()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<StopsType>>("http://localhost:5000/api/stopagetype");
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



        public class StopsType { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        }
    }
}
