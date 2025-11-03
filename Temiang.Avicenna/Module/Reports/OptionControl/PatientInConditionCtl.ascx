<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientInConditionCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PatientInConditionCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Item Medical" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPatientInCondition" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboPatientInCondition_ItemDataBound"
                OnItemsRequested="cboPatientInCondition_ItemsRequested">
                <ItemTemplate>
                    <b>               
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
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
