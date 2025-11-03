<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EducationIntegratedCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EducationIntegratedCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend><b>EDUCATION</b></legend>
    <table style="width: 100%">
        <tr>
            <td class="label">Method</td>
            <td style="width: 240px">
                <telerik:RadComboBox runat="server" ID="cboSREducationMethod" Width="100%">
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtMethodOther" Width="100%">
                </telerik:RadTextBox></td>
        </tr>
        <tr>
            <td class="label">Education Recipient</td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboSRPatientEducationRecipient" Width="100%">
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox ID="txtRecipientName" runat="server" Width="100%"></telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="label">Evaluation</td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboSRPatientEducationEvaluation" Width="100%">
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPatientEducationEvaluationOth" runat="server" Width="100%"></telerik:RadTextBox></td>
        </tr>
        <tr>
            <td class="label">Goal</td>
            <td >
                <telerik:RadComboBox runat="server" ID="cboSRPatientEducationGoal" Width="100%">
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtPatientEducationGoalOth" Width="100%">
                </telerik:RadTextBox></td>
        </tr>
        <tr>
            <td class="label">Duration (Min)</td>
            <td>
                <telerik:RadNumericTextBox runat="server" ID="txtDuration" NumberFormat-DecimalDigits="0" Width="100px">
                </telerik:RadNumericTextBox>
            </td>
            <td></td>
        </tr>
<%--        <tr>
            <td class="label">Verificator</td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtVerificator" Width="100%">
                </telerik:RadTextBox>
            </td>
            <td></td>
        </tr>--%>
        <tr>
            <td valign="top" class="label">Education</td>
            <td colspan="2">
                <telerik:RadGrid ID="grdPatientEducation" Width="100%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                    GridLines="None" Skin=""
                    OnItemDataBound="grdPatientEducation_ItemDataBound">
                    <MasterTableView DataKeyNames="ItemID" ShowHeader="False" Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="40px">
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
                                        ID="txtNotes" runat="server" TextMode="MultiLine" Height="50px"
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

            </td>
        </tr>

    </table>
</fieldset>
