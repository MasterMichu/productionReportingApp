﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    public class MachinesBreakdownsCollection
    {

        private readonly HttpClient _httpClient;
        public ObservableCollection<MachinesBreakdown> items { get; set; }

        public MachinesBreakdownsCollection()
        {
            _httpClient = new HttpClient();
            items = new ObservableCollection<MachinesBreakdown>();

        }

        public async Task<ObservableCollection<MachinesBreakdown>> GetCollection()
        {
            await this.LoadData();
            return items;


        }

        private async Task LoadData()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MachinesBreakdown>>("http://localhost:5000/api/StopageTypeDetails");
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
        public class MachinesBreakdown
        {
            public int Id { get; set; }
            public string SzczegulowyOpisPostoju { get; set; }
            public int MaszynaId { get; set; }
            public int TypPostojuId { get; set; }
            public int SkutekProblemuId { get; set; }
            

        }
    }
    
}
