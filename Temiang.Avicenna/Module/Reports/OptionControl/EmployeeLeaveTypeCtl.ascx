<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeLeaveTypeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.EmployeeLeaveTypeCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px"></td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Employee Leave Type" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboSREmployeeLeaveType" runat="server" Width="100%" HighlightTemplatedItems="True"
                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboSREmployeeLeaveType_ItemDataBound"
                OnItemsRequested="cboSREmployeeLeaveType_ItemsRequested" DataTextField="ItemName" DataValueField="ItemID">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                    </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 result
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
