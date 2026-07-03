namespace SCADA.HardwareUnits.TurboPumps;

public interface ITurboPump
{
    // TMS - Thermal Management System
    void PumpOn();
    void PumpOff();
    void SetRotationSpeed(double speed);
    void TmsOn();
    void TmsOff();
    void SetTmsTemp(double temperature);

}