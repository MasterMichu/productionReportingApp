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
        dane = new DataCollectionsForDashboardView();
        dane.wczytajDaneZPliku();
        loadingScreen=new LoadingPopup();
        InitializeComponent();
        DaneWczytaneFlag = true;
        
        DashboardStopsCollectionView.ItemsSource = dane.DaneDoWyswietleniaCollection;
        DelegateContainer.OrderShowLoadingScreen += new DelegateContainer.ShowLoadingScreen(showLoadingScreen);
        DelegateContainer.OrderCloseLoadingScreen += new DelegateContainer.CloseLoadingScreen(closeLoadingScreen);
        //this.ShowPopup(editpopup);






    }

    private void closeLoadingScreen()
    {
        loadingScreen.Close();
    }

    private void showLoadingScreen()
    {
        this.ShowPopup(loadingScreen);
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (!DaneWczytaneFlag)
        {
            dane.wczytajDaneZPliku();
            DaneWczytaneFlag = true;
        }
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        DaneWczytaneFlag = false;
    }

    private void EditButton_Clicked(DashboardData e)
    {
        


    }
    [RelayCommand]
    public void Edit(DashboardData daneDoEdycji)
    {
        editpopup = new EditPopup(daneDoEdycji);
        DelegateContainer.OnClosePopupSendData += new DelegateContainer.ClosePopupSendData(UruchomLoadingPopupZapiszDane);
        this.ShowPopup(editpopup);
        




}

    private void UruchomLoadingPopupZapiszDane(DashboardData data)
    {
        editpopup.Close();
        data.saveData();
        

    }
}





