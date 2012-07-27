using System.Windows.Forms;

namespace VeeTresette
{
    internal class Program
    {
        private static void Main()
        {
            Game testGame = new Game();
            GameForm testGameForm = new GameForm(testGame);
            testGame.GameForm = testGameForm;

            AI.Game = testGame;
            AI.GameForm = testGameForm;

            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(testGameForm);
        }
    }
}