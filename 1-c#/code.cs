/*
System.Collections.Generic Classes

Dictionary<TKey,TValue>   --> 
List<T>
Queue<T>   --> Represents a first in, first out (FIFO) collection of objects.
Stack<T>  --> Represents a last in, first out (LIFO) collection of objects.
SortedList<TKey,TValue>  --> Represents a collection of key/value pairs that are sorted by key based on the associated IComparer<T> implementation
Tuple

System.Collections Classes

ArrayList
Hashtable  --> Represents a collection of key/value pairs that are organized based on the hash code of the key.
Queue  --> 	Represents a first in, first out (FIFO) collection of objects.
Stack  --> 	Represents a last in, first out (LIFO) collection of objects.
SortedList
BitArray
*/


/*

c#                      JS                    Java
--------------------------------------------------
Find,First()            find
FindIndex               findindex
IndexOf                 indexOf
Where                   filter
Select                  map
Sum, Aggregate           recude
Any                     some
All                     every
StartsWith              startsWith
EndsWith                endsWith
                        flat
*/


// Delegate filter
Func<VpPaperWorkIntDocumentTracking, bool> funcFirstFilter = (d) =>
{
    var divisionCode = "";

    if (!string.IsNullOrEmpty(d.RecordCode))
    {
        var array = d.RecordCode.Split('_');
        if (array?.Length > 0)
            divisionCode = array[1];
    }

    return d.AppName.Equals(request.App.ToString()) && divisionCode.Equals(transactionHeader.PerformerUser.DivisionCode.ToString());
};
dataList = context.VpPaperWorkIntDocumentTracking.Where(funcFirstFilter).ToList();


// switch case *************************************************************************
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
state = (state, transition) switch
{
    (State.Running, Transition.Suspend) => State.Suspended,
    (State.Suspended, Transition.Resume) => State.Running,
    (State.Suspended, Transition.Terminate) => State.NotRunning,
    (State.NotRunning, Transition.Activate) => State.Running,
    _ => throw new InvalidOperationException()
};
