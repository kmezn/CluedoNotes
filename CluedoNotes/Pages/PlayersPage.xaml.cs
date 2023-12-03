using CluedoNotes.Models;

namespace CluedoNotes.Pages;

public partial class PlayersPage : ContentPage
{
    public PlayersPage()
    {
        InitializeComponent();
        RefreshPlayers();

    }

    private void RefreshPlayers()
    {
        List<Player> players = App.DBRepo.GetAllPlayers();
        playerList.ItemsSource = players;
    }

    private void LogCardSeen(object sender, EventArgs e)
    {
        try
        {
            statusMessage.Text = string.Empty;
            var button = sender as Button;
            var p = button.BindingContext as Player;
            // test
            p.HeldCards.Add(new HeldCard { IsConfirmed = false, CardId = App.DBRepo.GetAllCards().FirstOrDefault().Id, PlayerId = p.Id});

            //end test thing

            App.DBRepo.LogCardSeen(p);
            RefreshPlayers();
        }
        catch (Exception ex)
        {
            statusMessage.Text = ex.Message;
            throw;
        }
    }
    //private void RemovePlayerClicked(object sender, EventArgs e)
    //{
    //    statusMessage.Text = string.Empty;
    //    var button = sender as Button;
    //    var p = button.BindingContext as Player;
    //    App.DBRepo.RemovePlayer(p);
    //    statusMessage.Text = App.DBRepo.StatusMessage;

    //    RefreshPlayers();
    //}
}