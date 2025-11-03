<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RiskFactorsDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.RiskFactorsDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Risk Factors ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" MaxLength="20">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Risk Factors ID required."
                                ControlToValidate="txtItemID" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Risk Factors Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Risk Factors Name required."
                                ControlToValidate="txtItemName" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20px">
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
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        OnDeleteCommand="grdItem_DeleteCommand" OnInsertCommand="grdItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRRiskFactorsType, RiskFactorsID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="RiskFactorsID" HeaderText="ID" UniqueName="RiskFactorsID"
                    SortExpression="RiskFactorsID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RiskFactorsName" HeaderText="Risk Factors" UniqueName="RiskFactorsName"
                    SortExpression="RiskFactorsName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="RiskFactorsItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdRiskFactorsEditCommand">
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