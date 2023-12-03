using CluedoNotes.Models;

namespace CluedoNotes.Pages;

public partial class SetUpSuspects : ContentPage
{
	public SetUpSuspects()
	{
		InitializeComponent();
        RefreshSuspects();

    }
    private void NewSuspectClicked(object sender, EventArgs e)
    {
        try
        {
            statusMessage.Text = string.Empty;
            //App.DBRepo.AddNewSuspect(newSuspect.Text);
            App.DBRepo.AddNewCard(newSuspect.Text, isSuspect: true);
            statusMessage.Text = App.DBRepo.StatusMessage;
            newSuspect.Text = string.Empty;
            RefreshSuspects();
        }
        catch (Exception ex)
        {
            statusMessage.Text = ex.Message;
            throw;
        }
    }

    private void RemoveSuspectClicked(object sender, EventArgs e)
    {
        statusMessage.Text = string.Empty;
        var button = sender as Button;
        var p = button.BindingContext as Card;
        App.DBRepo.RemoveCard(p);
        statusMessage.Text = App.DBRepo.StatusMessage;

        RefreshSuspects();
    }


    private void RefreshSuspects()
    {
        List<Card> victims = App.DBRepo.GetAllCards(justSuspects: true);
        SuspectList.ItemsSource = victims;
    }
}