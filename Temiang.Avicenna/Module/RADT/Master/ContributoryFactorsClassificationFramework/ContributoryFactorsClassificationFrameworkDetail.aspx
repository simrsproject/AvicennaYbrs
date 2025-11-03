<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ContributoryFactorsClassificationFrameworkDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ContributoryFactorsClassificationFrameworkDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openComponent(fiid) {
                var ofid = $find("<%= txtFactorID.ClientID %>");
                var oWnd = $find("<%= winOpen.ClientID %>");
                oWnd.SetUrl("ContributoryFactorsClassificationFrameworkItemComponentList.aspx?fid=" + ofid.get_value() + "&fiid=" + fiid);
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
                            <asp:Label ID="lblFactorID" runat="server" Text="ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFactorID" runat="server" Width="100px" MaxLength="20">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFactorID" runat="server" ErrorMessage="ID required."
                                ControlToValidate="txtFactorID" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFactorName" runat="server" Text="Factor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFactorName" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFactorName" runat="server" ErrorMessage="Factor required."
                                ControlToValidate="txtFactorName" ValidationGroup="entry" SetFocusOnError="True"
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
    <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDetail_UpdateCommand"
        OnDeleteCommand="grdDetail_DeleteCommand" OnInsertCommand="grdDetail_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="Top" DataKeyNames="FactorID, FactorItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="FactorItemID" HeaderText="Factor Item ID" UniqueName="FactorItemID"
                    SortExpression="FactorItemID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FactorItemName" HeaderText="Factor Item Name" UniqueName="FactorItemName"
                    SortExpression="FactorItemName">
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
                        <%# string.Format("<a href=\"#\" onclick=\"openComponent('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Component\" /></a>",
                                                                                                                                        DataBinder.Eval(Container.DataItem, "FactorItemID"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="ContributoryFactorsClassificationFrameworkItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ContributoryFactorsClassificationFrameworkEditCommand">
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