using System;
using System.Reflection;
using System.Linq;
using Xunit;

namespace ProductService.Architecture.UnitTests;

public class ProductServiceTests
{
    private readonly Assembly _applicationAssembly;
    private readonly Assembly _infrastructureAssembly;
    private readonly Assembly _webApiAssembly;
    private readonly Assembly _domain;

    public ProductServiceTests()
    {
        // Load your assemblies here
        _applicationAssembly = Assembly.Load("ProductService.Application");
        _domain = Assembly.Load("ProductService.Domain");
        _infrastructureAssembly = Assembly.Load("ProductService.Infrastructure");
        _webApiAssembly = Assembly.Load("ProductService.Web");
    }

    [Fact]
    public void Application_ShouldNotDependOn_Infrastructure()
    {
        var infraTypes = _infrastructureAssembly.GetTypes();
        var appTypes = _applicationAssembly.GetTypes();

        foreach (var type in appTypes)
        {
            var dependencies = type.GetConstructors()
                .SelectMany(c => c.GetParameters().Select(p => p.ParameterType));

            Assert.False(dependencies.Intersect(infraTypes).Any(),
                $"{type.Name} in Application layer should not depend on Infrastructure layer.");
        }
    }

    [Fact]
    public void Infrastructure_ShouldDependOn_ApplicationInterfaces()
    {
        // Act
        var applicationInterfaces = _applicationAssembly.GetTypes()
                .Where(t => t.IsInterface && t.Namespace == "YourProject.Application.Interfaces")
                .ToList();

        var infrastructureTypes = _infrastructureAssembly.GetTypes();

        // Assert
        foreach (var infrastructureType in infrastructureTypes)
        {
            var interfacesImplemented = infrastructureType.GetInterfaces();

            foreach (var applicationInterface in applicationInterfaces)
            {
                // Check if the infrastructure type implements the application interface
                bool implementsInterface = interfacesImplemented.Contains(applicationInterface);
                Assert.True(implementsInterface,
                    $"The type {infrastructureType.Name} does not implement the interface {applicationInterface.Name}.");
            }
        }
    }

    [Fact]
    public void AllDomainModels_ShouldBeInDomainLayer()
    {
        //var domainAssembly = Assembly.Load("YourProject.Domain");
        var domainTypes = _domain.GetTypes();
        var appTypes = _applicationAssembly.GetTypes();

        // Check for domain models in the Application layer
        foreach (var type in appTypes)
        {
            Assert.False(domainTypes.Contains(type),
                $"{type.Name} should be in the Domain layer, not in Application.");
        }
    }

    [Fact]
    public void Controllers_ShouldOnlyDependOn_ApplicationServices()
    {
        var controllerTypes = _webApiAssembly.GetTypes()
            .Where(t => t.Name.EndsWith("Controller"));

        foreach (var controller in controllerTypes)
        {
            var dependencies = controller.GetConstructors()
                .SelectMany(c => c.GetParameters().Select(p => p.ParameterType));

            Assert.True(dependencies.All(d => d.Assembly == _applicationAssembly || d.IsInterface),
                $"{controller.Name} should only depend on Application layer services.");
        }

    }
}
