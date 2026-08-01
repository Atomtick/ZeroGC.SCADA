using System.Configuration;
using System.Data;
using System.Windows;
using SCADA.FSM.Samples.TrafficLightSample;

namespace SCADA.FSM.Samples
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TrafficLight trafficLight = new TrafficLight();
            trafficLight.PostMsg(TrafficLight.TrafficLightCommand.Green);
        }
    }
}
