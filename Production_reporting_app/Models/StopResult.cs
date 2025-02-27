﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Production_reporting_app.Models
{
    public class StopResultCollection
    {
        private readonly HttpClient _httpClient;

        public List<StopsResult> items { get; set; }

        public StopResultCollection()
        {
            _httpClient = new HttpClient();
            items = new List<StopsResult>();

        }

        public async Task<List<StopsResult>> GetCollection()
        {
            await this.LoadData();
            return items;


        }

        private async Task LoadData()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<StopsResult>>("http://localhost:5000/api/problemresult");
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



        public class StopsResult
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ColorCode { get; set; }
        }
    }
}

