<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ReasonForTreatmentDetailDesc.aspx.cs" Title="Service Unit Item Component"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ReasonForTreatmentDetailDesc" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdServiceUnitItemServiceComp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdServiceUnitItemServiceComp" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Reason Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRReasonVisit" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblSRReasonVisit" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Reason For Treatment"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReasonsForTreatmentID" runat="server" Width="100px" MaxLength="10" ReadOnly="true" />
                            <asp:Label ID="lblReasonsForTreatmentName" runat="server"></asp:Label>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdReasonsForTreatmentDesc" runat="server" 
                    OnNeedDataSource="grdReasonsForTreatmentDesc_NeedDataSource"
                    OnUpdateCommand="grdReasonsForTreatmentDesc_UpdateCommand" 
                    OnInsertCommand="grdReasonsForTreatmentDesc_InsertCommand"
                    OnDeleteCommand="grdReasonsForTreatmentDesc_DeleteCommand" 
                    AutoGenerateColumns="False"
                    GridLines="None">
                    <HeaderContextMenu>
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRReasonVisit, ReasonsForTreatmentID, ReasonsForTreatmentDescID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="ReasonsForTreatmentDescID" HeaderText="Reasons For Treatment Desc ID" HeaderStyle-Width="200"
                                UniqueName="ReasonsForTreatmentDescID" SortExpression="ReasonsForTreatmentDescID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />    
                             <telerik:GridBoundColumn DataField="ReasonsForTreatmentDescName" HeaderText="Reasons For Treatment Desc Name"
                                UniqueName="ReasonsForTreatmentDescName" SortExpression="ReasonsForTreatmentDescName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" /> 
                             <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ReasonForTreatmentDetailDescDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ReasonForTreatmentDetailDescDetailEditColumn">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
