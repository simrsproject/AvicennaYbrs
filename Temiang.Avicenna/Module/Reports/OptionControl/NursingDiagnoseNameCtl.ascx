<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NursingDiagnoseNameCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.NursingDiagnoseNameCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Nursing Diagnosis" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboNursingDiagnoseName" Width="100%" runat="server" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboNursingDiagnoseName_ItemDataBound"
                OnItemsRequested="cboNursingDiagnoseName_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "NursingDiagnosaName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "NursingDiagnosaID")%>) </b>
                    <br />
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
