<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ClassMenuSettingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.ClassMenuSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblClassID" runat="server" Text="Class ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="10" ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class ID ID required."
                    ValidationGroup="entry" ControlToValidate="txtClassID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblClassName" runat="server" Text="Class Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtClassName" runat="server" Width="300px" ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvClassName" runat="server" ErrorMessage="Class Name required."
                    ValidationGroup="entry" ControlToValidate="txtClassName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsOptional" Text="Optional" runat="server" />
            </td>
            <td width="20">
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
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SRMealSet">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SRMealSet" HeaderText="ID"
                    UniqueName="SRMealSet" SortExpression="SRMealSet" />
                <telerik:GridBoundColumn DataField="MealSetName" HeaderText="Meal Set" UniqueName="MealSetName"
                    SortExpression="MealSetName" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsOptional" HeaderText="Optional"
                    UniqueName="IsOptional" SortExpression="IsOptional" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ClassMenuSettingItemDetail.ascx" EditFormType="WebUserControl">
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
