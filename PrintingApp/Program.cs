using PrintingApp.Presenters;
using PrintingApp.Views;
using PrintingAppRepository.ImpositionCalculator;
using PrintingAppRepository.ImpositionCalculator.Implementation;
using PrintingAppRepository.PrintingDesign;
using PrintingAppRepository.PrintingDesign.Implementation;
using PrintingAppRepository.SystemVariables;
using PrintingAppRepository.SystemVariables.Implementation;
using SimpleInjector;
using System;
using System.Windows.Forms;

namespace PrintingApp {
    static class Program
    {
        private static Container container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Bootstrap();
            
            Application.Run(container.GetInstance<ImpositionForm>());
        }

        private static void Bootstrap() {
            container = new Container();

            container.Register<IImpositionCalculatorManager, ImpositionCalculatorManager>(Lifestyle.Singleton);
            container.Register<IImpositionCalculatorRepository, ImpositionCalculatorRepository>(Lifestyle.Singleton);

            container.Register<IPrintingDesignManager, PrintingDesignManager>(Lifestyle.Singleton);
            container.Register<IPrintingDesignRepository, PrintingDesignRepository>(Lifestyle.Singleton);

            container.Register<ISystemVariablesManager, SystemVariablesManager>(Lifestyle.Singleton);
            container.Register<ISystemVariablesRepository, SystemVariablesRepository>(Lifestyle.Singleton);

            container.Register<IImpositionFormView, ImpositionForm>(Lifestyle.Singleton);
            container.Register<ISystemVariablesView, SystemVariablesForm>(Lifestyle.Singleton);

            container.Register<ImpositionForm>(Lifestyle.Singleton);
            container.Register<SystemVariablesPresenter>(Lifestyle.Singleton);

            container.Verify();
        }
    }
}
