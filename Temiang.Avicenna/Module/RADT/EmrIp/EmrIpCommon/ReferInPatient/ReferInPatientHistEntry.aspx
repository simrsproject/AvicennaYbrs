<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogHistEntry.Master" AutoEventWireup="true"
    CodeBehind="ReferInPatientHistEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ReferInPatientHistEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
    <div class="divcaption" style="width: 100%;">Refer to Specialist History</div>

    <telerik:RadGrid ID="grdReferHist" runat="server" OnNeedDataSource="grdReferHist_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" Height="530px"
        OnItemCommand="grdReferHist_ItemCommand">
        <MasterTableView DataKeyNames="SequenceNo" ShowHeader="True" HierarchyDefaultExpanded="true">
            <NestedViewTemplate>
                <div style="padding-left: 20px; width: 98%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 33%">
                                <fieldset style="width: 90%">
                                    <legend>Examination Result</legend>
                                    <telerik:RadTextBox ID="txtActionExamTreatment" runat="server" Text='<%#Eval("ActionExamTreatment")%>' TextMode="MultiLine"
                                        Height="50px" Width="100%" ReadOnly="true" />
                                </fieldset>

                            </td>
                            <td style="width: 20%">
                                <fieldset style="width: 90%">
                                    <legend>Consultation Notes</legend>
                                    <telerik:RadTextBox ID="txtNotes" runat="server" Text='<%#Eval("Notes")%>' Width="100%" Height="50px"
                                        TextMode="MultiLine" ReadOnly="true" />
                                </fieldset>
                            </td>
                            <td>
                                <fieldset style="width: 90%">
                                    <legend>Consultation Result</legend>
                                    <telerik:RadTextBox ID="txtAnswer" runat="server" Text='<%#Eval("Answer")%>' Width="100%" Height="50px"
                                        TextMode="MultiLine" ReadOnly="true" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>


                </div>
            </NestedViewTemplate>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandName="View" ToolTip='View'>
                            <img src="../../../../../Images/Toolbar/views16.png" border="0" alt=""/>
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="ReferDateTime" HeaderText="Refer Date"
                    UniqueName="ReferDateTime" SortExpression="ReferDateTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="FromParamedicName" HeaderText="From Physician" UniqueName="FromParamedicName"
                    SortExpression="FromParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ToParamedicName" HeaderText="Refer To Physician" UniqueName="ToParamedicName"
                    SortExpression="ToParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <asp:HiddenField runat="server" ID="hdnSequenceNo" />
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Refer Date
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 103px">
                                        <telerik:RadDatePicker ID="txtReferDate" runat="server" Width="100px" />

                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtReferTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                        </telerik:RadMaskedTextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">Consult to
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboToParamedicID" runat="server" Width="304px" EmptyMessage="Select a Physician"
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                                <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReferParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="cboToParamedicID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Examination Result
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtActionExamTreatment" runat="server" Width="300px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                    <tr>
                        <td class="label">Consultation Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReferNotes" runat="server" Width="300px" MaxLength="500"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
