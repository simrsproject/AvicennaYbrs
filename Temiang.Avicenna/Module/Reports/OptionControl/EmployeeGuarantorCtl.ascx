<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeGuarantorCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.EmployeeGuarantorCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Guarantor" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboGuarantorID" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                OnItemsRequested="cboGuarantor_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "GuarantorName")%>
                    </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>