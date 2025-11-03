<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LabOrderNoCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.Reports.OptionControl.LabOrderNoCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<table style="width: 90%">
    <tr>
        <td style="width: 5px"></td>
        <td style="width: 100px">
            <asp:Label ID="lblPatientID" runat="server" Text="Patient Name" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="100%" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                    </b>&nbsp;-&nbsp;DoB :&nbsp;
                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                    <br />
                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                    &nbsp;|&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                    <br />
                    Address : <%# DataBinder.Eval(Container.DataItem, "Address")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px"></td>
        <td style="width: 100px">
            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="100%" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                OnItemDataBound="cboRegistrationNo_ItemDataBound" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                    </b>&nbsp;-&nbsp;
                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                    <br />
                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td style="width: 5px"></td>
        <td style="width: 100px">
            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No" />
        </td>
        <td>
            <telerik:RadComboBox ID="cboTransactionNo" runat="server" Width="100%" EnableLoadOnDemand="true"
                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboTransactionNo_ItemDataBound"
                OnItemsRequested="cboTransactionNo_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>
                    </b>&nbsp;-&nbsp;
                    <%# System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate")).ToString(Temiang.Avicenna.Common.AppConstant.DisplayFormat.Date)%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
    </tr>
</table>
