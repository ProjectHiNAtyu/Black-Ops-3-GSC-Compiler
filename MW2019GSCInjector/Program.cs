using Simple_PC_GSC_Injector;

namespace MW2019GSCInjector
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( )
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize( );
            Application.Run( new MW2019GscInjector( ) );
        }
    }
}