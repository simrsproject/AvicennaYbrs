<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MembershipMemberItem.ascx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipMemberItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MembershipMember"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%" valign="top">
            <table width="100%">
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
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label></td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="15"
                            ReadOnly="true" /></td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPatientName" runat="server" Text="Member Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                </td>
                                <td style="width: 3px"></td>
                                <td>
                                    <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                </td>
                                <td style="width: 3px"></td>
                                <td>
                                    <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td class="entry" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="MembershipMember"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MembershipMember" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width=50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
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
                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="true"
                            TextMode="MultiLine" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No / Mobile Phone No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="148px" ReadOnly="true" />
                        <telerik:RadTextBox ID="txtMobilePhone" runat="server" Width="150px" ReadOnly="true" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>

</table>
