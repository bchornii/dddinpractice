namespace DddInPractice.CommandHandlers.Results
{
    public enum CommandResultStatus
    {
        Ok,
        Failed,
        BadCommandData
    }

    public class CommandResult
    {
        private readonly CommandResultStatus _commandResultStatus;
        private readonly string _message;

        public bool IsSuccess => _commandResultStatus == CommandResultStatus.Ok;
        public bool IsFailed => _commandResultStatus == CommandResultStatus.Failed ||
            _commandResultStatus == CommandResultStatus.BadCommandData;

        public CommandResult(CommandResultStatus commandResultStatus, string message = null)
        {
            _commandResultStatus = commandResultStatus;
        }

        public static CommandResult GetSuccess()
        {
            return new CommandResult(CommandResultStatus.Ok);
        }

        public static CommandResult GetFailed(string message)
        {
            return new CommandResult(CommandResultStatus.Failed, message);
        }
    }
}
