var dataFromService = this.blnSocketSoapClient.RunQuery(context.ConfigContext.BilinUserCode, context.ConfigContext.BilinPassWord, context.ConfigContext.BilinQueryCode, context.ConfigContext.BilinParmString);
if ((dataFromService == null))
    return;

StringReader theReader = new StringReader(dataFromService);
DataSet theDataSet = new DataSet();
theDataSet.ReadXml(theReader);
var dtTable = theDataSet.Tables[0];
DataRow[] drsPersonelData = dtTable.Select();
