<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeRemunByIdiSettingSearch.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeRemunByIdiSettingSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFilterItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="ID" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtID" runat="server" Width="50px" NumberFormat-DecimalDigits="0" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label12" runat="server" Text="Smf" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSmf" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Paramedic" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboParamedic" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
       
        <tr>
            <td class="label">
                <asp:Label ID="Label9" runat="server" Text="Item Group" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItemGroup" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Item" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterItem" runat="server" Width="100px" AutoPostBack="true"  
                    OnSelectedIndexChanged="cboFilterItem_SelectedIndexChanged">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItem" runat="server" Width="300px" EnableLoadOnDemand="True"
                    HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboItem_ItemDataBound"
                    OnItemsRequested="cboItem_ItemsRequested">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
