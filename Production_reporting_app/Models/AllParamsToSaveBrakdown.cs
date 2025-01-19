using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;




namespace Production_reporting_app.Models
{
    public class AllParamsToSaveBrakdown
    {
        private int _LiniaID;
        private int _MaszynaID;
        private int _PrzyczynaPostojuID;
        private int _SkutekPostojuID;
        private int _SzczegulyPostojuID;
        private DateTime _dzienWystapieniaAwarii;
        private DateTime _dzienZakonczeniaAwarii;
        private TimeSpan _godzinaWystapieniaAwarii;
        private TimeSpan _godzinaZakonczeniaAwarii;
        private bool _godzinaWystapieniaAwariiUstawiona;
        private bool _godzinaZakonczeniaAwariiUstawiona;
        private int _StrataProdukcji;
        public bool ParamsAreComplete;
  
        public AllParamsToSaveBrakdown() 
        {
        _LiniaID=0;
        _MaszynaID = 0;
        _PrzyczynaPostojuID = 0;
        _SkutekPostojuID = 0;
        _SzczegulyPostojuID = 0;
        _dzienWystapieniaAwarii = DateTime.UnixEpoch;
        _dzienZakonczeniaAwarii = DateTime.UnixEpoch;
        _godzinaWystapieniaAwarii =TimeSpan.MaxValue;
        _godzinaZakonczeniaAwarii =TimeSpan.MaxValue;
        _StrataProdukcji = 0;
        _godzinaWystapieniaAwariiUstawiona=false;
        _godzinaZakonczeniaAwariiUstawiona=false;
     

        }
        
        public int LiniaID
        {
            get { return _LiniaID; }
            set {
                _MaszynaID = 0;
                _SzczegulyPostojuID = 0;
                _LiniaID = value;
                CheckComletionOfParameters();
            }
        
        
        }
        public int MaszynaID
        {
            get { return _MaszynaID; }
            set
            {
                _SzczegulyPostojuID = 0;
                _MaszynaID = value;
                CheckComletionOfParameters();
            }


        }
        public int PrzyczynaPostojuID
        {
            get { return _PrzyczynaPostojuID; }
            set
            {
                _SzczegulyPostojuID = 0;
                _PrzyczynaPostojuID = value;
                CheckComletionOfParameters();
            }


        }
        public int SkutekPostojuID
        {
            get { return _SkutekPostojuID; }
            set
            {
                _SkutekPostojuID = value;
                CheckComletionOfParameters();
            }


        }
        public int SzczegulyPostojuID
        {
            get { return _SzczegulyPostojuID; }
            set
            {
                _SzczegulyPostojuID = value;
                CheckComletionOfParameters();
            }


        }
        public DateTime dzienWystapieniaAwarii
        {
            get { return _dzienWystapieniaAwarii; }
            set
            {
                _dzienWystapieniaAwarii = value;
                CheckComletionOfParameters();
            }


        }
        public DateTime dzienZakonczeniaAwarii
        {
            get { return _dzienZakonczeniaAwarii; }
            set
            {
                _dzienZakonczeniaAwarii = value;
                CheckComletionOfParameters();
            }


        }
        public TimeSpan godzinaWystapieniaAwarii
        {
            get { return _godzinaWystapieniaAwarii; }
            set
            {
                _godzinaWystapieniaAwarii = value;
                _godzinaWystapieniaAwariiUstawiona = true;
                
                CheckComletionOfParameters();
            }


        }
        public TimeSpan godzinaZakonczeniaAwarii
        {
            get { return _godzinaZakonczeniaAwarii; }
            set
            {
                _godzinaZakonczeniaAwarii = value;
                _godzinaZakonczeniaAwariiUstawiona = true;
                CheckComletionOfParameters();
            }


        }
        public int StrataProdukcji
        {
            get { return _StrataProdukcji; }
            set { _StrataProdukcji = value; }
        
        
        }
        private void CheckComletionOfParameters()
        {
            if (LiniaID != 0 &&
                MaszynaID != 0 &&
                PrzyczynaPostojuID != 0 &&
                SkutekPostojuID != 0 &&
                SzczegulyPostojuID != null &&
                dzienWystapieniaAwarii != DateTime.UnixEpoch &&
                _godzinaWystapieniaAwariiUstawiona
                )
            { 
            ParamsAreComplete = true;
     
            
            }
            else
            { ParamsAreComplete = false; }
            paramsCompleteChanged(ParamsAreComplete);


        }
        public async void saveParametersToFile()
        {
            savingStarted();
            DataToSaveJson DaneDoZapisu = new DataToSaveJson(this);
            await DaneDoZapisu.SaveData();
            
            
        savingEnded();
        
        
        }

        

        internal class DataToSaveJson
        {

            public int SzczegulyPostojuID;
            public bool CzyZakonczony;
            public DateTime CzasRozpoczeciaPostoju;
            public DateTime CzasZakonczeniaPostoju;
            public int StrataProdukcji;
            internal DataToSaveJson(AllParamsToSaveBrakdown classobject)
            {
                
                SzczegulyPostojuID = classobject.SzczegulyPostojuID;
                if (classobject._godzinaZakonczeniaAwariiUstawiona && classobject.dzienZakonczeniaAwarii!=DateTime.UnixEpoch)
                {
                    CzasZakonczeniaPostoju = classobject.dzienZakonczeniaAwarii.AddTicks(classobject.godzinaZakonczeniaAwarii.Ticks);
                    CzyZakonczony = true;

                }
                else 
                { 
                    CzyZakonczony = false; 
                }
                CzasRozpoczeciaPostoju = classobject.dzienWystapieniaAwarii.AddTicks(classobject.godzinaWystapieniaAwarii.Ticks);
                
                StrataProdukcji = classobject.StrataProdukcji;

            }
            internal async Task SaveData()
            {
                var options = new JsonSerializerOptions
                {
                    IncludeFields = true // Include private fields in the serialization
                };
                string JsonString=JsonSerializer.Serialize(this,options);
                using var httpClient = new HttpClient();
                var content = new StringContent(JsonString, System.Text.Encoding.UTF8, "application/json"); try
                { 
                    // Send the POST request
                    var response = await httpClient.PostAsync("http://localhost:50000/api/reportedstopage", content); 
                    // Ensure the request was successful
                    response.EnsureSuccessStatusCode(); 
                    Console.WriteLine("Data saved successfully."); 
                } catch (Exception ex) 
                { 
                    // Handle exceptions
                    Console.WriteLine($"Error: {ex.Message}"); }
                }
        }
        public event ParametryKompletne paramsCompleteChanged;
        public event SavingStarted savingStarted;
        public event SavingEnded savingEnded;
        


    }
    
    public delegate void ParametryKompletne(bool czyparametrykompletne);
    public delegate void SavingStarted();
    public delegate void SavingEnded();
   
}
