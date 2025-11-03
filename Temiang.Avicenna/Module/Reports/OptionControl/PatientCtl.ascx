<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientCtl.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Reports.OptionControl.PatientCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 100px">
            <asp:Label ID="lblCaption" runat="server" Text="Patient" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="100%" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                OnItemsRequested="cboPatientID_ItemsRequested" >
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                    </b>&nbsp;-&nbsp;
                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                    <br />
                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                    &nbsp;|&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                    <br />
                    Address :
                    <%# DataBinder.Eval(Container.DataItem, "Address")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 5 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
