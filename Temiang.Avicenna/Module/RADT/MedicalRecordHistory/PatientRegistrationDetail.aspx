<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientRegistrationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientRegistrationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var _lastProcessID = '';

        function openRegPrintOpt(regNo, regType) {
            _lastProcessID = 'printopt';

            var oWnd = $find("<%= winPrintOpt.ClientID %>");
            oWnd.setUrl("../Registration/RegistrationPrint.aspx?regno=" + regNo + "&rt=" + regType);
            oWnd.show();
        }

        function openWinMergeBillingInfo(regNo) {
            var oWnd = $find("<%= winRegInfo.ClientID %>");
            oWnd.setUrl("../../Charges/Billing/FinalizeBilling/MergeBillingInfo.aspx?regNo=" + regNo);
            oWnd.show();
        }

        function onClientClose(oWnd, args) {
            switch (_lastProcessID) {
                case 'printopt':
                    if (oWnd.argument && oWnd.argument.print != null) {
                        var oWnd = $find("<%= winPrintPreview.ClientID %>");
                        oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                        oWnd.show();
                        //oWnd.maximize();
                        oWnd.add_pageLoad(onClientPageLoad);
                    }
                    break;
            }
            oWnd = null;
        }

        function openWinRegistrationInfo(regNo) {
            var oWnd = $find("<%= winRegInfo.ClientID %>");
            var lblToBeUpdate = "noti_" + regNo;

            oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
            oWnd.show();
        }
    </script>

    <telerik:RadWindow ID="winPrintOpt" Width="400px" Height="300px" runat="server" Title="Select report then click Ok button"
        Behavior="Move,Close" DestroyOnClose="false" VisibleStatusbar="false" Modal="true"
        ReloadOnShow="true" OnClientClose="onClientClose" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrintPreview" Behavior="Move,Close" DestroyOnClose="false"
        VisibleStatusbar="false" Modal="true" runat="server" Title="Preview" ReloadOnShow="true"
        ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="400px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdRegisteredList" runat="server" OnNeedDataSource="grdRegisteredList_NeedDataSource"
                    AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowSorting="True"
                    GridLines="None" ShowStatusBar="true">
                    <MasterTableView DataKeyNames="RegistrationNo" GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="RegistrationTypeName" HeaderText="Registration Type "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="RegistrationTypeName" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit " />
                                    <telerik:GridGroupByField FieldName="RoomName" HeaderText="Room " />
                                    <telerik:GridGroupByField FieldName="ClassName" HeaderText="Class " />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="RegistrationDate"
                                HeaderText="Reg. Date" UniqueName="RegistrationDate" SortExpression="RegistrationDate"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="RegistrationTime" HeaderText="Time"
                                UniqueName="RegistrationTime" SortExpression="RegistrationTime" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No"
                                UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="PatientID" HeaderText="Patient ID"
                                UniqueName="PatientID" SortExpression="PatientID" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="MedicalNo" HeaderText="Medical No"
                                UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False" />
                            <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                                SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="100px" Visible="false" />
                            <%--<telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                                SortExpression="BedID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />--%>

                            <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Bed No"
                                UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "BedID")%>&nbsp;
                                    <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBedStatusPending")) ? "<img src=\"../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Need check-in confirmation\" title=\"Need check-in confirmation\" />" : string.Empty%>
                                    
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridDateTimeColumn HeaderStyle-Width="60px" DataField="DischargeDate" HeaderText="Discharge Date"
                                UniqueName="DischargeDate" SortExpression="DischargeDate" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DischargeTime" HeaderText="Time"
                                UniqueName="DischargeTime" SortExpression="DischargeTime" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="LastCreateUserID" HeaderText="Created By" UniqueName="LastCreateUserID"
                                SortExpression="LastCreateUserID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsClosed" HeaderText="Closed"
                                UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsVoid" HeaderText="Void"
                                UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsHoldTransactionEntry"
                                HeaderText="Locked" UniqueName="IsHoldTransactionEntry" SortExpression="IsHoldTransactionEntry"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridBoundColumn DataField="IsHoldTransactionEntryByUserID" HeaderText="Locked / Unlocked By"
                                UniqueName="IsHoldTransactionEntryByUserID" SortExpression="IsHoldTransactionEntryByUserID"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) ? string.Empty
                                    : string.Format("<a href=\"#\" onclick=\"openRegPrintOpt('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SRRegistrationType"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="35px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"openWinMergeBillingInfo('{0}'); return false;\"><img src=\"../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Merge Billing History\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="35px" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" AllowExpandCollapse="true">
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
