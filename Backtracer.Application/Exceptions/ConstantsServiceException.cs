namespace Backtracer.Application.Exceptions;

public class ConstantsServiceException : Exception {
    public ConstantsServiceException(string message = "Unknown Constants Service Error") : base(message) { }
}

public class ConstantTypeNotFoundException : ConstantsServiceException {
    public ConstantTypeNotFoundException() : base("Couldn't find Constant Type") { }
}

public class ConstantGroupUniqueException : ConstantsServiceException {
    public ConstantGroupUniqueException() : base("Constant group already exists") { }
}
