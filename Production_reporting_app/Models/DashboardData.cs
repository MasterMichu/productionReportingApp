using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Production_reporting_app.Models
{
    public partial class DashboardData
    {
        public string Linia { get; set; }
        public string Maszyna { get; set; }
        public string PrzyczynaPostoju { get; set; }
        public Color PrzyczynaPostojuKolor { get; set; }
        public string SkutekPostoju { get; set; }
        public Color SkutekPostojuKolor { get; set; }
        public string SzczegulyPostoju { get; set; }
        public Color SczegulyPostojuKolor { get; set; }
        public int SczegulyPostojuId { get; set; }
        public bool CzyPostojZakonczony { get; set; }
        public DateTime CzasRozpoczeciaPostoju { get; set; }
        public string CzasRozpoczeciaPostojustring { get; set; }
        public DateTime CzasZakonczeniaPostoju { get; set; }
        public string CzasZakonczeniaPostojustring { get; set; }
        public int StrataProdukcji { get; set; }
        public int id { get; set; }

        public EditDelegate editDelegate;

        public DeleteDelegate deleteDelegate;

        internal async void saveData()
        {
            DelegateContainer.RaiseOrderShowLoadingScreen();
            //tu wywolamy metode zapisujaca dane
            await SendUpdateRequest();
            DelegateContainer.RaiseOrderCloseLoadingScreen();

        }
        internal async void deleteData()
        {
            DelegateContainer.RaiseOrderShowLoadingScreen();
            //tu wywolamy metode zapisujaca dane
            await SendDeleteRequest();
            DelegateContainer.RaiseOrderCloseLoadingScreen();

        }

        private async Task SendDeleteRequest()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage message = await httpClient.DeleteAsync($"http://localhost:5000/api/reportedstopage/Delete/{this.id}");
            message.EnsureSuccessStatusCode();
        }

        internal async Task SendUpdateRequest()
        { 
        HttpClient httpClient = new HttpClient();
            DashboardDataUpdateDTO dashboardDataUpdateDTO=new DashboardDataUpdateDTO(this);
            HttpResponseMessage message = await httpClient.PutAsJsonAsync("http://localhost:5000/api/reportedstopage", dashboardDataUpdateDTO);
            message.EnsureSuccessStatusCode();
        
        }
        internal class DashboardDataUpdateDTO 
        {
            public int Id { get; set; }
            public int SzczegulyPostojuId { get; set; }
            public int StrataProdukcji { get; set; }
            public DateTime CzasRozpoczecia { get; set; }
            public DateTime CzasZakonczenia { get; set; }
            public bool CzyZakonczony { get; set; }
            public DashboardDataUpdateDTO(DashboardData dashboardData) 
            { 
             Id=dashboardData.id;
                SzczegulyPostojuId = dashboardData.SczegulyPostojuId;
                StrataProdukcji=dashboardData.StrataProdukcji;
                CzasRozpoczecia=dashboardData.CzasRozpoczeciaPostoju;
                CzasZakonczenia = dashboardData.CzasZakonczeniaPostoju;
                CzyZakonczony = dashboardData.CzyPostojZakonczony;
            }

        }

    }
    public delegate void EditDelegate();
    public delegate void DeleteDelegate();
}
