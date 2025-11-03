<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProcedureNameCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.ProcedureNameCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Diagnosis" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboProcedureName" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboProcedureName_ItemDataBound"
                OnItemsRequested="cboProcedureName_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ProcedureName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ProcedureID")%>) </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
