
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
