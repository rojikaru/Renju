using CommunityToolkit.Mvvm.Input;
using RenjuLib.Data;
using RenjuLib.Player;
using RenjuLib.Session;

namespace Renju.ViewModels;

public class RenjuViewModel : ObservableObject
{
    #region data

    public ISession CurrentGameSession { get; set; }

    #endregion

    #region commands

    // add more commands here

    #endregion

    public RenjuViewModel()
    {
        // Grab the current game session from the database
        CurrentGameSession = new SingleSession(
            new HumanPlayer(CellStone.Black, "A"),
            new HumanPlayer(CellStone.Black, "B")
        );
    }
}
