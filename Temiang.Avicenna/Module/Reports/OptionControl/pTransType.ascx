<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pTransType.ascx.cs" 
Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pTransType" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Guarantor" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboTransType" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" > 
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Penerimaan" Value="D" />
                    <telerik:RadComboBoxItem runat="server" Text="Pengeluaran" Value="K" />
                </Items>        
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
