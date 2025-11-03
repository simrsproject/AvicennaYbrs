<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="AssetItemSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.AssetItemSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblAssetGropName" runat="server" Text="Asset Group" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboAssetGroupID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAssetID" runat="server" Text="Asset ID" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAssetID" runat="server" Width="300px" MaxLength="30" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAssetName" runat="server" Text="Asset Name" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAssetName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMaintenanceUnit" runat="server" Text="Maintenance Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboMaintenanceServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPoReceivedNo" runat="server" Text="PO Received No" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPoReceivedNo" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td></td>
        </tr>
        <tr runat="server" id="trRbtIsFixedAssetStatus">
            <td class="label"></td>
            <td class="filter"></td>
            <td class="entry">
                <asp:RadioButtonList ID="rbtIsFixedAssetStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="" Text="All"/>
                    <asp:ListItem Value="1" Text="Fixed Asset" />
                    <asp:ListItem Value="0" Text="Inventory Item" />
                </asp:RadioButtonList>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
