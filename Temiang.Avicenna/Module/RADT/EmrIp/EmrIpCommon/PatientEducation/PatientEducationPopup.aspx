<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master"
    AutoEventWireup="true" CodeBehind="PatientEducationPopup.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PatientEducationPopup" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphEntry" runat="server">
    <asp:HiddenField runat="server" ID="hdfReturnValue" />
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%;">
                    <tr>
                        <td class="label">No</td>
                        <td>
                           <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="120px" Enabled="False" />&nbsp;-&nbsp;<telerik:RadTextBox ID="txtSeqNo" runat="server" Width="50px" Enabled="False" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date Time</td>
                        <td>
                            <telerik:RadDateTimePicker ID="txtEducationDateTime" runat="server" Width="160px" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Educator</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboEducationByUserID" Width="100%" EmptyMessage="Select a Educator" Enabled="False"
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="True" OnSelectedIndexChanged="cboEducationByUserID_OnSelectedIndexChanged">
                                <WebServiceSettings Method="Users" Path="~/WebService/ComboBoxDataService.asmx" />
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Type</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEducationType" runat="server" Width="50px" Enabled="False" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Problem</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationProblem" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Method</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationMethod" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Method</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtMethodOther" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Recipient</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtRecipientName" Width="100%">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Relationship</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRPatientEducationRecipient" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Evaluation</td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREducationEvaluation" Width="100%">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Education Duration (Minute)</td>
                        <td class="entry">
                            <telerik:RadNumericTextBox runat="server" ID="txtDuration" NumberFormat-DecimalDigits="0" Width="40px">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>

                </table>

            </td>
            <td style="vertical-align: top;">
                <fieldset>
                    <legend>Education</legend>

                    <telerik:RadGrid ID="grdPatientEducation" Width="100%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                        AllowMultiRowSelection="True"
                        OnItemDataBound="grdPatientEducation_ItemDataBound">
                        <MasterTableView DataKeyNames="ItemID" ShowHeader="true" ShowHeadersWhenNoRecords="false" Width="100%">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsSelected" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                                <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="" HeaderStyle-Width="200px" />
                                <telerik:GridBoundColumn DataField="EducationNotes" UniqueName="EducationNotes" HeaderText="Notes" />
                                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="NotesEdit">
                                    <ItemTemplate>
                                        <telerik:RadTextBox
                                            ID="txtNotes" runat="server"
                                            Width="100%">
                                        </telerik:RadTextBox>

                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                            <Resizing AllowColumnResize="False" />
                            <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
