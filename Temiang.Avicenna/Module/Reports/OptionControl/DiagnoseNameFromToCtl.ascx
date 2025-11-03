<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiagnoseNameFromToCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.DiagnoseNameFromToCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Diagnosis From" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboDiagnoseNameFrom" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboDiagnoseName_ItemDataBound"
                OnItemsRequested="cboDiagnoseName_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "DiagnoseName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>) </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption2" runat="server" Text="To Diagnosis" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboDiagnoseNameTo" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboDiagnoseName_ItemDataBound"
                OnItemsRequested="cboDiagnoseName_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "DiagnoseName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "DiagnoseID")%>) </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>