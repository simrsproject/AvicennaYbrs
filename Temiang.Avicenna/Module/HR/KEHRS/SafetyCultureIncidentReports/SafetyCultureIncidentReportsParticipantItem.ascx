<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SafetyCultureIncidentReportsParticipantItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.SafetyCultureIncidentReportsParticipantItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSafetyCultureIncidentReportsParticipant" runat="server" ValidationGroup="SafetyCultureIncidentReportsParticipant" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SafetyCultureIncidentReportsParticipant"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>

                <tr>
                    <td class="label">
                        <asp:Label ID="lblPersonID" runat="server" Text="Participant Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                            OnItemsRequested="cboPersonID_ItemsRequested">
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
                        <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Participant Name required."
                            ControlToValidate="cboPersonID" SetFocusOnError="True" ValidationGroup="SafetyCultureIncidentReportsParticipant"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRParticipantStatus" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRParticipantStatus" runat="server" Width="300px" />
                    </td>
                    <td width="20" />
                    <td />
                </tr>
                
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" Height="100px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SafetyCultureIncidentReportsParticipant"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="SafetyCultureIncidentReportsParticipant" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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
