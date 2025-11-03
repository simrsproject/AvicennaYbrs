<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="BpjsCheckoutList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsCheckoutList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinProcess(regno) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("BpjsCheckoutDialog.aspx?src=&noSep=" + regno);
                oWnd.show();
            }
            
            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winProcess" Animation="None" Width="400px" Height="200px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterNoKartu">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterNoSep">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%">
            <tr>
                <td class="label">
                    No Kartu
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtNoKartu" runat="server" Width="300px" />
                </td>
                <td width="20">
                    <asp:ImageButton ID="btnFilterNoKartu" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnFilter_Click" ToolTip="Search" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="noSEP" ClientDataKeyNames="noSEP">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                    <ItemTemplate>
                        <%# (this.IsUserEditAble.Equals(false) ? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "NoSEP")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="noSEP" HeaderText="No SEP"
                    UniqueName="noSEP" SortExpression="noSEP" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="tglSEP" HeaderText="Tgl SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="tglSEP" SortExpression="tglSEP"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="tglPulang" HeaderText="Tgl Pulang"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="tglPulang" SortExpression="TanggalLahir"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="jnsPelayanan" HeaderText="Jenis Pelayanan" UniqueName="jnsPelayanan"
                    SortExpression="jnsPelayanan" />
                <telerik:GridBoundColumn DataField="poliTujuan" HeaderText="Poli Tujuan" UniqueName="poliTujuan"
                    SortExpression="poliTujuan" />
                <telerik:GridBoundColumn DataField="diagnosa" HeaderText="Diagnosa" UniqueName="diagnosa"
                    SortExpression="diagnosa" />
                <telerik:GridNumericColumn DataField="biayaTagihan" HeaderText="Biaya Tagihan" HeaderStyle-HorizontalAlign="Center"
                    UniqueName="biayaTagihan" SortExpression="biayaTagihan" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
