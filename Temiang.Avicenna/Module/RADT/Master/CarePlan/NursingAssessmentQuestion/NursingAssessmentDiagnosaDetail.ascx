<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NursingAssessmentDiagnosaDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingAssessmentDiagnosaDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumNursingAssessmentDiagnosa" runat="server" BackColor="PapayaWhip"
    Font-Size="Small" BorderColor="#FF8000" BorderStyle="Solid" ValidationGroup="NursingAssessmentDiagnosa" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NursingAssessmentDiagnosa"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td style="width:50%; vertical-align:top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNursingDiagnosaID" runat="server" Text="Diagnosa"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboNursingDiagnosaID" Width="300px" AutoPostBack="True"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemsRequested="cboNursingDiagnosaID_ItemsRequested" OnItemDataBound="cboNursingDiagnosaID_ItemDataBound" 
                                OnSelectedIndexChanged="cboNursingDiagnosaID_SelectedIndexChanged" >
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvNursingDiagnosaID" runat="server" ErrorMessage="Nursing Diagnosa required."
                                ControlToValidate="cboNursingDiagnosaID" SetFocusOnError="True" ValidationGroup="NursingAssessmentDiagnosa"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Age Start - Age End"></asp:Label>
                        </td>
                        <td class="entry" colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtAgeStart" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="40px"></telerik:RadNumericTextBox>
                                                    <telerik:RadComboBox runat="server" ID="cboAgeStartConversion" Width="80px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Month" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Year" Value="12" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Age start required."
                                                        ControlToValidate="txtAgeStart" SetFocusOnError="True" ValidationGroup="NursingAssessmentDiagnosa"
                                                        Width="100%">
                                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                <td>
                                                <td>&nbsp;&nbsp;-&nbsp;&nbsp;</td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtAgeEnd" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="40px"></telerik:RadNumericTextBox>
                                                    <telerik:RadComboBox runat="server" ID="cboAgeEndConversion" Width="80px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Month" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Year" Value="12" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                            </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Age end required."
                                                        ControlToValidate="txtAgeEnd" SetFocusOnError="True" ValidationGroup="NursingAssessmentDiagnosa"
                                                        Width="100%">
                                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Sex"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSex" Width="300px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="" />
                                    <telerik:RadComboBoxItem Value="M" Text="M" />
                                    <telerik:RadComboBoxItem Value="F" Text="F" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trPrefSuf" runat="server" visible="false">
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkShowInPrefix" runat="server" Text="Show In Prefix" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkShowInSuffix" runat="server" Text="Show In Suffix" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trMandatoryType" runat="server" visible="false">
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Mandatory Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:RadioButtonList ID="rbAssLevel" runat="server" RepeatDirection="Horizontal" >
                            </asp:RadioButtonList>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trAdditional" runat="server" visible="false">
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Additional Diagnosis Prefix"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRDiagPrefix" Width="300px" AutoPostBack="True"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemsRequested="cboSRDiagPrefix_ItemsRequested" OnItemDataBound="cboSRDiagPrefix_ItemDataBound" >
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trAddSuff" runat="server" visible="false">
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Additional Diagnosis Suffix"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRDiagSuffix" Width="300px" AutoPostBack="True"
                            EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                            OnItemsRequested="cboSRDiagSuffix_ItemsRequested" OnItemDataBound="cboSRDiagPrefix_ItemDataBound" >
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:50%; vertical-align:top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Checked"></asp:Label>
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkCheck" runat="server" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkUsingRange" runat="server" Text="Using Range" 
                            AutoPostBack="true" OnCheckedChanged="chkUsingRange_OnCheckedChanged" />
                        </td>
                        <td width="20px">
                        
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Operand"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboOperand" Width="300px">
                                <Items>
                                    <telerik:RadComboBoxItem Value="=" Text="=" />
                                    <telerik:RadComboBoxItem Value="=" Text=">" />
                                    <telerik:RadComboBoxItem Value="=" Text="<" />
                                    <telerik:RadComboBoxItem Value="=" Text="Like" />
                                    <telerik:RadComboBoxItem Value="=" Text="Not Like" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Numeric Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtNumAceptedValue" runat="server" Value="0" Width="130px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtNumAceptedValue2" runat="server" Value="0" Width="130px">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Text Value(s)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAcceptedValues" runat="server" Width="300px" 
                                MaxLength="350" TextMode="MultiLine" >
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px" colspan="2">
                            <asp:Label ID="lblSeparatedWith" runat="server" Text="Separated with |" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" style="height: 26px">
                            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="NursingAssessmentDiagnosa"
                                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                                ValidationGroup="NursingAssessmentDiagnosa" Visible='<%# DataItem is GridInsertionObject %>'>
                            </asp:Button>
                            &nbsp;
                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                CommandName="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

