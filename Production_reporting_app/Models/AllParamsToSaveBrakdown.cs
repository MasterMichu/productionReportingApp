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
        private linia _Linia;
        private machinesInLine _Maszyna;
        private Categories _PrzyczynaPostoju;
        private StopResult _SkutekPostoju;
        private MachinesBreakdowns _SzczegulyPostoju;
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
        _Linia=null;
        _Maszyna = null;
        _PrzyczynaPostoju = null;
        _SkutekPostoju = null;
        _SzczegulyPostoju = null;
        _dzienWystapieniaAwarii = DateTime.UnixEpoch;
        _dzienZakonczeniaAwarii = DateTime.UnixEpoch;
        _godzinaWystapieniaAwarii =TimeSpan.MaxValue;
        _godzinaZakonczeniaAwarii =TimeSpan.MaxValue;
        _StrataProdukcji = 0;
        _godzinaWystapieniaAwariiUstawiona=false;
        _godzinaZakonczeniaAwariiUstawiona=false;
     

        }
        
        public linia Linia
        {
            get { return _Linia; }
            set {
                _Maszyna = null;
                _SzczegulyPostoju = null;
                _Linia = value;
                CheckComletionOfParameters();
            }
        
        
        }
        public machinesInLine Maszyna
        {
            get { return _Maszyna; }
            set
            {
                _SzczegulyPostoju = null;
                _Maszyna = value;
                CheckComletionOfParameters();
            }


        }
        public Categories PrzyczynaPostoju
        {
            get { return _PrzyczynaPostoju; }
            set
            {
                _SzczegulyPostoju = null;
                _PrzyczynaPostoju = value;
                CheckComletionOfParameters();
            }


        }
        public StopResult SkutekPostoju
        {
            get { return _SkutekPostoju; }
            set
            {
                _SkutekPostoju = value;
                CheckComletionOfParameters();
            }


        }
        public MachinesBreakdowns SzczegulyPostoju
        {
            get { return _SzczegulyPostoju; }
            set
            {
                _SzczegulyPostoju = value;
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
            if (Linia != null &&
                Maszyna != null &&
                PrzyczynaPostoju != null &&
                SkutekPostoju != null &&
                SzczegulyPostoju != null &&
                dzienWystapieniaAwarii != DateTime.UnixEpoch &&
                dzienZakonczeniaAwarii != DateTime.UnixEpoch &&
                _godzinaWystapieniaAwariiUstawiona &&
                _godzinaZakonczeniaAwariiUstawiona
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
            DaneDoZapisu.SaveData();
            
            await Task.Delay(2000);
        savingEnded();
        
        
        }

        

        internal class DataToSaveJson
        {
            public linia Linia;
            public machinesInLine Maszyna;
            public Categories PrzyczynaPostoju;
            public StopResult SkutekPostoju;
            public MachinesBreakdowns SzczegulyPostoju;
            public bool CzyPostojZakonczony;
            public DateTime CzasRozpoczeciaPostoju;
            public DateTime CzasZakonczeniaPostoju;
            public int CzasTrwania;
            internal DataToSaveJson(AllParamsToSaveBrakdown classobject)
            {
                Linia = classobject.Linia;
                Maszyna = classobject.Maszyna;
                PrzyczynaPostoju = classobject.PrzyczynaPostoju;
                SkutekPostoju = classobject.SkutekPostoju;
                SzczegulyPostoju = classobject.SzczegulyPostoju;
                if (classobject._godzinaWystapieniaAwariiUstawiona && classobject._godzinaZakonczeniaAwariiUstawiona)
                {
                    CzyPostojZakonczony = true;

                }
                else { CzyPostojZakonczony = false; }
                CzasRozpoczeciaPostoju = classobject.dzienWystapieniaAwarii.AddTicks(classobject.godzinaWystapieniaAwarii.Ticks);
                CzasZakonczeniaPostoju = classobject.dzienZakonczeniaAwarii.AddTicks(classobject.godzinaZakonczeniaAwarii.Ticks);
                CzasTrwania = classobject.StrataProdukcji;

            }
            internal void SaveData()
            {
                var options = new JsonSerializerOptions
                {
                    IncludeFields = true // Include private fields in the serialization
                };
                string JsonString=JsonSerializer.Serialize(this,options);
                string path = "example.txt";

                using (StreamWriter sw = new StreamWriter(path))
                {

                    sw.WriteLine(JsonString);
                }
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
