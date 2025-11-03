<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SafetyCultureIncidentReportsChronologyItem.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.SafetyCultureIncidentReportsChronologyItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSafetyCultureIncidentReportsChronology" runat="server" ValidationGroup="SafetyCultureIncidentReportsChronology" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SafetyCultureIncidentReportsChronology"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnSequenceNo" />
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="50%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChronologyDateTime" runat="server" Text="Date / Time" />
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtChronologyDate" runat="server" Width="100px" />
                                </td>
                                <td>
                                    <telerik:RadMaskedTextBox ID="txtChronologyTime" runat="server" Mask="<00..23>:<00..59>"
                                        PromptChar="_" RoundNumericRanges="false" Width="50px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvChronologyDate" runat="server" ErrorMessage="Date required."
                            ValidationGroup="SafetyCultureIncidentReportsChronology" ControlToValidate="txtChronologyDate" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChronologyDescription" runat="server" Text="Chronology Description" />
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtChronologyDescription" runat="server" Width="100%" TextMode="MultiLine" Height="250px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvChronologyDescription" runat="server" ErrorMessage="Chronology Description required."
                            ValidationGroup="SafetyCultureIncidentReportsChronology" ControlToValidate="txtChronologyDescription" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SafetyCultureIncidentReportsChronology"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="SafetyCultureIncidentReportsChronology" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="Top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSubjectPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboSubjectPersonID_ItemDataBound"
                            OnItemsRequested="cboSubjectPersonID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>)</b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:Button ID="btnAddSubject" runat="server" Text="Add" OnClick="btnAddSubject_Click" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadGrid ID="grdSubject" runat="server" AutoGenerateColumns="False" GridLines="None"
                            OnNeedDataSource="grdSubject_NeedDataSource" OnDeleteCommand="grdSubject_DeleteCommand">
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo, SubjectPersonID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="SubjectName" HeaderText="Subject Name"
                                        UniqueName="SubjectName" SortExpression="SubjectName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="false">
                                <Resizing AllowColumnResize="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
