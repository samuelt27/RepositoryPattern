namespace RepositoryPattern.EntityFrameworkCore.Modules;

public interface IModule
{
    void MapEndpoints(IEndpointRouteBuilder endpoint);
}