using DirectoryWalker.Services.Interfaces;
using System;

namespace DirectoryWalker.Services
{
    public class ErrorLogService: IErrorLogService
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string GetExceptionMessage(Exception exception)
        {
            var message = "Error occured on server's side";
            logger.Error($"{message}: {exception.Message}. {exception.StackTrace}");
            return message;
        }

        public string GetWrongPathErrorMesage(string enteredPath)
        {
            var message = "There is no node with current path";
            logger.Info($"{message}: {enteredPath}");
            return message;
        }

        public string GetWrongPatternErrorMessage(string enteredPath)
        {
            var message = "There are incompatible characters in current path";
            logger.Info($"{message}: {enteredPath}");
            return message;
        }
    }
}