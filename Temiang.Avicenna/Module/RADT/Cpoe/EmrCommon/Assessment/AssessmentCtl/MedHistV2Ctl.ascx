<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedHistV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.MedHistV2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<table style="width: 100%; padding: 0 0 0 0;">
    <tr>
        <td style="width: 50%;">
            <table style="width: 100%;">
                <tr>
                    <td valign="top" class="label">Past Medical History</td>
                    <td>
                        <telerik:RadTextBox ID="txtPastmedical" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" />
                    </td>
                </tr>
                                <tr>
                    <td valign="top" class="label">Family Medical History</td>
                    <td>
                        <telerik:RadTextBox ID="txtFamilyMedical" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="label">Surgical History</td>
                    <td>
                        <telerik:RadTextBox ID="txtSurgicalHistory" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="label">Job History<br />
                        Is the patient's work related to harmful substances (for example: Chemistry, Gas, etc.). Explain
                                        <asp:LinkButton runat="server" ID="lbtnPrevJobHistNotes" OnClick="lbtnPrevJobHistNotes_OnClick" OnClientClick="if (!confirm('Copy Job History OutPatient')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                                        </asp:LinkButton>

                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtJobHistNotes" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="label">Transfusion History</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td class="label">Year</td>
                                <td>
                                    <telerik:RadTextBox ID="txtYearOfTransfusion" runat="server" Width="100px" MaxLength="4" /></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Allergic Reaction</td>
                                <td>
                                    <telerik:RadTextBox ID="txtAllergicReactionOfTransfusion" runat="server" Width="100%" Height="80px"
                                        TextMode="MultiLine" />
                                </td>
                                <td></td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%;" valign="top">
            <table style="width: 100%;">
                <tr>
                    <td valign="top" class="label">Allergies</td>
                    <td>
                        <telerik:RadGrid ID="grdPatientAllergy" runat="server" AutoGenerateColumns="False" EnableViewState="true" Width="100%"
                            OnNeedDataSource="grdPatientAllergy_NeedDataSource" Skin="" GridLines="None">
                            <MasterTableView DataKeyNames="ItemID" ShowHeader="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="StandardReferenceID" Display="False" UniqueName="StandardReferenceID" />
                                    <telerik:GridBoundColumn DataField="ItemID" Display="False" UniqueName="ItemID" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Allergen Name" SortExpression="ItemName"
                                        UniqueName="ItemName" HeaderStyle-Width="182px" />
                                    <telerik:GridTemplateColumn UniqueName="DescAndReaction" HeaderStyle-Width="300px">
                                        <ItemTemplate>
                                            <telerik:RadTextBox ID="txtAllergenDesc" runat="server" Width="100%" MaxLength="4000"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "DescAndReaction") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Resizing AllowColumnResize="False" />
                            </ClientSettings>
                        </telerik:RadGrid>

                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
