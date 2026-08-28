using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Atomtick.Avalonia.UIClient;

public class MfcVerficationViewModel
{
 
    public RelayCommand StartVerficationCommand { get; set; }
    public RelayCommand CancelVerficationCommand { get; set; }
    public RelayCommand LoadSinglePointVerficationModeCommand{get;set;}
    
    public RelayCommand LoadCustomVerficationModelCommand { get; set; }

    public MfcVerficationViewModel()
    {
        
    }
}

public class MfcVerficationViewModel : ObservableObject
{
    public MfcVerficationViewModel()
    {
        
    }

    [ObservableProperty]
    private string _gasName;
    [ObservableProperty]
    private double _setpointAbsolute;

    [ObservableProperty] private double _setpointRatio;
    [ObservableProperty] private double _setpointVerfication;
    [ObservableProperty] private double _errorRateSP;
    [ObservableProperty] private double _errorRateFS;
    
}