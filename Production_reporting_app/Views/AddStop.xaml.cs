using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using Production_reporting_app.Models;
using static Production_reporting_app.Models.ProductionLinesCollection;
using static Production_reporting_app.Models.ProductionMachinesCollection;
using static Production_reporting_app.Models.StopTypeCollection;
using static Production_reporting_app.Models.MachinesBreakdownsCollection;
using static Production_reporting_app.Models.StopResultCollection;

namespace Production_reporting_app.Views;

public partial class AddStop : ContentPage
{
    ObservableCollection<ProductionLines> Linesbasecollection=new ObservableCollection<ProductionLines>();
    ObservableCollection<ProductionLines> Lines = new ObservableCollection<ProductionLines>();
    ObservableCollection<ProductionMachines> MachinesBaseCollection = new ObservableCollection<ProductionMachines>();
    ObservableCollection<ProductionMachines> MachinesCollection = new ObservableCollection<ProductionMachines>();
    ObservableCollection<StopsType> kategoriePostojow = new ObservableCollection<StopsType>();
    ObservableCollection<MachinesBreakdown> MachinesBreakdownscollection = new ObservableCollection<MachinesBreakdown>();
    ObservableCollection<MachinesBreakdown> MachinesBreakdownsBaseCollection = new ObservableCollection<MachinesBreakdown>();
    List<StopsResult> stopresults = new List<StopsResult>();
    private string breakdownFilterValue="";
    private AllParamsToSaveBrakdown parametersToSave;
    private LoadingPopup loading;
    private bool _staticDataLoaded=false;

    public AddStop() 
    {
        InitializeComponent();
        

        
     
        
        
        
       

        
        
        
        
        
        

        
        //radiobuttonslistview.ItemsSource = kategoriePostojow;
        
        
        parametersToSave = new AllParamsToSaveBrakdown();
        loading = new LoadingPopup();

        parametersToSave.paramsCompleteChanged += new ParametryKompletne(ButtonEnableDisable);
        parametersToSave.savingStarted += new SavingStarted(ShowLoadingScreen);
        parametersToSave.savingEnded += new SavingEnded(closeLoadingScreen);
        parametersToSave.savingEnded += new SavingEnded(NavigateToDashboard);
    }
    public async Task  LoadAllDataForContentPage()
    {
        var linesFromApi = new ProductionLinesCollection();
        var machinesFromApi = new ProductionMachinesCollection();
        var stopsType=new StopTypeCollection();
        var machinesBreakdownsFromApi = new MachinesBreakdownsCollection();
        var stopsResultFromApi= new StopResultCollection();
        stopresults = await stopsResultFromApi.GetCollection();
        kategoriePostojow= await stopsType.GetCollection();
        MachinesBaseCollection = await machinesFromApi.GetCollection();
        Linesbasecollection = await linesFromApi.GetCollection();
        MachinesBreakdownsBaseCollection = await machinesBreakdownsFromApi.GetCollection();
        radiobuttonslistview.ItemsSource = kategoriePostojow;
        podkategoriaMaszyny.ItemsSource = MachinesCollection;
        podkategoria1.ItemsSource = Lines;
        szczegolypostoju.ItemsSource = MachinesBreakdownscollection;
        stopResultCollectionView.ItemsSource = stopresults;
    }
    protected override async void OnAppearing() 
    { 
        base.OnAppearing();
        if (!_staticDataLoaded) 
        { 
            await InitializeAsync(); 
        }
        
     
    }
    private async Task InitializeAsync() 
    { 
        await LoadAllDataForContentPage();
        populateItemscollection();
        populatemachinescollection();
        _staticDataLoaded= true;
    }

    private async void NavigateToDashboard()
    {
        await Shell.Current.GoToAsync("//Dashboard");    
    }

    private void closeLoadingScreen()
    {
        loading.Close();
    }

    private void ShowLoadingScreen()
    {
        this.ShowPopupAsync(loading);
    }

    private void ButtonEnableDisable(bool czyparametrykompletne)
    {
        SaveButton.IsEnabled = czyparametrykompletne; 
    }

    private void populatemachinesbreakdownscollection()
    {
        foreach (MachinesBreakdown breakdown in MachinesBreakdownsBaseCollection)
        {
            MachinesBreakdownscollection.Add(breakdown);
        }
    }

    private void populatemachinescollection()
    {
        foreach (ProductionMachines maszyna in MachinesBaseCollection)
        { 
        MachinesCollection.Add(maszyna);
        }
    }

    private void populateItemscollection()
    {
        foreach (ProductionLines item in Linesbasecollection)
        {
        Lines.Add(item);
        }
    }

    private void setwybranalinia(ProductionLines wybranalinia)
    {
        parametersToSave.LiniaID = wybranalinia.Id;
        filterMachinesChoice();
        podkategoriaMaszyny.IsVisible = true;
        filterBreakdowns();

    }

    private void filterMachinesChoice()
    {
        MachinesCollection.Clear();
       
        foreach (ProductionMachines machine in MachinesBaseCollection)
        {
            if (machine.ProductionLineId == parametersToSave.LiniaID)
            {
                MachinesCollection.Add(machine);
            }
        }
    }
    private void filterBreakdowns()
    {
        MachinesBreakdownscollection.Clear();
        if (parametersToSave.MaszynaID!= 0 && parametersToSave.PrzyczynaPostojuID!= 0)
        {
            
            foreach(MachinesBreakdown breakdown in MachinesBreakdownsBaseCollection)
            {
                if (breakdown.MaszynaId == parametersToSave.MaszynaID && breakdown.TypPostojuId == parametersToSave.PrzyczynaPostojuID && breakdown.SzczegulowyOpisPostoju.Contains(breakdownFilterValue))
                { 
                MachinesBreakdownscollection.Add(breakdown);                              
                }
            }
        }
    }

    /// <summary>
    /// przy zmianie wartosci entry w kategorii maszyna uruchamia filtrowanie
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var jest = e.NewTextValue;
        var bylo = e.OldTextValue;
        Console.WriteLine("wpisano "+jest);
        FilterCollectionOfItems(jest, bylo);
        
        
        
    }

    /// <summary>
    /// funkcaj przyjmuje argumenty jest i by³o dla filtrowania listy maszyn na podstawie wpisu w entry
    /// </summary>
    /// <param name="input"> jest, bylo </param>
    /// <param name="previousinput"> void </param>
    private void FilterCollectionOfItems(string input, string previousinput)
    {

        if (previousinput != null)
        {
            if (input.Length > previousinput.Length)
            {
                Lines.Clear();
                foreach (ProductionLines item in Linesbasecollection)
                {
                    
                    if (item.Name.Contains(input))
                    {
                        Lines.Add(item);

                    }

                }

            }
            else
            {
                Lines.Clear();
                foreach (ProductionLines item in Linesbasecollection)
                {
                    if (item.Name.Contains(input))
                    {
                        Lines.Add(item);

                    }

                }
            }


        }
        else {
            Lines.Clear();
            foreach (ProductionLines item in Linesbasecollection)
            {   
                if (item.Name.Contains(input))
                {
                     Lines.Add(item);

                }

            }
        }
        

    }

    private void podkategoria1_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
    {
        var wybrany = e.SelectedItem as ProductionLines;
        setwybranalinia(wybrany);
        machineBreakdownsFilterEntryClear();
        
        


    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var buttoninstance = sender as RadioButton;
        var value = buttoninstance.Value as StopsType;
        if (e.Value)
        {
            parametersToSave.PrzyczynaPostojuID = value.Id;
        
        
        
        }
        
        filterBreakdowns();
        machineBreakdownsFilterEntryClear();

    }

    private void machineBreakdownsFilterEntryClear()
    {
        BreakdownFilterEntry.Text="";
    }

    private void podkategoriaMaszyny_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var maszyna=e.Item as ProductionMachines;
        parametersToSave.MaszynaID = maszyna.Id;
        filterBreakdowns();
        machineBreakdownsFilterEntryClear();
    }

    private void Entry_TextChanged_1(object sender, TextChangedEventArgs e)
    {
        var jest = e.NewTextValue;
        var bylo = e.OldTextValue;
        breakdownFilterValue = jest;
        filterBreakdowns();
    }

    private void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
    {
        var buttoninstance = sender as RadioButton;
        var value = buttoninstance.Value as StopsResult;
        if (e.Value)
        {
            parametersToSave.SkutekPostojuID = value.Id;



        }

    }
    
    private void szczegolypostoju_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selecteditem=e.SelectedItem as MachinesBreakdown;
        parametersToSave.SzczegulyPostojuID = selecteditem.Id; 
        
    }

    private void WystapienieDzisCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            DzienWystapieniaPicker.IsEnabled = false;
            ustawDzienWystapieniaAwari();
        }
        else { DzienWystapieniaPicker.IsEnabled = true; }

    }

    private void ustawDzienWystapieniaAwari()
    {
        parametersToSave.dzienWystapieniaAwarii= DateTime.Today.Date;
    }
    private void ustawDzienWystapieniaAwari(DateTime data)
    {
        parametersToSave.dzienWystapieniaAwarii = data.Date ;
    }

    private void ZakonczenieDzisCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            DzienZakonczeniaPicker.IsEnabled = false;
            ustawDzienZakonczeniaAwari();
        }
        else { DzienZakonczeniaPicker.IsEnabled = true; }

    }

    private void ustawDzienZakonczeniaAwari()
    {
        parametersToSave.dzienZakonczeniaAwarii = DateTime.Today.Date;
    }
    private void ustawDzienZakonczeniaAwari(DateTime data)
    {
        parametersToSave.dzienZakonczeniaAwarii = data.Date;
    }

    private void DzienWystapieniaPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        ustawDzienWystapieniaAwari(e.NewDate);
    }

    private void DzienZakonczeniaPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        ustawDzienZakonczeniaAwari(e.NewDate);
    }

    private void TimePicker_TimeSelected(object sender, TimeChangedEventArgs e)
    {
         parametersToSave.godzinaZakonczeniaAwarii = e.NewTime;
        
    }

    private void TimePicker_TimeSelected_1(object sender, TimeChangedEventArgs e)
    {
        parametersToSave.godzinaWystapieniaAwarii = e.NewTime;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        parametersToSave.saveParametersToFile();
    }

   
}