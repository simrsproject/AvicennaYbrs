<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NursingImplementationEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NursingImplementationEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="episodediagnosectl" Src="~/Module/RADT/Cpoe/Common/EpisodeDiagnoseCtl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function winDialog_ClientClose(oWnd, args) {
            //get the transferred arguments from MasterDialogEntry
            var arg = args.get_argument();
            if (arg != null) {
                if (arg.callbackMethod === 'submit') {
                    __doPostBack(arg.eventTarget, arg.eventArgument);
                } else {

                    if (arg.callbackMethod === 'rebind') {
                        var ctl = $find(arg.eventTarget);
                        if (typeof ctl.rebind == 'function') {
                            ctl.rebind();
                        } else {
                            var masterTable = $find(arg.eventTarget).get_masterTableView();
                            masterTable.rebind();
                        }
                    }
                }
            }
        }

    </script>
    <telerik:RadAjaxLoadingPanel ID="ralp_ajaxLoadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridListImplementasiDiagnosa">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListImplementasi" LoadingPanelID="ralp_ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblInterventionName" LoadingPanelID="ralp_ajaxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="gridListImplementasi">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridListImplementasi" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winDialog" Width="700px" Height="300px" runat="server" Modal="True" Behaviors="None" VisibleTitlebar="False" VisibleStatusbar="False" OnClientClose="winDialog_ClientClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnServiceUnitID" />
    <asp:HiddenField runat="server" ID="hdnRegistrationInfoMedicID" />

    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 25%; vertical-align: top;">
                <telerik:RadGrid ID="gridListImplementasiDiagnosa" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListImplementasiDiagnosa_NeedDataSource"
                    OnItemDataBound="gridListImplementasiDiagnosa_ItemDataBound"
                    OnItemCommand="gridListImplementasiDiagnosa_ItemCommand" OnPreRender="gridListImplementasiDiagnosa_PreRender">
                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID,ID" DataKeyNames="NursingDiagnosaID,ID"
                        GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="DiagName" HeaderText=" " HeaderValueSeparator=" ">
                                    </telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="DiagID" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" SortExpression="Id"
                                Visible="false" UniqueName="Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaID" HeaderText="Id" SortExpression="NursingDiagnosaID"
                                Visible="false" UniqueName="NursingDiagnosaID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaNameEdited" HeaderText="NIC" SortExpression="NursingDiagnosaNameEdited"
                                UniqueName="NursingDiagnosaNameEdited">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                                SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td style="width: 75%; vertical-align: top;">
                <table class="info success" width="100%">
                    <tr>
                        <td style="width:100px;">
                            <span style="font-size: 1.5em;">Implementation: </span>
                        </td> 
                        <td><span style="font-size: 1.5em;"><asp:Label ID="lblInterventionName" runat="server"></asp:Label></span></td>
                    </tr>
                </table>
                <telerik:RadGrid ID="gridListImplementasi" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListImplementasi_OnNeedDataSource"
                    OnDeleteCommand="gridListImplementasi_DeleteCommand" OnInsertCommand="gridListImplementasi_InsertCommand"
                    OnUpdateCommand="gridListImplementasi_UpdateCommand" OnItemDataBound="gridListImplementasi_ItemDataBound" OnPreRender="gridListImplementasi_OnPreRender"
                    AllowPaging="True" PageSize="10" AllowSorting="False" AllowCustomPaging="true" >
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID" DataKeyNames="NursingDiagnosaID,ID" 
                    InsertItemPageIndexAction="ShowItemOnCurrentPage">
                        <Columns>
                            <telerik:GridEditCommandColumn UniqueName="EditColumn" ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" SortExpression="Id"
                                Visible="false" UniqueName="Id">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ExecuteDateTime" HeaderText="Execution Date"
                                DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime" 
                                SortExpression="ExecuteDateTime" UniqueName="ExecuteDateTime" >
                                <HeaderStyle HorizontalAlign="Left" Width="130" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Implemetation" UniqueName="GetFullImplementationName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
	                                <%# GetFullImplementationNameFormatted(((string)Eval("NursingDiagnosaName")), (string)Eval("S"),(string)Eval("O"),(string)Eval("A"),(string)Eval("P"),Eval("Info5"),Eval("SubmitBy"),Eval("ReceiveBy")).Replace("\n", "<br/>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Respond / Result" UniqueName="Respond">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
	                                <%# (Eval("Respond")).ToString().Replace("\n", "<br/>") + " " + (Eval("Respond2")).ToString().Replace("\n", "<br/>")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                                SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TmpNursingDiagnosaID" HeaderText="Id" SortExpression="TmpNursingDiagnosaID"
                                Visible="false" UniqueName="TmpNursingDiagnosaID">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RefToUserName" HeaderText="User" SortExpression="RefToUserName"
                                UniqueName="RefToUserName">
                                <HeaderStyle HorizontalAlign="Left" Width="90px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings 
                            UserControlName="NursingCareStandardTransDTDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="NursingCareImplementationEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
