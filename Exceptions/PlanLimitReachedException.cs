namespace RedirectPizza.NET.Exceptions;

public class PlanLimitReachedException : Exception
{
    public PlanLimitReachedException(string message) : base(message) { }
}