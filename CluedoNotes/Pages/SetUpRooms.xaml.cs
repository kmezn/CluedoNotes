using CluedoNotes.Models;

namespace CluedoNotes.Pages;

public partial class SetUpRooms : ContentPage
{
	public SetUpRooms()
	{
		InitializeComponent();
        RefreshRooms();

    }
    private void NewRoomClicked(object sender, EventArgs e)
    {
        try
        {
            statusMessage.Text = string.Empty;
            App.DBRepo.AddNewRoom(newRoom.Text);
            statusMessage.Text = App.DBRepo.StatusMessage;
            newRoom.Text = string.Empty;
            RefreshRooms();
        }
        catch (Exception ex)
        {
            statusMessage.Text = ex.Message;
            throw;
        }
    }

    private void RemoveRoomClicked(object sender, EventArgs e)
    {
        statusMessage.Text = string.Empty;
        var button = sender as Button;
        var p = button.BindingContext as Room;
        App.DBRepo.RemoveRoom(p);
        statusMessage.Text = App.DBRepo.StatusMessage;

        RefreshRooms();
    }


    private void RefreshRooms()
    {
        List<Room> victims = App.DBRepo.GetAllRooms();
        RoomList.ItemsSource = victims;
    }
}