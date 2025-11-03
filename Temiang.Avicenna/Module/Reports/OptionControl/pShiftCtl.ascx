<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pShiftCtl.ascx.cs" 
Inherits="Temiang.Avicenna.Module.Reports.OptionControl.pShiftCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Shift" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboShift" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" > 
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Shift 1" Value="1" />
                    <telerik:RadComboBoxItem runat="server" Text="Shift 2" Value="2" />
                    <telerik:RadComboBoxItem runat="server" Text="Shift 3" Value="3" />
                </Items>        
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
