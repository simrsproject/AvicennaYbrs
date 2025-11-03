<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="ComplaintResponseTimeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.Complaint.ComplaintResponseTimeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblComplaintNo" runat="server" Text="Complaint No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtComplaintNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvComplaintNo" runat="server" ErrorMessage="Complaint No required."
                                ValidationGroup="entry" ControlToValidate="txtComplaintNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblComplaintDate" runat="server" Text="Complaint Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtComplaintDate" runat="server" Width="100px" OnSelectedDateChanged="txtComplaintDate_SelectedDateChanged"
                                AutoPostBack="True">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvComplaintDate" runat="server" ErrorMessage="Complaint Date required."
                                ValidationGroup="entry" ControlToValidate="txtComplaintDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCustomerName" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ErrorMessage="Customer Name required."
                                ValidationGroup="entry" ControlToValidate="txtCustomerName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientID" runat="server" Text="Patient ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPatientID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboPatientID_ItemDataBound"
                                OnItemsRequested="cboPatientID_ItemsRequested" OnSelectedIndexChanged="cboPatientID_SelectedIndexChanged">
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
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="243px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="23px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCustomerAddress" runat="server" Text="Customer Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtCustomerAddress" runat="server" Width="300px" TextMode="MultiLine" Height="50px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                OnItemsRequested="cboServiceUnitID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblComplaintDescription" runat="server" Text="Complaint Description"></asp:Label>
                        </td>
                        <td class="entry" colspan="3">
                            <telerik:RadTextBox ID="txtComplaintDescription" runat="server" Width="95%" TextMode="MultiLine" Height="270px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label1" runat="server" Text="REPORT RECEIVED"></asp:Label>
                    </legend>
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRComplaintRiskGrading" runat="server" Text="Risk Grading"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblSRComplaintRiskGrading" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="1" Text="Merah" />
                                    <asp:ListItem Value="3" Text="Kuning" />
                                    <asp:ListItem Value="7" Text="Hijau" />
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvSRComplaintRiskGrading" runat="server" ErrorMessage="Risk Grading required."
                                    ValidationGroup="entry" ControlToValidate="rblSRComplaintRiskGrading" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label"><asp:Label ID="lblReportReceived" runat="server" Text="Report Received"></asp:Label></td>
                            <td class="entry" colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td class="label" style="width:100px">
                                            <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                        </td>
                                        <td style="width:2px"></td>
                                        <td class="label" style="width:300px">
                                            <asp:Label ID="lblBy" runat="server" Text="By"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">&nbsp;&nbsp;
                                <asp:Label ID="lblMarketing" runat="server" Text="Marketing"></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td style="width:100px">
                                            <telerik:RadDatePicker ID="txtReportReceivedMarketingDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvReportReceivedMarketingDate" runat="server" ErrorMessage="Report Received Date - Marketing required."
                                                ValidationGroup="entry" ControlToValidate="txtReportReceivedMarketingDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtReportReceivedMarketingBy" runat="server" Width="300px" MaxLength="255" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvReportReceivedMarketingBy" runat="server" ErrorMessage="Received By required."
                                                ValidationGroup="entry" ControlToValidate="txtReportReceivedMarketingBy" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">&nbsp;&nbsp;
                                <asp:Label ID="lblRelatedUnit" runat="server" Text="Related Unit"></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtReportReceivedUnitDate" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td width="10px">
                                            <asp:RequiredFieldValidator ID="rfvReportReceivedUnitDate" runat="server" ErrorMessage="Report Received Date - Related Unit required."
                                                ValidationGroup="entry" ControlToValidate="txtReportReceivedUnitDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtReportReceivedUnitBy" runat="server" Width="300px" MaxLength="255" />
                                        </td>
                                        <td width="10px">
                                            <asp:RequiredFieldValidator ID="rfvReportReceivedUnitBy" runat="server" ErrorMessage="Received By required."
                                                ValidationGroup="entry" ControlToValidate="txtReportReceivedUnitBy" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td width="50%" valign="top">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblCorrectiveActionFieldset" runat="server" Text="CORRECTIVE ACTION"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCorrectiveActionDate" runat="server" Text="Corrective Action Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtCorrectiveActionDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvCorrectiveActionDate" runat="server" ErrorMessage="Corrective Action Date required."
                                    ValidationGroup="entry" ControlToValidate="txtCorrectiveActionDate" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCorrectiveActionBy" runat="server" Text="Corrective Action By"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtCorrectiveActionBy" runat="server" Width="300px" MaxLength="255" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvCorrectiveActionBy" runat="server" ErrorMessage="Corrective Action By required."
                                    ValidationGroup="entry" ControlToValidate="txtCorrectiveActionBy" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCorrectiveAction" runat="server" Text="Corrective Action"></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <telerik:RadTextBox ID="txtCorrectiveAction" runat="server" Width="95%" Height="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPreventiveAction" runat="server" Text="Preventive Action"></asp:Label>
                            </td>
                            <td class="entry" colspan="3">
                                <telerik:RadTextBox ID="txtPreventiveAction" runat="server" Width="95%" Height="80px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>

        </tr>
    </table>
</asp:Content>
