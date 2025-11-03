<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicFeeCategoryCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ParamedicFeeCategoryCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Type" />
        </td>
        <td>
             <telerik:RadComboBox ID="cboSRParamedicFeeCategory" runat="server" Width="100%">
             </telerik:RadComboBox>
        </td>
    </tr>
</table>