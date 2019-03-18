using System;
using System.Windows.Forms;

namespace Galaxies.Debug
{

    static class CrashHandler
    {

        public static void ShowException(string exception)
        {
            MessageBox.Show(exception, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowException(Exception exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }

}
