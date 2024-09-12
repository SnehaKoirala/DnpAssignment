namespace Entities;

public class NotFoundException(string message): Exception(message);

public class ValidationException(string message): Exception(message);

