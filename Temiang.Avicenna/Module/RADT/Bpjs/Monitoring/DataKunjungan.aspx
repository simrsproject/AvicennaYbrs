<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="DataKunjungan.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.Monitoring.DataKunjungan" %>

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
            <telerik:AjaxSetting AjaxControlID="btnFilterTglPelayanan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterJenisPelayanan">
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
                            <td class="label">Tgl Pelayanan
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker runat="server" ID="txtTglPelayanan" Width="100px" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterTglPelayanan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterTglPelayanan_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Jenis Pelayanan
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPelayanan" runat="server" Width="304px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="1" Text="Rawat Inap" Selected="true" />
                                        <telerik:RadComboBoxItem Value="2" Text="Rawat Jalan" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterJenisPelayanan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterTglPelayanan_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top"></td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" GridLines="None" AutoGenerateColumns="true" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="NoSep">
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
