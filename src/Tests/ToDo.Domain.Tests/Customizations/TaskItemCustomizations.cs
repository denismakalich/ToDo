using AutoFixture;
using ToDo.Domain.Entities;
using ToDo.Domain.Entities.Enums;

namespace ToDo.Domain.Tests.Customizations;

public class TaskItemCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var id = fixture.Create<Guid>();
        var title = fixture.Create<string>();
        var description = fixture.Create<string>();
        var priority = fixture.Create<int>();
        var status = fixture.Create<Status>();
        var createdOn = fixture.Create<DateTimeOffset>();
        var modifiedOn = fixture.Create<DateTimeOffset>();

        fixture.Customize<TaskItem>(composer =>
            composer.FromFactory(() => new TaskItem(id, title, description, 0, Status.New, createdOn, modifiedOn)));
    }
}