using System;

namespace DirectoryWalker.Services.Interfaces
{
    public interface IErrorLogService
    {
        string GetWrongPatternErrorMessage(string enteredPath);
        string GetWrongPathErrorMesage(string enteredPath);
        string GetExceptionMessage(Exception exception);
    }
}
