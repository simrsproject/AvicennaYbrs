<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RL4EducationLevelDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.HRBase.RL4.RL4EducationLevelDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openEducationMajor(eduId, md) {
                var oTypeId = $find("<%= txtRL4TypeID.ClientID %>");
                var oProfId = $find("<%= txtRL4ProfessionTypeID.ClientID %>");
                var oWnd = $find("<%= winOpen.ClientID %>");
                oWnd.SetUrl("RL4EducationMajorDialog.aspx?typeId=" + oTypeId.get_value() + "&profId=" + oProfId.get_value() + "&eduId=" + eduId + "&md=" + md);
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winOpen" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEducationLevel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEducationLevel" />
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
                            <asp:Label ID="lblRL4Type" runat="server" Text="RL4 Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRL4TypeID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblRL4TypeName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRL4ProfessionType" runat="server" Text="RL4 Profession Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRL4ProfessionTypeID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblRL4ProfessionTypeName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdEducationLevel" runat="server" OnNeedDataSource="grdEducationLevel_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEducationLevel_UpdateCommand"
                    OnDeleteCommand="grdEducationLevel_DeleteCommand" OnInsertCommand="grdEducationLevel_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ItemID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn UniqueName="process">
                                <ItemTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"openEducationMajor('{0}', 'view'); return false;\"><img src=\"../../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Education Major\" /></a>",
                                                                                                                                        DataBinder.Eval(Container.DataItem, "ItemID"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="process">
                                <ItemTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"openEducationMajor('{0}', 'edit'); return false;\"><img src=\"../../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit Education Major\" /></a>",
                                                                                                                                        DataBinder.Eval(Container.DataItem, "ItemID"))%>
                                </ItemTemplate>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ItemID" HeaderText="ID"
                                UniqueName="ItemID" SortExpression="ItemID">
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ItemName" HeaderText="Education Level"
                                UniqueName="ItemName" SortExpression="ItemName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="RL4EducationLevelItem.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="RL4EducationLevelItemEditCommand">
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
            </td>
        </tr>
    </table>
</asp:Content>
