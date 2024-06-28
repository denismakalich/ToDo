using AutoFixture;
using FluentAssertions;
using ToDo.Domain.Entities;
using ToDo.Domain.Entities.Enums;
using ToDo.Domain.Tests.Extensions;

namespace ToDo.Domain.Tests.Entities;

public class TaskItemTests
{
    private readonly IFixture _fixture = DomainFixtureExtensions.GetFixture();

    [Fact]
    public void Ctor_WithValidParams_SuccessInit()
    {
        // Arange
        var id = _fixture.Create<Guid>();
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var priority = 0;
        var status = _fixture.Create<Status>();
        var createdOn = _fixture.Create<DateTimeOffset>();
        var userId = _fixture.Create<Guid>();

        // Act
        var task = new TaskItem(id, title, description, priority, status, createdOn, userId);

        // Assert
        task.Id.Should().Be(id);
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Priority.Should().Be(priority);
        task.Status.Should().Be(status);
        task.CreatedOn.Should().Be(createdOn);
        task.UserId.Should().Be(userId);
    }

    [Fact]
    public void Ctor_WithIdEmpty_ThrowsArgumentException()
    {
        // Arange
        var id = Guid.Empty;
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var priority = 0;
        var status = _fixture.Create<Status>();
        var createdOn = _fixture.Create<DateTimeOffset>();
        var userId = _fixture.Create<Guid>();

        // Act
        var action = () => new TaskItem(id, title, description, priority, status, createdOn, userId);

        // Assert
        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(id));
    }

    [Fact]
    public void Create_ValidParams_SuccessCreate()
    {
        // Arange
        var title = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var priority = 0;
        var status = _fixture.Create<Status>();
        var userId = _fixture.Create<Guid>();

        // Act
        var task = TaskItem.Create(title, description, priority, status, userId);

        // Assert
        task.Id.Should().NotBeEmpty();
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Priority.Should().Be(priority);
        task.Status.Should().Be(status);
        task.UserId.Should().Be(userId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetTitle_IsNullOrWhiteSpace_ThrowsArgumentException(string? title)
    {
        // Arange
        var taskItem = _fixture.Create<TaskItem>();

        // Act
        var action = () => taskItem.SetTitle(title);

        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(title));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SetDescription_IsNullOrWhiteSpace_ThrowsArgumentException(string? description)
    {
        // Arange
        var taskItem = _fixture.Create<TaskItem>();

        // Act
        var action = () => taskItem.SetDescription(description);

        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(description));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void SetPriority_IsNullOrWhiteSpace_ThrowsArgumentOutOfRangeException(int priority)
    {
        // Arange
        var taskItem = _fixture.Create<TaskItem>();

        // Act
        var action = () => taskItem.SetPriority(priority);

        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(priority));
    }
    
    [Theory]
    [InlineData(Status.None)]
    public void SetStatus_IsNullOrWhiteSpace_ThrowsArgumentException(Status status)
    {
        // Arange
        var taskItem = _fixture.Create<TaskItem>();

        // Act
        var action = () => taskItem.SetStatus(status);

        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(status));
    }
    
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public void SetUserId_IsNullOrWhiteSpace_ThrowsArgumentException(Guid userId)
    {
        // Arange
        var taskItem = _fixture.Create<TaskItem>();

        // Act
        var action = () => taskItem.SetUser(userId);

        action.Should()
            .Throw<ArgumentException>()
            .WithParameterName(nameof(userId));
    }
}