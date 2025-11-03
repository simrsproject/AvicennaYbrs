<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SafetyCultureIncidentReportsSubjectItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.SafetyCultureIncidentReportsSubjectItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSafetyCultureIncidentReportsSubject" runat="server" ValidationGroup="SafetyCultureIncidentReportsSubject" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SafetyCultureIncidentReportsSubject"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                            OnItemsRequested="cboPersonID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                            ControlToValidate="cboPersonID" SetFocusOnError="True" ValidationGroup="SafetyCultureIncidentReportsSubject"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRProfessionType" runat="server" Text="Profession Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRProfessionType" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" Enabled="false" />
                    </td>
                    <td width="20" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOrganizationUnitID" runat="server" Text="Department"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboOrganizationUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubOrganizationUnitID" runat="server" Text="Division"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSubOrganizationUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false" >
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="trSubDivision">
                    <td class="label">
                        <asp:Label ID="lblSubDivisonID" runat="server" Text="Sub Division"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSubDivisonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblServiceUnit" runat="server" Text="Section / Service Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsMainSubject" runat="server" Text="Main Subject" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SafetyCultureIncidentReportsSubject"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="SafetyCultureIncidentReportsSubject" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                
            </table>
        </td>
    </tr>
</table>
