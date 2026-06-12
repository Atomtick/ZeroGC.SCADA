namespace SCADA.HardwareUnits.TurboPumps;

public struct DeviceData
{
    public bool IsTmsOn { get; set; }
    public double TmsTempSetpoint { get; set; }
    public double TmsTempFeedback { get; set; }
    public double RotationSpeedSetpoint { get; set; }
    public double RotationSpeedFeedback { get; set; }
    public bool IsPumpOn { get; set; }
}