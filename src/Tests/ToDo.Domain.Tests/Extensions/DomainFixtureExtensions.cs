using AutoFixture;
using ToDo.Domain.Tests.Customizations;
using ToDo.Domain.Tests.Entities;

namespace ToDo.Domain.Tests.Extensions;

public static class DomainFixtureExtensions
{
    public static IFixture GetFixture()
    {
        var fixture = new Fixture();
        fixture.Customize(new TaskItemCustomizations());

        return fixture;
    }

    public static string CreateString(this IFixture fixture, int length) =>
        new(fixture.CreateMany<char>(length).ToArray());
}