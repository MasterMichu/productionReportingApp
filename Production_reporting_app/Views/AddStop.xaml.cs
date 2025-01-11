using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using Production_reporting_app.Models;

namespace Production_reporting_app.Views;

public partial class AddStop : ContentPage
{
    ObservableCollection<linia> itemsbasecollection=new ObservableCollection<linia>();
    ObservableCollection<linia> items = new ObservableCollection<linia>();
    ObservableCollection<machinesInLine> MachinesListBase = new ObservableCollection<machinesInLine>();
    ObservableCollection<machinesInLine> Machineslist = new ObservableCollection<machinesInLine>();
    ObservableCollection<Categories> kategoriePostojow = new ObservableCollection<Categories>();
    ObservableCollection<MachinesBreakdowns> MachinesBreakdownscollection = new ObservableCollection<MachinesBreakdowns>();
    ObservableCollection<MachinesBreakdowns> MachinesBreakdownsBaseCollection = new ObservableCollection<MachinesBreakdowns>();
    List<StopResult> stopresults = new List<StopResult>();
    private string breakdownFilterValue="";
    private AllParamsToSaveBrakdown parametersToSave;
    private LoadingPopup loading;

    public AddStop() 
    {
        InitializeComponent();
        linia linia1 = new linia { Nazwa = "567" };
        linia linia2 = new linia { Nazwa = "568" };
        linia linia3 = new linia { Nazwa = "569" };
        linia linia4 = new linia { Nazwa = "560" };
        linia linia5 = new linia { Nazwa = "571" };
        linia linia6 = new linia { Nazwa = "572" };
        linia linia7 = new linia { Nazwa = "576" };
        linia linia8 = new linia { Nazwa = "590" };
        linia linia9 = new linia { Nazwa = "591" };
        linia linia10 = new linia { Nazwa = "592" };
        StopResult stopresult1 = new StopResult { name = "postój linii" };
        StopResult stopresult2 = new StopResult { name = "problemy z jakoscia" };
        StopResult stopresult3 = new StopResult { name = "mikroprzestoje" };
        StopResult stopresult4 = new StopResult { name = "spowolnienie linii" };
        stopresults.Add(stopresult1);
        stopresults.Add(stopresult2);
        stopresults.Add(stopresult3);
        stopresults.Add(stopresult4);

        itemsbasecollection.Add(linia1);
        itemsbasecollection.Add(linia2);
        itemsbasecollection.Add(linia3);
        itemsbasecollection.Add(linia4);
        itemsbasecollection.Add(linia5);
        itemsbasecollection.Add(linia6);
        itemsbasecollection.Add(linia7);
        itemsbasecollection.Add(linia8);
        itemsbasecollection.Add(linia9);
        itemsbasecollection.Add(linia10);
        machinesInLine maszyna1= new machinesInLine {linia=linia1, Maszyna="gluwna maszyna test" };
        machinesInLine maszyna2 = new machinesInLine { linia = linia2, Maszyna = "sprezarka" };
        machinesInLine maszyna3 = new machinesInLine { linia = linia3, Maszyna = "etykieciarka" };
        machinesInLine maszyna4 = new machinesInLine { linia = linia1, Maszyna = "nalewarka" };
        machinesInLine maszyna5 = new machinesInLine { linia = linia3, Maszyna = "drukarka" };
        machinesInLine maszyna6 = new machinesInLine { linia = linia2, Maszyna = "pralka" };
        machinesInLine maszyna7 = new machinesInLine { linia = linia1, Maszyna = "kosiarka" };
        machinesInLine maszyna8 = new machinesInLine { linia = linia3, Maszyna = "sralka" };
        MachinesListBase.Add(maszyna1);
        MachinesListBase.Add (maszyna2);
        MachinesListBase.Add(maszyna3);
        MachinesListBase.Add((maszyna4));
        MachinesListBase.Add ((maszyna5));
        MachinesListBase.Add(maszyna6);
        MachinesListBase.Add(((maszyna7)));
        MachinesListBase.Add(((maszyna8)));
        
        Categories kategoria1 = new Categories { name = "awaria" };
        Categories kategoria2 = new Categories { name = "regulacja" };
        Categories kategoria3 = new Categories { name = "braki materialow" };
        Categories kategoria4 = new Categories { name = "inne" };
        kategoriePostojow.Add(kategoria1);
        kategoriePostojow.Add(kategoria2 );
        kategoriePostojow.Add(kategoria3);
        kategoriePostojow.Add(kategoria4 );
        MachinesBreakdowns breakdown1 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "awaria nalewarka", nazwakategorii=kategoria1 };
        MachinesBreakdowns breakdown2 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "brak masy" ,nazwakategorii=kategoria3};
        MachinesBreakdowns breakdown3 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "brak nalewaka", nazwakategorii=kategoria1 };
        MachinesBreakdowns breakdown4 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "awaria separatora", nazwakategorii = kategoria1 };
        MachinesBreakdowns breakdown5 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "ustawianie dozy", nazwakategorii = kategoria2 };
        MachinesBreakdowns breakdown6 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "regulacja band", nazwakategorii = kategoria2 };
        MachinesBreakdowns breakdown7 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "awaria elektryczna", nazwakategorii = kategoria1 };
        MachinesBreakdowns breakdown8 = new MachinesBreakdowns { Machine = maszyna1, breakdowndetails = "awaria pneumatyki", nazwakategorii = kategoria1 };
        MachinesBreakdownsBaseCollection.Add(breakdown1);
        MachinesBreakdownsBaseCollection.Add(breakdown2);
        MachinesBreakdownsBaseCollection.Add(breakdown3);
        MachinesBreakdownsBaseCollection.Add(breakdown4);
        MachinesBreakdownsBaseCollection.Add(breakdown5);
        MachinesBreakdownsBaseCollection.Add(breakdown6);
        MachinesBreakdownsBaseCollection.Add(breakdown7);
        MachinesBreakdownsBaseCollection.Add(breakdown8);
        
        populateItemscollection();
        populatemachinescollection();


        podkategoriaMaszyny.ItemsSource = Machineslist;
		podkategoria1.ItemsSource = items;
        radiobuttonslistview.ItemsSource = kategoriePostojow;
        szczegolypostoju.ItemsSource = MachinesBreakdownscollection;
        stopResultCollectionView.ItemsSource = stopresults;
        parametersToSave = new AllParamsToSaveBrakdown();
        loading = new LoadingPopup();

        parametersToSave.paramsCompleteChanged += new ParametryKompletne(ButtonEnableDisable);
        parametersToSave.savingStarted += new SavingStarted(ShowLoadingScreen);
        parametersToSave.savingEnded += new SavingEnded(closeLoadingScreen);
        parametersToSave.savingEnded += new SavingEnded(NavigateToDashboard);
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
        foreach (MachinesBreakdowns breakdown in MachinesBreakdownsBaseCollection)
        {
            MachinesBreakdownscollection.Add(breakdown);
        }
    }

    private void populatemachinescollection()
    {
        foreach (machinesInLine maszyna in MachinesListBase)
        { 
        Machineslist.Add(maszyna);
        }
    }

    private void populateItemscollection()
    {
        foreach (linia item in itemsbasecollection)
        {
        items.Add(item);
        }
    }

    private void setwybranalinia(linia wybranalinia)
    {
        parametersToSave.Linia = wybranalinia;
        filterMachinesChoice();
        podkategoriaMaszyny.IsVisible = true;
        filterBreakdowns();

    }

    private void filterMachinesChoice()
    {
        Machineslist.Clear();
        foreach (machinesInLine machine in MachinesListBase)
        {
            if (machine.linia == parametersToSave.Linia)
            {
                Machineslist.Add(machine);
            }
        }
    }
    private void filterBreakdowns()
    {
        MachinesBreakdownscollection.Clear();
        if (parametersToSave.Maszyna!= null && parametersToSave.PrzyczynaPostoju!= null)
        {
            
            foreach(MachinesBreakdowns breakdown in MachinesBreakdownsBaseCollection)
            {
                if (breakdown.Machine == parametersToSave.Maszyna && breakdown.nazwakategorii == parametersToSave.PrzyczynaPostoju && breakdown.breakdowndetails.Contains(breakdownFilterValue))
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
                items.Clear();
                foreach (linia item in itemsbasecollection)
                {
                    
                    if (item.Nazwa.Contains(input))
                    {
                        items.Add(item);

                    }

                }

            }
            else
            {
                items.Clear();
                foreach (linia item in itemsbasecollection)
                {
                    if (item.Nazwa.Contains(input))
                    {
                        items.Add(item);

                    }

                }
            }


        }
        else {
            items.Clear();
            foreach (linia item in itemsbasecollection)
            {   
                if (item.Nazwa.Contains(input))
                {
                     items.Add(item);

                }

            }
        }
        

    }

    private void podkategoria1_ItemSelected_1(object sender, SelectedItemChangedEventArgs e)
    {
        var wybrany = e.SelectedItem as linia;
        setwybranalinia(wybrany);
        machineBreakdownsFilterEntryClear();


    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var buttoninstance = sender as RadioButton;
        var value = buttoninstance.Value as Categories;
        if (e.Value)
        {
            parametersToSave.PrzyczynaPostoju = value;
        
        
        
        }
        
        filterBreakdowns();
        machineBreakdownsFilterEntryClear();

    }

    private void machineBreakdownsFilterEntryClear()
    {
        BreakdownFilterEntry.Text="";
    }

    private void podkategoriaMaszyny_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        parametersToSave.Maszyna = e.SelectedItem as machinesInLine;
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
        var value = buttoninstance.Value as StopResult;
        if (e.Value)
        {
            parametersToSave.SkutekPostoju = value;



        }

    }
    
    private void szczegolypostoju_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        parametersToSave.SzczegulyPostoju = e.SelectedItem as MachinesBreakdowns;
        
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