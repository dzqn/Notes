switch (request.FlowAction)
{
    case PaperworkActionStatusEnum.Received:
        {
            result = WorkflowTransactionCallManager.ExecuteWorkflowApiCommand(request.WorkFlowId, "SendGmTargetToEnd",
                new CommandParameter[] { new CommandParameter
            {
                ParameterName = "FlowState",
                TypeName = "Command",
                Value = "End"
            }});
        }
        break;

    case PaperworkActionStatusEnum.Return when (request.From == PaperWorkTypeEnum.Sube && request.To == PaperWorkTypeEnum.GM):
        {
            result = WorkflowTransactionCallManager.ExecuteWorkflowApiCommand(request.WorkFlowId, "ReturnGmTargetToMuhaberatGw",
                new CommandParameter[] { new CommandParameter
            {
                ParameterName = "FlowState",
                TypeName = "Command",
                Value = "MuhaberatGateway"
            }});
        }
        break;

    case PaperworkActionStatusEnum.Return when (request.From == PaperWorkTypeEnum.Muhaberat && request.To == PaperWorkTypeEnum.GM):
        {
            result = WorkflowTransactionCallManager.ExecuteWorkflowApiCommand(request.WorkFlowId, "",
                new CommandParameter[] { new CommandParameter
            {
                ParameterName = "FlowState",
                TypeName = "Command",
                Value = ""
            }});
        }
        break;
    default:
        break;
}
//************************************************************************************************
switch (state, transition)
{
    case (State.Running, Transition.Suspend):
        state = State.Suspended;
        break;
    case (State.Suspended, Transition.Resume):
        state = State.Running;
        break;
    case (State.Suspended, Transition.Terminate):
        state = State.NotRunning;
        break;
    case (State.NotRunning, Transition.Activate):
        state = State.Running;
        break;
    default:
        throw new InvalidOperationException();
}
//************************************************************************************************
state = (state, transition) switch
{
    (State.Running, Transition.Suspend)     => State.Suspended,
    (State.Suspended, Transition.Resume)    => State.Running,
    (State.Suspended, Transition.Terminate) => State.NotRunning,
    (State.NotRunning, Transition.Activate) => State.Running,
    _ => throw new InvalidOperationException()
};
