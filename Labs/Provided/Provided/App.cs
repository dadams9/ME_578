using NXOpen;
using NXOpen.MenuBar;

namespace ME578_Lab7
{
    public class App
    {
        public static int Startup()
        {
            UI.GetUI().MenuBarManager.AddMenuAction("ME578_Lab7", PartGen);
            return 0;
        }

        public static int GetUnloadOption(string unused)
        {
            return (int)Session.LibraryUnloadOption.AtTermination;
        }

        private static MenuBarManager.CallbackStatus PartGen(MenuButtonEvent ev)
        {
            //Get data from block dialog
            block_dialog bracket_parameters = new block_dialog();
            bracket_parameters.Show();

            double base_thick = bracket_parameters.BaseThickness;
            double base_length = bracket_parameters.BaseLength;
            double back_thick = bracket_parameters.BackThickness;
            double back_height = bracket_parameters.BackHeight;
            double fillet_rad = bracket_parameters.FilletRadius;

            //Pass to "journal"
            NXJournal bracket = new NXJournal();
            bracket.SetBracketDimensions(base_thick, base_length, back_thick, back_height, fillet_rad);

            return MenuBarManager.CallbackStatus.Continue;
        }
    }
}