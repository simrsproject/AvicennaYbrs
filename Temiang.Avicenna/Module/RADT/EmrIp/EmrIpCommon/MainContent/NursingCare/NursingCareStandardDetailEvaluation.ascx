<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NursingCareStandardDetailEvaluation.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardDetailEvaluation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="NursingDiagnosaTransDT" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NursingDiagnosaTransDT"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%;" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Date Time"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDateTimePicker ID="txtDateTimeImplementation" runat="server" AutoPostBackControl="None">
                            <DateInput ID="DateInput1" runat="server"
                                DisplayDateFormat="dd/MM/yyyy HH:mm"
                                DateFormat="dd/MM/yyyy HH:mm">
                            </DateInput>
                            <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm"></TimeView>
                        </telerik:RadDateTimePicker>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDateTimeImplementation" runat="server" ErrorMessage="Nursing implementation date time required."
                            ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNursingDiagnosaID" runat="server" Text="S"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <asp:HiddenField ID="hfTmpNursingDiagnosaID" runat="server" />
                        <telerik:RadTextBox ID="txtS" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label3" runat="server" Text="O"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtO" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Objective (O) required."
                            ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtO" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label4" runat="server" Text="A"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtA" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Assessment (A) required."
                            ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="txtA" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label5" runat="server" Text="P"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboP" runat="server" Width="304px"
                            AutoPostBack="true" OnSelectedIndexChanged="cboP_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Planning (P) required."
                            ValidationGroup="NursingDiagnosaTransDT" ControlToValidate="cboP" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="PPA Ins"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPpaInstruction" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 50%;" valign="top">
            <div style="width: 100%; height: 250px; overflow: scroll">
                <telerik:RadGrid ID="gridListRencana" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListRencana_NeedDataSource" OnItemDataBound="gridListRencana_ItemDataBound">
                    <MasterTableView ClientDataKeyNames="NursingDiagnosaID"
                        HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="NursingDiagnosaID"
                        GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="SRNursingNicTypeName" HeaderText=" " HeaderValueSeparator=" "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="SRNursingNicType" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Select" UniqueName="ChkBox">
                                <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="defaultChkBox" runat="server"
                                        Checked='<%#!Eval("TransNursingDiagnosaID").ToString().Equals(string.Empty)%>'
                                        Enabled='<%#Eval("TransNursingDiagnosaID").ToString().Equals(string.Empty)%>'></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Stop" UniqueName="ChkBoxStop">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkStop" runat="server"
                                        Checked='<%#(bool)Eval("Status")%>'
                                        Visible='<%#!Eval("TransNursingDiagnosaID").ToString().Equals(string.Empty)%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="NIC" UniqueName="NursingDiagnosaNameEdited">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtNursingDiagnosaName" runat="server" Text='<%#Eval("NursingDiagnosaNameEdited")%>'
                                        Width="100%" MaxLength="255" TextMode="MultiLine" Rows="1" onkeyup="AutoRes(this);" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaName" HeaderText="NIC" SortExpression="NursingDiagnosaName"
                                UniqueName="NursingDiagnosaName" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NursingDiagnosaParentID" HeaderText="Parent"
                                SortExpression="NursingDiagnosaParentID" UniqueName="NursingDiagnosaParentID"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="NursingDiagnosaTransDT"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="NursingDiagnosaTransDT" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>

