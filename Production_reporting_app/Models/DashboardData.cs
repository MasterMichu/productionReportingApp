using System;
using System.Collections.Generic;
using System.Linq;
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
            await Task.Delay(2000);
            DelegateContainer.RaiseOrderCloseLoadingScreen();

        }
    }
    public delegate void EditDelegate();
    public delegate void DeleteDelegate();
}
