<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PrintDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PrintDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rblStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rblStatus" />
                    <telerik:AjaxUpdatedControl ControlID="txtPatientName" />
                    <telerik:AjaxUpdatedControl ControlID="txtRelationship" />
                    <telerik:AjaxUpdatedControl ControlID="rblMedicalInsuranceType" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:HiddenField runat="server" ID="hdnGender"/>
    <asp:HiddenField runat="server" ID="hdnMaritalStatus"/>
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <fieldset>
                    <legend>EMPLOYEE MEDICAL INSURANCE FORM</legend>
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblStatus" Text="Status" />
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="SELF" />
                                    <asp:ListItem Value="1" Text="FAMILY MEMBER" Selected="True" />
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblGuaranteeNumber" Text="Guarantee Number" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtGuaranteeNumber" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblEmployeeNumber" Text="Employee Number" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblEmployeeName" Text="Employee Name" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblPatientName" Text="Patient Name" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblRelationship" Text="Relationship" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRelationship" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblDate" Text="Date" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblServiceUnitID" Text="Polyclinic" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblSRMedicalInsuranceType" Text="Medical Insurance Type" />
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblMedicalInsuranceType" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label runat="server" ID="lblComplaint" Text="Complaint" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtComplaint" runat="server" Width="300px" Height="50px" TextMode="MultiLine" MaxLength="2000"/>
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
