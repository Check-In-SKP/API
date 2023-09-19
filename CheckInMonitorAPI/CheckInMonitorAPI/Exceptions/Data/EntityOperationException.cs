namespace CheckInMonitorAPI.Exceptions.Data
{
    public class EntityOperationException : Exception
    {
        public EntityOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
