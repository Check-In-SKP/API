namespace CheckInSKP.Infrastructure.Mappings.Interfaces
{
    public interface IGenericMapper<TDomain, TEntity>
    {
        TDomain? MapToDomain(TEntity entity);
        TEntity MapToEntity(TDomain domain);
    }
}
