<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="FluidBalanceDetailEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.FluidBalanceDetailEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function OnClientSelectedIndexChanged(sender, args) {
            if (sender.get_selectedItem().get_value() != "INF") {
                sender.hideDropDown()
                args.set_cancel(true);
            }
        }
    </script>
    <table width="100%">
        <tr>
            <td class="label">Time
            </td>
            <td>
                <telerik:RadDateTimePicker ID="txtInOutDateTime" runat="server" Width="160px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblInOutMethod" runat="server" Text="Fluid Method"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRFluidInOutMethod" runat="server" Width="304px"/>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Schema Infus"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSchemaInfus" runat="server" Width="304px"></telerik:RadComboBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFluidName" runat="server" Width="304px" MaxLength="100" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr style="display: none">
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Fluid Volume"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtFluidQty" runat="server" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Qty"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtInOutQty" runat="server" />
            </td>
            <td width="20px"></td>
        </tr>


    </table>
</asp:Content>
