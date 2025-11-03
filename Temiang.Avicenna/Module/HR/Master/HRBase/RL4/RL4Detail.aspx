<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RL4Detail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.HRBase.RL4.RL4Detail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openEducationLevel(profId, md) {
                var oTypeId = $find("<%= txtRL4TypeID.ClientID %>");
                var oWnd = $find("<%= winOpen.ClientID %>");
                oWnd.SetUrl("RL4EducationLevelDialog.aspx?typeId=" + oTypeId.get_value() + "&profId=" + profId + "&md=" + md);
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winOpen" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRL4TypeID" runat="server" Text="RL4 Type ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRL4TypeID" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRL4TypeID" runat="server" ErrorMessage="RL4 Type ID required."
                                ValidationGroup="entry" ControlToValidate="txtRL4TypeID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRL4TypeName" runat="server" Text="RL4 Type Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRL4TypeName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRL4TypeName" runat="server" ErrorMessage="RL4 Type Name required."
                                ValidationGroup="entry" ControlToValidate="txtRL4TypeName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdItem_DeleteCommand"
        OnInsertCommand="grdItem_InsertCommand" OnUpdateCommand="grdItem_UpdateCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridTemplateColumn UniqueName="process">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openEducationLevel('{0}', 'view'); return false;\"><img src=\"../../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Education Level\" /></a>",
                                                                                                                                        DataBinder.Eval(Container.DataItem, "ItemID"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="process">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openEducationLevel('{0}', 'edit'); return false;\"><img src=\"../../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit Education Level\" /></a>",
                                                                                                                                        DataBinder.Eval(Container.DataItem, "ItemID"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Profession Type"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="RL4ProfessionTypeItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="RL4ProfessionTypeItemEditCommand">
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