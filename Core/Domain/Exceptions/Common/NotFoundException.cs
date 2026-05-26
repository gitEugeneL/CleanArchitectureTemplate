namespace Domain.Exceptions.Common;

public abstract class NotFoundException(string message) : Exception(message);
