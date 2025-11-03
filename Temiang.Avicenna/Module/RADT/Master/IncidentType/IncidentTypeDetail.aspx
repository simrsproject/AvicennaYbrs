<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="IncidentTypeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.IncidentTypeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openSubComponent(compId) {
                var oType = $find("<%= txtItemID.ClientID %>");
                var oWnd = $find("<%= winOpen.ClientID %>");
                oWnd.SetUrl("IncidentTypeSubComponentList.aspx?type=" + oType.get_value() + "&compId=" + compId);
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
                            <asp:Label ID="lblItemName" runat="server" Text="Incident Type"></asp:Label>
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                        </td>
                        <td width="20">
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
    <telerik:RadGrid ID="grdIncidentType" runat="server" OnNeedDataSource="grdIncidentType_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdIncidentType_UpdateCommand"
        OnDeleteCommand="grdIncidentType_DeleteCommand" OnInsertCommand="grdIncidentType_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRIncidentType,ComponentID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="ComponentID" HeaderText="Component ID" UniqueName="ComponentID"
                    SortExpression="ItemID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ComponentName" HeaderText="Component Name" UniqueName="ComponentName"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn UniqueName="process">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openSubComponent('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Setting Sub Component\" /></a>",
                                                                                                                                        DataBinder.Eval(Container.DataItem, "ComponentID"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="IncidentTypeComponentDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="grdIncidentTypeEditCommand">
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
