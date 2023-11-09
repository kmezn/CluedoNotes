using CluedoNotes.Models;

namespace CluedoNotes.Pages;

public partial class SetUpVictims : ContentPage
{
	public SetUpVictims()
	{
		InitializeComponent();
        RefreshVictims();

    }
    private void NewVictimClicked(object sender, EventArgs e)
    {
        try
        {
            statusMessage.Text = string.Empty;
            App.DBRepo.AddNewVictim(newVictim.Text);
            statusMessage.Text = App.DBRepo.StatusMessage;
            newVictim.Text = string.Empty;
            RefreshVictims();
        }
        catch (Exception ex)
        {
            statusMessage.Text = ex.Message;
            throw;
        }
    }

    private void RemoveVictimClicked(object sender, EventArgs e)
    {
        statusMessage.Text = string.Empty;
        var button = sender as Button;
        var p = button.BindingContext as Victim;
        App.DBRepo.RemoveVictim(p);
        statusMessage.Text = App.DBRepo.StatusMessage;

        RefreshVictims();
    }


    private void RefreshVictims()
    {
        List<Victim> victims = App.DBRepo.GetAllVictims();
        VictimList.ItemsSource = victims;
    }
}