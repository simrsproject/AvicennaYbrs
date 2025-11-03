<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="InternalSepList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.InternalSepList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterNoSep">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">No SEP
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtNoSep" Width="300px" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterNoSep" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterNoSep_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top"></td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" OnDeleteCommand="grdList_DeleteCommand"
        AllowSorting="true">
        <MasterTableView DataKeyNames="Nosep, Nosurat, Tglrujukinternal, Kdpolituj">
            <Columns>
                <telerik:GridBoundColumn DataField="Tglrujukinternal" HeaderText="Tgl Rujuk"
                    UniqueName="Tglrujukinternal" SortExpression="Tglrujukinternal">
                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nosep" HeaderText="No Sep"
                    UniqueName="Nosep" SortExpression="Nosep">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Tglsep" HeaderText="Tgl SEP"
                    UniqueName="Tglsep" SortExpression="Tglsep">
                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nosepref" HeaderText="No Sep Ref"
                    UniqueName="Nosepref" SortExpression="Nosepref">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nosurat" HeaderText="No Surat"
                    UniqueName="Nosurat" SortExpression="Nosurat">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nmpoliasal" HeaderText="Poli Asal"
                    UniqueName="Nmpoliasal" SortExpression="Nmpoliasal">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nmtujuanrujuk" HeaderText="Poli Tujuan"
                    UniqueName="Nmtujuanrujuk" SortExpression="Nmtujuanrujuk">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nmdokter" HeaderText="DPJP"
                    UniqueName="Nmdokter" SortExpression="Nmdokter">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Nmdiag" HeaderText="Diagnosa"
                    UniqueName="Nmdiag" SortExpression="Nmdiag">
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
