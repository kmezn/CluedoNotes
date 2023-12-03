using CluedoNotes.Models;
using System.Windows.Input;

namespace CluedoNotes.Pages;

public partial class SetUpWeapons : ContentPage
{
	public SetUpWeapons()
	{
		InitializeComponent();
        RefreshWeapons();

    }

    private void NewWeaponClicked(object sender, EventArgs e)
    {
        try
        {
            statusMessage.Text = string.Empty;
            App.DBRepo.AddNewCard(newWeapon.Text, isWeapon: true);
            statusMessage.Text = App.DBRepo.StatusMessage;
            newWeapon.Text = string.Empty;
            RefreshWeapons();
        }
        catch (Exception ex)
        {
            statusMessage.Text = ex.Message;
            throw;
        }
    }

    private void RemoveWeaponClicked(object sender, EventArgs e)
    {
        statusMessage.Text = string.Empty;
        var button = sender as Button;
        var p = button.BindingContext as Card;
        App.DBRepo.RemoveCard(p);
        statusMessage.Text = App.DBRepo.StatusMessage;

        RefreshWeapons();
    }


    private void RefreshWeapons()
    {
        List<Card> victims = App.DBRepo.GetAllCards(JustWeapons: true);
        WeaponList.ItemsSource = victims;
    }
}