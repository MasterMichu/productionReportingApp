using CommunityToolkit.Maui.Views;
using Production_reporting_app.Models;

namespace Production_reporting_app.Views;

public partial class EditPopup :Popup
{
	public DashboardData daneWejsciowe;
    public DashboardData daneWyjsciowe;
    
    public EditPopup(DashboardData dane)
	{
		daneWejsciowe = dane;
        daneWyjsciowe= dane;
		this.BindingContext = dane;
		

        InitializeComponent();
		SetStartDataOfPickers();
        daneWejsciowe.editDelegate += new EditDelegate(verifyDataShowInfoNSave);
        
        





	}

    private void verifyDataShowInfoNSave()
    {

        if (this.dataRozpoczeciaPicker.Date == this.dataZakonczeniaPicker.Date && this.czasRozpoczeciaPicker.Time >= this.czasZakonczeniaPicker.Time)
        {
            this.labelKoniec.Text = "Wpisz Prawid³ow¹ wartoœæ";
            this.labelKoniec.TextColor = Colors.Red;
        }
        else 
        { ////dopisz dane z formularza do danych wyjsciowych
            daneWyjsciowe.CzasRozpoczeciaPostoju = dataRozpoczeciaPicker.Date.AddTicks(czasRozpoczeciaPicker.Time.Ticks);
            daneWyjsciowe.CzasZakonczeniaPostoju = dataZakonczeniaPicker.Date.AddTicks(czasZakonczeniaPicker.Time.Ticks);
            daneWejsciowe.CzyPostojZakonczony = true;
        
            DelegateContainer.RaiseOnClosePopupSendData(this.daneWyjsciowe);
        }

    }

    private void SetStartDataOfPickers()
	{
		
		var dzienRozpoczecia = this.daneWejsciowe.CzasRozpoczeciaPostoju.Date;
        var dzienZakonczenia = this.daneWejsciowe.CzasRozpoczeciaPostoju.Date;
        var czasrozpoczecia = this.daneWejsciowe.CzasRozpoczeciaPostoju.TimeOfDay;
        var czaszakonczenia = this.daneWejsciowe.CzasZakonczeniaPostoju.TimeOfDay;
		if (dzienRozpoczecia < dzienZakonczenia)
		{
            this.dataRozpoczeciaPicker.Date = dzienRozpoczecia;
            this.dataZakonczeniaPicker.Date = dzienZakonczenia;
            this.czasRozpoczeciaPicker.Time = czasrozpoczecia;
            this.czasZakonczeniaPicker.Time = czaszakonczenia;

        }
		else if (dzienRozpoczecia == dzienZakonczenia)
		{
            this.dataRozpoczeciaPicker.Date = dzienRozpoczecia;
            this.dataZakonczeniaPicker.Date = dzienZakonczenia;
            this.czasRozpoczeciaPicker.Time = czasrozpoczecia;
            this.czasZakonczeniaPicker.Time = czaszakonczenia;


        }
		else 
		{ 
		
		
		}
	    this.dataZakonczeniaPicker.MaximumDate = DateTime.Now.Date;
        this.dataRozpoczeciaPicker.MaximumDate = DateTime.Now.Date;






    }

    private void czasRozpoczeciaPicker_TimeSelected(object sender, TimeChangedEventArgs e)
    {

    }

    private void dataRozpoczeciaPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        //do dodania ograniczenie na picklerze
        //this.dataZakonczeniaPicker.MinimumDate = e.NewDate;


    }


    private void dataZakonczeniaPicker_DateSelected(object sender, DateChangedEventArgs e)
    {

    }

    private void czasZakonczeniaPicker_TimeSelected(object sender, TimeChangedEventArgs e)
    {

    }

    private void saveButton_Clicked(object sender, EventArgs e)
    {
        
        this.daneWejsciowe.editDelegate();
        
    }

    private void DeleteIcon_Clicked(object sender, EventArgs e)
    {
        DelegateContainer.RaiseOnClosePopupDeleteData(this.daneWyjsciowe);
    }

    private void ExitButton_Clicked_1(object sender, EventArgs e)
    {
        this.Close();
    }
    
    
}

