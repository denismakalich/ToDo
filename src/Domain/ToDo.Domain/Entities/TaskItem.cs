using System.Diagnostics.CodeAnalysis;
using ToDo.Domain.Entities.Enums;

namespace ToDo.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private init; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    
    /// <summary>
    /// Priority is rated from 0 to 10
    /// </summary>
    public int Priority { get; private set; }
    public Status Status { get; private set; }
    public DateTimeOffset CreatedOn { get; private init; }
    public DateTimeOffset ModifiedOn { get; private set; }

    public TaskItem(Guid id, string title, string description, int priority, Status status, DateTimeOffset createdOn,
        DateTimeOffset modifiedOn)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id can't be an empty GUID", nameof(id));

        Id = id;
        SetTitle(title);
        SetDescription(description);
        SetPriority(priority);
        SetStatus(status);
        SetModifiedOn(modifiedOn);
        CreatedOn = createdOn;
    }

    public static TaskItem Create(string title, string description, int priority, Status status)
    {
        var id = Guid.NewGuid();
        var dateTimeNow = DateTimeOffset.Now;

        return new TaskItem(id, title, description, priority, status, dateTimeNow, dateTimeNow);
    }

    [MemberNotNull(nameof(Title))]
    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title can't be null or white space", nameof(title));

        Title = title;
        SetModifiedOn(DateTimeOffset.Now);
    }

    [MemberNotNull(nameof(Description))]
    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Title can't be null or white space", nameof(description));

        Description = description;
        SetModifiedOn(DateTimeOffset.Now);
    }

    public void SetPriority(int priority)
    {
        if (priority < 0 || priority > 10)
            throw new ArgumentOutOfRangeException(nameof(priority), "Priority can be from 0 to 10");

        Priority = priority;
        SetModifiedOn(DateTimeOffset.Now);
    }

    public void SetStatus(Status status)
    {
        if (status == Status.None)
            throw new ArgumentException("status can't be none", nameof(status));

        Status = status;
        SetModifiedOn(DateTimeOffset.Now);
    }

    public void SetModifiedOn(DateTimeOffset modifiedOn)
    {
        if (ModifiedOn != modifiedOn)
            ModifiedOn = modifiedOn;
    }
}