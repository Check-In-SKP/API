using CheckInSKP.Domain.Entities;

namespace CheckInSKP.Domain.Factories
{
    public class TimeTypeFactory
    {
        public TimeType CreateTimeType(int id, string name)
        {
            return new TimeType(id, name);
        }

        public TimeType CreateNewTimeType(string name)
        {
            return new TimeType(name);
        }
    }
}
