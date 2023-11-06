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
            App.PlayerRepo.AddNewPlayer(newPlayer.Text);
            statusMessage.Text = App.PlayerRepo.StatusMessage;
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
        App.PlayerRepo.RemovePlayer(p);
        statusMessage.Text = App.PlayerRepo.StatusMessage;

        RefreshPlayers();
    }


    private void RefreshPlayers()
    {
        List<Player> players = App.PlayerRepo.GetAllPlayers();
        playerList.ItemsSource = players;
    }
}