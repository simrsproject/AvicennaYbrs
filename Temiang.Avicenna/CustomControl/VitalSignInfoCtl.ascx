<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VitalSignInfoCtl.ascx.cs"
    Inherits="Temiang.Avicenna.CustomControl.VitalSignInfoCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<telerik:RadCodeBlock runat="server" id="cdBlock">
<script type="text/javascript">
    function AddFilterExpression(grid, dataField, filterValue) {
        var filterExpression = new Telerik.Web.UI.GridFilterExpression();
        var filterFunction = Telerik.Web.UI.GridFilterFunction.Contains;

        filterExpression.set_fieldName(dataField);
        filterExpression.set_fieldValue(filterValue);
        filterExpression.set_filterFunction(filterFunction);
        grid.get_masterTableView()._filterExpressions.add(filterExpression);
    }

    function grdVitalSign_GridCreated(sender, args) {
        var grid = sender;
        AddFilterExpression(grid, "RegistrationNo", "<%=RegistrationNo%>");
        AddFilterExpression(grid, "UrlRoot", "<%=Helper.UrlRoot()%>");
        AddFilterExpression(grid, "VitalSignDateTime", "<%=VitalSignDateTime==null? string.Empty : VitalSignDateTime.Value.ToString("s")%>");
    }
</script>
</telerik:RadCodeBlock>
<telerik:RadGrid ID="grdVitalSign" runat="server"
    AutoGenerateColumns="False" GridLines="None">
    <ClientSettings EnableRowHoverStyle="false">
        <ClientEvents OnGridCreated="grdVitalSign_GridCreated" />
        <DataBinding Location="~/Module/RADT/Cpoe/EmrWebService.asmx" SelectMethod="VitalSignLastValueAndCount" />

        <Selecting AllowRowSelect="false" />
        <Resizing AllowColumnResize="True" />
    </ClientSettings>
    <MasterTableView  ClientDataKeyNames="VitalSignID" TableLayout="Fixed">
        <Columns>
            <telerik:GridTemplateColumn UniqueName="VitalSignName" HeaderText="Vital Sign">
                <ClientItemTemplate>
                      <a href="javascript:void(0);" onclick='javascript:openVitalSignChart("#=VitalSignID #")'>#=VitalSignName #</a>
                </ClientItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="72px"></telerik:GridDateTimeColumn>
            <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="43px"></telerik:GridBoundColumn>
            <telerik:GridTemplateColumn UniqueName="Value" HeaderText="" HeaderStyle-Width="80px">
                <ClientItemTemplate>
                      <div style='background-color: #=EwsLevelColor #;width:100%;padding-left: 2px'>#=QuestionAnswerFormatted #</div>
                </ClientItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="Chart" HeaderText="">
                <HeaderStyle Width="30px"></HeaderStyle>
                <ClientItemTemplate>
                      <a href="javascript:void(0);" onclick='javascript:openVitalSignChart("#=VitalSignID #")'>
                       <img src='#=UrlRoot#/Images/Toolbar/barchart.bmp' alt='chart' /></a>
                </ClientItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>

</telerik:RadGrid>

