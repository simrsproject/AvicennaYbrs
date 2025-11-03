<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NutritionCareStandardDetailEvaluation.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare.NutritionCareStandardDetailEvaluation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="NutritionCareDiagnoseTransDT" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="NutritionCareDiagnoseTransDT"
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
                        <asp:RequiredFieldValidator ID="rfvDateTimeImplementation" runat="server" ErrorMessage="Nutrition Care implementation date time required."
                            ValidationGroup="NutritionCareDiagnoseTransDT" ControlToValidate="txtDateTimeImplementation" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMonitoringEvaluation" runat="server" Text="Monitoring & Evaluation"></asp:Label>
                    </td>
                    <td class="entry">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <asp:HiddenField ID="hfTmpTerminologyID" runat="server" />
                        <telerik:RadTextBox ID="txtMonitoringEvaluation" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 50%;" valign="top">
            <div style="width: 100%; height: 250px; overflow: scroll">
                <telerik:RadGrid ID="gridListRencana" runat="server" AutoGenerateColumns="false"
                    GridLines="None" OnNeedDataSource="gridListRencana_NeedDataSource">
                    <MasterTableView ClientDataKeyNames="TerminologyID"
                        HierarchyDefaultExpanded="true" ExpandCollapseColumn-Display="false" DataKeyNames="TerminologyID"
                        GroupLoadMode="Client">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="DomainName" HeaderText=" " HeaderValueSeparator=" "></telerik:GridGroupByField>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="DomainSeqNo" SortOrder="Ascending"></telerik:GridGroupByField>
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Select" UniqueName="ChkBox" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="defaultChkBox" runat="server"
                                        Checked='<%#!Eval("TransTerminologyID").ToString().Equals(string.Empty)%>'></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Detail" UniqueName="IsDetail" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsDetail" runat="server" Checked='<%#Eval("IsDetail")%>'></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="TransTerminologyID" UniqueName="IsTransTerminologyID" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkTransTerminologyID" runat="server"
                                        Checked='<%#!Eval("TransTerminologyID").ToString().Equals(string.Empty)%>'></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="TerminologyID" HeaderText="Code" SortExpression="TerminologyID"
                                UniqueName="TerminologyID">
                                <HeaderStyle HorizontalAlign="Left" Width="100" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TerminologyNameEdited" HeaderText="Intervention" SortExpression="TerminologyNameEdited"
                                UniqueName="TerminologyNameEdited">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Intervention" UniqueName="TerminologyNameEditedTxt" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txtTerminologyName" runat="server" Text='<%#Eval("TerminologyNameEdited")%>'
                                        Width="100%" MaxLength="255" onkeyup="AutoRes(this);" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <%--<telerik:GridTemplateColumn HeaderText="Intervention" UniqueName="TerminologyNameEdited">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtTerminologyName" runat="server" Text='<%#Eval("TerminologyNameEdited")%>'
                                            Width="100%" MaxLength="255" TextMode="MultiLine" Rows="1" onkeyup="AutoRes(this);" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn DataField="TerminologyName" HeaderText="NIC" SortExpression="TerminologyName"
                                UniqueName="TerminologyName" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TerminologyParentID" HeaderText="Parent"
                                SortExpression="TerminologyParentID" UniqueName="TerminologyParentID"
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
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="NutritionCareDiagnoseTransDT"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="NutritionCareDiagnoseTransDT" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
