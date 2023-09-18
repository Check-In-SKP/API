namespace CheckInMonitorAPI
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string CheckInDB { get; set; }
    }
}
