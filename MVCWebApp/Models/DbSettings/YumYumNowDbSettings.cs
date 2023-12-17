namespace MVCWebApp.Models.DbSettings;

public class YumYumNowDbSettings : IYumYumNowDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string UsersCollectionName { get; set; } = string.Empty;
}
