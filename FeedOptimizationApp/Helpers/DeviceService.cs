namespace FeedOptimizationApp.Helpers;

public class DeviceService
{
    public DeviceInfoModel GetDeviceInfo()
    {
        return new DeviceInfoModel
        {
            Manufacturer = DeviceInfo.Manufacturer,
            Model = DeviceInfo.Model,
            Name = DeviceInfo.Name,
            VersionString = DeviceInfo.VersionString,
            Platform = DeviceInfo.Platform.ToString(),
            Idiom = DeviceInfo.Idiom.ToString(),
            DeviceType = DeviceInfo.DeviceType.ToString()
        };
    }
}

public class DeviceInfoModel
{
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string Name { get; set; }
    public string VersionString { get; set; }
    public string Platform { get; set; }
    public string Idiom { get; set; }
    public string DeviceType { get; set; }
}