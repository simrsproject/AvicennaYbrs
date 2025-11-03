<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="RiskGradingMtxDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.RiskGradingMtxDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="20">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="ID required."
                                ControlToValidate="txtItemID" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Clinical Impact"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Incident Type required."
                                ControlToValidate="txtItemName" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdGrading" runat="server" OnNeedDataSource="grdGrading_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdGrading_UpdateCommand"
        OnDeleteCommand="grdGrading_DeleteCommand" OnInsertCommand="grdGrading_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRIncidentProbabilityFrequency">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="IncidentProbabilityFrequency" HeaderText="Incident Probability Frequency"
                    UniqueName="IncidentProbabilityFrequency" SortExpression="IncidentProbabilityFrequency">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IncidentFollowUp" HeaderText="Incident Follow Up"
                    UniqueName="IncidentFollowUp" SortExpression="IncidentFollowUp">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RiskGradingName" HeaderText="Risk Grading"
                    UniqueName="RiskGradingName" SortExpression="RiskGradingName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="RiskGradingMtxItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdGradingEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
