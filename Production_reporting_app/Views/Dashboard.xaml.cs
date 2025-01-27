using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Production_reporting_app.Models;
using Production_reporting_app.Views;

namespace Production_reporting_app.Views;

public partial class Dashboard : ContentPage
{
    public DataCollectionsForDashboardView dane;
    private bool DaneWczytaneFlag;
    private Popup editpopup;
    private Popup loadingScreen;
    public ICommand EdytujCommand { get; private set; }
    

    public Dashboard()
    {
        
        loadingScreen=new LoadingPopup();
        InitializeComponent();
        
        
        DelegateContainer.OrderShowLoadingScreen += new DelegateContainer.ShowLoadingScreen(showLoadingScreen);
        DelegateContainer.OrderCloseLoadingScreen += new DelegateContainer.CloseLoadingScreen(ReloadDataAfterUpdate);
        DelegateContainer.OrderCloseLoadingScreen += new DelegateContainer.CloseLoadingScreen(closeLoadingScreen);
        DelegateContainer.OnClosePopupSendData += new DelegateContainer.ClosePopupSendData(UruchomLoadingPopupZapiszDane);
        DelegateContainer.OnClosePopupDeleteData += new DelegateContainer.ClosePopupDeleteData(UruchomLoadingPopupUsunDane);
    }
    protected override async void OnAppearing() 
    {
        await ContentPage_NavigatedToHandler();
    }
    private void closeLoadingScreen()
    {
        
        loadingScreen.Close();
    }
    private async void ReloadDataAfterUpdate()
    {
        DaneWczytaneFlag = false;
        await ContentPage_NavigatedToHandler();
    }

    private void showLoadingScreen()
    {
        this.ShowPopup(loadingScreen);
    }
    private async Task ContentPage_NavigatedToHandler()
    {
        if (!DaneWczytaneFlag)
        {
            dane = new DataCollectionsForDashboardView();
            DashboardStopsCollectionView.ItemsSource =await  dane.wczytajDaneZPliku();
            DaneWczytaneFlag = true;
        }

    }

    private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
       await ContentPage_NavigatedToHandler();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        DaneWczytaneFlag = false;
        dane.DaneDoWyswietleniaCollection.Clear();
    }

    private void EditButton_Clicked(DashboardData e)
    {
        


    }
    [RelayCommand]
    public void Edit(DashboardData daneDoEdycji)
    {
        editpopup = new EditPopup(daneDoEdycji);
        //DelegateContainer.OnClosePopupSendData += new DelegateContainer.ClosePopupSendData(UruchomLoadingPopupZapiszDane);
        this.ShowPopup(editpopup);
        




}

    private void UruchomLoadingPopupZapiszDane(DashboardData data)
    {
        
        
        
        data.saveData();
        editpopup.Close();

    }
    private void UruchomLoadingPopupUsunDane(DashboardData data)
    {



        data.deleteData();
        editpopup.Close();

    }
}





