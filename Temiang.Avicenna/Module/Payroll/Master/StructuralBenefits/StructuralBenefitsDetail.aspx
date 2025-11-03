<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="StructuralBenefitsDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.StructuralBenefitsDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="lblOrganizationUnitID" runat="server" Text="Organization Unit ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtOrganizationUnitID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvOrganizationUnitID" runat="server" ErrorMessage="Organization Unit ID required."
                    ValidationGroup="entry" ControlToValidate="txtOrganizationUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOrganizationUnitCode" runat="server" Text="Organization Unit Code"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtOrganizationUnitCode" runat="server" Width="300px" ReadOnly="True" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvOrganizationUnitCode" runat="server" ErrorMessage="Organization Unit Code required."
                    ValidationGroup="entry" ControlToValidate="txtOrganizationUnitCode" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOrganizationUnitName" runat="server" Text="Organization Unit Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtOrganizationUnitName" runat="server" Width="300px" ReadOnly="True" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvOrganizationUnitName" runat="server" ErrorMessage="* if this Organization Unit top level"
                    ValidationGroup="entry" ControlToValidate="txtOrganizationUnitName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        OnInsertCommand="grdItem_InsertCommand" OnDeleteCommand="grdItem_DeleteCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionID, ValidFrom">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" UniqueName="PositionName"
                    SortExpression="PositionName" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                    UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="StructuralBenefitsItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings>
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
