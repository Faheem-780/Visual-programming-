using System;
using System.Windows.Forms;
using Daily_Tracker_ver2.Helpers;

namespace Daily_Tracker_ver2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DatabaseHelper.EnsureHabitsTable();
            DatabaseHelper.EnsureUsersColumns();   // ← replaces EnsureProfilePictureColumn

            Application.Run(new Form1());
        }
    }
}
