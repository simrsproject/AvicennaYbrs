<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="CasemixCoverageList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.Casemix.CasemixCoverageList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">

            function gotoAddExceptionUrl() {
                var url = 'CasemixExceptionDialog.aspx?md=new&id=';
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title('New Exception');
                oWnd.show();
                oWnd.maximize();
            }

            function gotoEditExceptionUrl(id) {
                var url = 'CasemixExceptionDialog.aspx?md=edit&id=' + id;
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.set_title('Edit Exception');
                oWnd.show();
                oWnd.maximize();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) {
                    if (oWnd.argument.mode == 'reload') {
                        __doPostBack("<%= grdException.UniqueID %>", 'rebind');
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdException">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdException" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Behavior="Close, Move" ShowContentDuringLoad="False"
        Width="750px" Height="720px" VisibleStatusbar="false" Modal="true" ID="winDialog"
        OnClientClose="onClientClose" />
    <telerik:RadGrid ID="grdException" runat="server" OnNeedDataSource="grdException_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="25"
        AllowSorting="true">
        <MasterTableView Name="master" DataKeyNames="CasemixCoveredID" ClientDataKeyNames="CasemixCoveredID"
            GroupLoadMode="Client" CommandItemDisplay="Top">
            <CommandItemTemplate>
                <table width="100%">
                    <tr>
                        <td align="left">&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lbInsert" runat="server" OnClientClick="javascript:gotoAddExceptionUrl();return false;" Visible='<%# this.AddNewException%>'>
                                        <img style="border: 0px; vertical-align: middle;" alt="New" title="New" src="../../../../Images/Toolbar/new16.png" />
                                        &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="New"></asp:Label>
                                    </asp:LinkButton>
                        </td>
                        <td align="right"></td>
                    </tr>
                </table>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoEditExceptionUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>", DataBinder.Eval(Container.DataItem, "CasemixCoveredID"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="CasemixCoveredName" HeaderText="Name"
                    UniqueName="CasemixCoveredName" SortExpression="CasemixCoveredName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                    UniqueName="Notes" SortExpression="Notes">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
