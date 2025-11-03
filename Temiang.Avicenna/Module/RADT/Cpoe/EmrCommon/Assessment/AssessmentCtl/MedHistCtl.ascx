<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.MedHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<table style="width: 100%; padding: 0 0 0 0;">
    <tr>
        <td style="width: 50%;">
            <table style="width: 100%;">
                <tr>
                    <td valign="top" class="label">Past Medical History</td>
                    <td>
                        <telerik:RadGrid ID="grdPastMedicalHist" Width="100%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                            GridLines="None" AllowMultiRowSelection="True" Skin=""
                            OnNeedDataSource="grdPastMedicalHist_NeedDataSource" OnItemDataBound="grdPastMedicalHist_ItemDataBound">
                            <MasterTableView DataKeyNames="ItemID" ShowHeader="False" ShowHeadersWhenNoRecords="false" Width="100%">
                                <Columns>
                                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1" HeaderStyle-Width="30px">
                                    </telerik:GridClientSelectColumn>
                                    <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                                    <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Past Medical History" HeaderStyle-Width="150px" />
                                    <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" HeaderText="Notes" />
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
                                <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True"></Selecting>
                                <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                            </ClientSettings>
                        </telerik:RadGrid>
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
                    <td valign="top" class="label">Family Medical History</td>
                    <td>
                        <telerik:RadGrid ID="grdFamilyMedicalHist" Width="100%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                            GridLines="None" AllowMultiRowSelection="True" Skin=""
                            OnNeedDataSource="grdFamilyMedicalHist_NeedDataSource" OnItemDataBound="grdFamilyMedicalHist_ItemDataBound">
                            <MasterTableView DataKeyNames="ItemID" ShowHeader="False" ShowHeadersWhenNoRecords="True" Width="100%">
                                <Columns>
                                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1" HeaderStyle-Width="30px">
                                    </telerik:GridClientSelectColumn>
                                    <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                                    <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="Family Medical History" HeaderStyle-Width="150px" />
                                    <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" HeaderText="Notes" />
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
                                <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True"></Selecting>
                                <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
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
