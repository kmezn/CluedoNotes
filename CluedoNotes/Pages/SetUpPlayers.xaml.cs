using CluedoNotes.Models;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CluedoNotes.Pages;

public partial class SetUpPlayers : ContentPage
{
	public SetUpPlayers()
	{
		InitializeComponent();
        RefreshPlayers();
	}

    
    private void NewPlayerClicked(object sender, EventArgs e)
    {
        try
        {
            statusMessage.Text = string.Empty;
            App.DBRepo.AddNewPlayer(newPlayer.Text);
            statusMessage.Text = App.DBRepo.StatusMessage;
            newPlayer.Text = string.Empty;
            RefreshPlayers();
        }
        catch (Exception ex)
        {
            statusMessage.Text = ex.Message;
            throw;
        }
    }

    private void RemovePlayerClicked(object sender, EventArgs e)
    {
        statusMessage.Text = string.Empty;
        var button = sender as Button;
        var p = button.BindingContext as Player;
        App.DBRepo.RemovePlayer(p);
        statusMessage.Text = App.DBRepo.StatusMessage;

        RefreshPlayers();
    }


    private void RefreshPlayers()
    {
        List<Player> players = App.DBRepo.GetAllPlayers();
        playerList.ItemsSource = players;
    }
}