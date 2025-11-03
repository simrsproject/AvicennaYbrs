<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalFamilyDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalFamilyDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="PersonalFamily" runat="server" ValidationGroup="PersonalFamilyDetail" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalFamilyDetail"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPersonalFamilyID" runat="server" Text="Personal Family ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPersonalFamilyID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonalFamilyID" runat="server" ErrorMessage="Personal Family ID required."
                            ControlToValidate="txtPersonalFamilyID" SetFocusOnError="True" ValidationGroup="PersonalFamily"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPatientID" runat="server" Text="Patient ID / Medical No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPatientID" runat="server" Width="150px" EnableLoadOnDemand="true"
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
                        &nbsp;/&nbsp;
                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="132px" MaxLength="20"
                            ReadOnly="True" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRFamilyRelation" runat="server" Text="Family Relation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRFamilyRelation" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRFamilyRelation" runat="server" ErrorMessage="Family Relation required."
                            ControlToValidate="cboSRFamilyRelation" SetFocusOnError="True" ValidationGroup="PersonalFamily"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFamilyName" runat="server" Text="Family Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtFamilyName" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFamilyName" runat="server" ErrorMessage="Family Name required."
                            ControlToValidate="txtFamilyName" SetFocusOnError="True" ValidationGroup="PersonalFamily"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblDateBirth" runat="server" Text="Date Birth"></asp:Label>
                    </td>
                    <td class="entry">
                        <table width="100%">
                            <tr>
                                <td style="width: 190px">
                                    <telerik:RadTextBox ID="txtPlaceBirth" runat="server" Width="180px" MaxLength="60" />
                                </td>
                                <td style="width: 10px">/
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtDateBirth" runat="server" Width="100px" MinDate="01/01/1900"
                                        MaxDate="12/31/2999" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDateBirth" runat="server" ErrorMessage="Date Birth required."
                            ControlToValidate="txtDateBirth" SetFocusOnError="True" ValidationGroup="PersonalFamily"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRGenderType" runat="server" Text="Gender"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="M" Text="Male" />
                            <asp:ListItem Value="F" Text="Female" />
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRGenderType" runat="server" ErrorMessage="Gender required."
                            ControlToValidate="rbtSex" SetFocusOnError="True" ValidationGroup="PersonalFamily"
                            Width="100%">
                            <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRMaritalStatus" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRMaritalStatus" runat="server" ErrorMessage="Marital Status required."
                            ControlToValidate="cboSRMaritalStatus" SetFocusOnError="True" ValidationGroup="PersonalFamily"
                            Width="100%">
                            <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRFamilyOccupation" runat="server" Text="Occupation"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRFamilyOccupation" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREducationLevel" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="No. BPJS Kesehatan"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBpjsKesehatanNo" runat="server" Width="300px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Coverage Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCoverageType" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCoverageClass" runat="server" Text="Coverage Class"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboCoverageClass" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCoverageClassBPJS" runat="server" Text="Coverage Class BPJS"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboCoverageClassBPJS" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblWeddingDate" runat="server" Text="Wedding Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtWeddingDate" runat="server" Width="100px" MinDate="01/01/1900"
                            MaxDate="12/31/2999" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" Height="80px" MaxLength="400"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Address required."
                            ControlToValidate="txtAddress" SetFocusOnError="True" ValidationGroup="PersonalFamily"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboZipCode" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboZipCode_ItemDataBound"
                            OnItemsRequested="cboZipCode_ItemsRequested" OnSelectedIndexChanged="cboZipCode_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "District")%>
                                &nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ZipPostalCode")%>) </b>
                                <br />
                                County :
                                <%# DataBinder.Eval(Container.DataItem, "County")%>
                                <br />
                                City :
                                <%# DataBinder.Eval(Container.DataItem, "City")%>
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
                        <asp:Label ID="lblDistrict" runat="server" Text="District"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDistrict" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCounty" runat="server" Text="County"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCounty" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCity" runat="server" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRState" runat="server" Text="State"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRState" runat="server" Width="300px" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblSRCity" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCity" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPhone" runat="server" Width="300px" MaxLength="20" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsGuaranteed" Text="Guaranteed" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalFamily"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PersonalFamily" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
