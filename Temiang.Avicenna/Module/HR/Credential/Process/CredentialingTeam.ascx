<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CredentialingTeam.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.CredentialingTeam" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCredentialProcessTeam" runat="server" ValidationGroup="CredentialProcessTeam" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CredentialProcessTeam"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPersonID" runat="server" Text="Team Member"></asp:Label>
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
                        <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Team Member required."
                            ControlToValidate="cboPersonID" SetFocusOnError="True" ValidationGroup="CredentialProcessTeam" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboPositionID_ItemDataBound" Enabled="false">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRCredentialingTeam" runat="server" Text="Team Status"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRCredentialingTeam" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRCredentialingTeam" runat="server" ErrorMessage="Team Status required."
                            ControlToValidate="cboSRCredentialingTeam" SetFocusOnError="True" ValidationGroup="CredentialProcessTeam" Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAreasOfExpertise" runat="server" Text="Areas Of Expertise"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAreasOfExpertise" runat="server" Width="300px" TextMode="MultiLine" MaxLength="255"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAreasOfExpertise" runat="server" ErrorMessage="Areas Of Expertise required."
                            ControlToValidate="txtAreasOfExpertise" SetFocusOnError="True" ValidationGroup="CredentialProcessTeam" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CredentialProcessTeam"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="CredentialProcessTeam"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top"></td>
    </tr>

</table>
