namespace RepositoryPattern.DapperORM.Modules;

public interface IModule
{
    void MapEndpoints(IEndpointRouteBuilder endpoint);
}