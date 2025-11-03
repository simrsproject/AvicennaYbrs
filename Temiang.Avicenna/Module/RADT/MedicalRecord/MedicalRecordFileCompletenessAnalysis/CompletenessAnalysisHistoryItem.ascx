<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompletenessAnalysisHistoryItem.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileCompletenessAnalysis.CompletenessAnalysisHistoryItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMedicalRecordFileCompletenessHistory" runat="server" ValidationGroup="MedicalRecordFileCompletenessHistory" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="MedicalRecordFileCompletenessHistory"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top;width:50%">
            <table style="width:100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubmitDate" runat="server" Text="Submit Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtSubmitDate" runat="server" Width="100px" Enabled="false" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSubmitDate" runat="server" ErrorMessage="Submit Date required."
                            ControlToValidate="txtSubmitDate" SetFocusOnError="True" ValidationGroup="MedicalRecordFileCompletenessHistory" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubmitByUserID" runat="server" Text="Submit By"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSubmitByUserID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSubmitByUserID_ItemDataBound"
                            OnItemsRequested="cboSubmitByUserID_ItemsRequested" Enabled="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSubmitByUserID" runat="server" ErrorMessage="Submit By required."
                            ControlToValidate="cboSubmitByUserID" SetFocusOnError="True" ValidationGroup="MedicalRecordFileCompletenessHistory" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubmitNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSubmitNotes" runat="server" Width="300px" TextMode="MultiLine" MaxLength="1000" Height="50px"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSubmitNotes" runat="server" ErrorMessage="Submit Notes required."
                            ControlToValidate="txtSubmitNotes" SetFocusOnError="True" ValidationGroup="MedicalRecordFileCompletenessHistory" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="MedicalRecordFileCompletenessHistory"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="MedicalRecordFileCompletenessHistory"
                            Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top;width:50%">
            <table style="width:100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReturnDate" runat="server" Text="Return Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtReturnDate" runat="server" Width="100px" Enabled="false" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReturnByUserID" runat="server" Text="Return By"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboReturnByUserID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboReturnByUserID_ItemDataBound"
                            OnItemsRequested="cboReturnByUserID_ItemsRequested" Enabled="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReturnNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReturnNotes" runat="server" Width="300px" TextMode="MultiLine" MaxLength="1000" ReadOnly="true" Height="50px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>

</table>
