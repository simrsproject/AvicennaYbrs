<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationInfoList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.RegistrationInfoList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Additional Note</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script language="javascript" type="text/javascript">
                function RowDblClick(sender, eventArgs) {
                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                }

                function UpdateInformationCount(objectName, iCount) {
                    //alert(objectName);
                    if (objectName == null || objectName == undefined || objectName == 'none') {
                        // do nothing
                    } else {
                        var obj = GetRadWindow().BrowserWindow.document.getElementById(objectName);
                        obj.innerHTML = iCount
                        if (iCount > 0) {
                            // set bubble visible true
                            obj.style.visibility = 'visible';
                        } else {
                            // set bubble visible false
                            obj.style.visibility = 'hidden';
                        }
                    }
                }

                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }

                function CallFnOnParent() {
                    GetRadWindow().BrowserWindow.CalledFn();
                }
            </script>

            <style type="text/css">
                .MyImageButton {
                    cursor: hand;
                }

                .EditFormHeader td {
                    font-size: 14px;
                    padding: 4px !important;
                    color: #0066cc;
                }
            </style>
        </telerik:RadCodeBlock>
        <telerik:RadSkinManager ID="fw_RadSkinManager" runat="server" />
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
            Orientation="HorizontalTop">
            <Tabs>
                <telerik:RadTab runat="server" Text="Registration Info" PageViewID="pgRegistrationInfo"
                    Selected="True" />
                <telerik:RadTab runat="server" Text="Patient Info" PageViewID="pgPatientInfo" />
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray" SelectedIndex="0">
            <telerik:RadPageView ID="pgRegistrationInfo" runat="server">
                <telerik:RadGrid ID="grdRegistrationInfo" GridLines="None" runat="server" AllowAutomaticDeletes="False"
                    AllowAutomaticInserts="False" PageSize="10" AllowAutomaticUpdates="False" AllowPaging="False"
                    AutoGenerateColumns="False" OnItemUpdated="grdRegistrationInfo_ItemUpdated" OnItemDeleted="grdRegistrationInfo_ItemDeleted"
                    OnItemInserted="grdRegistrationInfo_ItemInserted" OnDataBound="grdRegistrationInfo_DataBound"
                    OnNeedDataSource="grdRegistrationInfo_NeedDataSource" OnInsertCommand="grdRegistrationInfo_InsertCommand"
                    OnUpdateCommand="grdRegistrationInfo_UpdateCommand" OnDeleteCommand="grdRegistrationInfo_DeleteCommand">
                    <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="RegistrationInfoID"
                        HorizontalAlign="NotSet" AutoGenerateColumns="False">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="Information" HeaderText="Information" UniqueName="Information"
                                ColumnEditorID="GridTextBoxColumnEditorInformation">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="User" UniqueName="UserName"
                                ReadOnly="true" HeaderStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update" UniqueName="LastUpdateDateTime"
                                SortExpression="LastUpdateDateTime" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridButtonColumn ConfirmText="Delete this data?" ConfirmDialogType="RadWindow"
                                ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                UniqueName="DeleteColumn">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings ColumnNumber="1" CaptionDataField="RegistrationInfoID" CaptionFormatString="Edit properties of ID: {0}">
                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" BackColor="White"
                                Width="100%" />
                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                            <EditColumn ButtonType="ImageButton" InsertText="Save New Note" UpdateText="Save Note"
                                UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                            </EditColumn>
                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
                <telerik:GridTextBoxColumnEditor ID="GridTextBoxColumnEditorInformation" runat="server"
                    TextBoxStyle-Width="700px" TextBoxMode="MultiLine" />
            </telerik:RadPageView>
            <telerik:RadPageView ID="pgPatientInfo" runat="server">
                <telerik:RadGrid ID="grdPatientInfo" GridLines="None" runat="server" AllowAutomaticDeletes="False"
                    AllowAutomaticInserts="False" PageSize="10" AllowAutomaticUpdates="False" AllowPaging="False"
                    AutoGenerateColumns="False" OnItemUpdated="grdPatientInfo_ItemUpdated" OnItemDeleted="grdPatientInfo_ItemDeleted"
                    OnItemInserted="grdPatientInfo_ItemInserted" OnDataBound="grdPatientInfo_DataBound"
                    OnNeedDataSource="grdPatientInfo_NeedDataSource" OnInsertCommand="grdPatientInfo_InsertCommand"
                    OnUpdateCommand="grdPatientInfo_UpdateCommand" OnDeleteCommand="grdPatientInfo_DeleteCommand">
                    <MasterTableView Width="100%" CommandItemDisplay="Top" DataKeyNames="PatientInfoID"
                        HorizontalAlign="NotSet" AutoGenerateColumns="False">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="Information" HeaderText="Information" UniqueName="Information"
                                ColumnEditorID="GridTextBoxColumnEditorInformation">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="User" UniqueName="UserName"
                                ReadOnly="true" HeaderStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update" UniqueName="LastUpdateDateTime"
                                SortExpression="LastUpdateDateTime" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridButtonColumn ConfirmText="Delete this data?" ConfirmDialogType="RadWindow"
                                ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                UniqueName="DeleteColumn">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings ColumnNumber="1" CaptionDataField="PatientInfoID" CaptionFormatString="Edit properties of ID: {0}">
                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3" BackColor="White"
                                Width="100%" />
                            <FormTableStyle CellSpacing="0" CellPadding="2" BackColor="White" />
                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                            <EditColumn ButtonType="ImageButton" InsertText="Save New Note" UpdateText="Save Note"
                                UniqueName="EditCommandColumn1" CancelText="Cancel edit">
                            </EditColumn>
                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
                <telerik:GridTextBoxColumnEditor ID="GridTextBoxColumnEditor1" runat="server"
                    TextBoxStyle-Width="700px" TextBoxMode="MultiLine" />
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </form>
</body>
</html>
