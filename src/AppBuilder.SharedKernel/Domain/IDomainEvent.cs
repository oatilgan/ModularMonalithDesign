namespace AppBuilder.SharedKernel.Domain
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }

}
