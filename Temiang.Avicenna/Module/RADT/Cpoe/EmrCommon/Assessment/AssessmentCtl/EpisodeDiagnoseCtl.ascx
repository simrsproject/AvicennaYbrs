<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EpisodeDiagnoseCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EpisodeDiagnoseCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<telerik:RadAjaxManagerProxy ID="ajxProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdDiagnose">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function onDiagnoseNameClick(value) {
            var grid = $find("<%=grdDiagnose.ClientID%>");
            var txt = $telerik.findElement(grid.get_element(), "txtDiagnosisText");
            txt.value = value;
        }
        function onExternalCauseNameClick(value) {
            var grid = $find("<%=grdDiagnose.ClientID%>");
            var txt = $telerik.findElement(grid.get_element(), "txtExternalCauseName");
            txt.value = value;
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadGrid ID="grdDiagnose" Width="100%" GridLines="None" runat="server"
    AllowAutomaticInserts="True" PageSize="10"
    OnInsertCommand="grdDiagnose_OnInsertCommand"
    OnUpdateCommand="grdDiagnose_OnUpdateCommand"
    OnDeleteCommand="grdDiagnose_OnDeleteCommand"
    OnNeedDataSource="grdDiagnose_OnNeedDataSource"
    OnItemDataBound="grdDiagnose_OnItemDataBound"
    OnItemCommand="grdDiagnose_OnItemCommand"
    AllowAutomaticUpdates="False"
    AutoGenerateColumns="False"
    AllowMultiRowEdit="False"
    AllowAutomaticDeletes="False">

    <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="SequenceNo"
        HorizontalAlign="NotSet" AutoGenerateColumns="False">
        <CommandItemSettings AddNewRecordImageUrl="~/Images/Toolbar/insert16.png" AddNewRecordText="Add"></CommandItemSettings>
        <SortExpressions>
            <telerik:GridSortExpression FieldName="SequenceNo" SortOrder="Ascending" />
        </SortExpressions>
        <Columns>
            <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Toolbar/edit16.png" HeaderStyle-Width="30px"></telerik:GridEditCommandColumn>
            <telerik:GridTemplateColumn HeaderText="Diagnose Type" HeaderStyle-Width="150px" UniqueName="DiagnoseType">
                <ItemTemplate>
                    <%# Eval("DiagnoseType") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="ICD 10" HeaderStyle-Width="80px" UniqueName="DiagnoseID" DataField="DiagnoseID" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
            <telerik:GridTemplateColumn HeaderText="Diagnose" UniqueName="DiagnosisText" DataField="DiagnosisText">
                <ItemTemplate>
                    <%# Eval("DiagnosisText") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn HeaderText="Synonym" UniqueName="DiagnoseSynonym" DataField="DiagnoseSynonym">
                <ItemTemplate>
                    <%# Eval("DiagnoseSynonym") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridCheckBoxColumn HeaderText="Old Case" HeaderStyle-Width="80px" UniqueName="IsOldCase" DataField="IsOldCase" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn HeaderText="ExternalCauseID" HeaderStyle-Width="80px" UniqueName="ExternalCauseID" DataField="ExternalCauseID" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Display="false" />
            <telerik:GridTemplateColumn HeaderText="External Cause" UniqueName="ExternalCauseName">
                <ItemTemplate>
                    <%#string.Format("{0} {1}", Eval("ExternalCauseID"), Eval("ExternalCauseName")) %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>

            <telerik:GridCheckBoxColumn HeaderText="Void" HeaderStyle-Width="40px" UniqueName="IsVoid" DataField="IsVoid" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />

            <telerik:GridTemplateColumn HeaderText="" UniqueName="colVoidUnVoid" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Delete" OnClientClick="javascript:if (!confirm('Continue ?')) return false;"><%# Eval("IsVoid").ToBoolean()?"UnVoid":"Void" %></asp:LinkButton>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
        <EditFormSettings EditFormType="Template">
            <FormTemplate>
                <table>
                    <tr>
                        <td class="label" style="width: 100px">Type</td>
                        <td>
                            <telerik:RadDropDownList ID="cboSRDiagnoseType" runat="server" Width="200px" SelectedValue='<%# Bind("SRDiagnoseType") %>'>
                            </telerik:RadDropDownList>
                            &nbsp;
                            <telerik:RadCheckBox runat="server" ID="chkIsOldCase" Text="Old Case" Font-Italic="True" Checked='<%# Eval("IsOldCase").ToBoolean() %>' OnCheckedChanged="chkIsOldCase_CheckedChanged"></telerik:RadCheckBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 100px">ICD X</td>
                        <td>
                            <telerik:RadComboBox ID="cboDiagnoseID" runat="server" Width="300px" EmptyMessage="Select a Diagnosis" Text='<%# Bind("DiagnoseID") %>'
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true" OnSelectedIndexChanged="cboDiagnoseID_SelectedIndexChanged">
                                <WebServiceSettings Method="DiagnosePerSmf" Path="~/WebService/ComboBoxDataService.asmx" />
                                <ClientItemTemplate>
                                                    <div onclick="onDiagnoseNameClick('#= Attributes.DiagnoseName #')">
                                                        <ul class="details">
                                                            <li class="bold"><span>#= Value #</span></li>
                                                            <li class="small"><span>#= Attributes.DiagnoseName #</span></li>
                                                            <li class="smaller"><span>DTD: [#= Attributes.DtdNo #] #= Attributes.DtdName #  </span></li>
                                                        </ul>
                                                    </div>
                                </ClientItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvDiagnoseID" runat="server" ErrorMessage="Diagnose ID required."
                                ValidationGroup="EntryIcdX" ControlToValidate="cboDiagnoseID"
                                Width="24px">
                                &nbsp;<asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 100px">Diagnose</td>
                        <td>
                            <telerik:RadTextBox ID="txtDiagnosisText" runat="server" Width="300px" MaxLength="250" Text='<%# Bind("DiagnosisText") %>'
                                TextMode="MultiLine" Resize="Vertical" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvDiagnosisText" runat="server" ErrorMessage="Diagnose text required."
                                ValidationGroup="EntryIcdX" ControlToValidate="txtDiagnosisText"
                                Width="24px">
                                &nbsp;<asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSynonym" runat="server" Text="Synonym"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSynonym" runat="server" Width="300px" EmptyMessage="Select a Synonym" Text='<%# Bind("DiagnoseSynonym") %>'>
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 100px">External Cause</td>
                        <td>
                            <telerik:RadComboBox ID="cboExternalCauseID" runat="server" Width="300px" EmptyMessage="Select a External Cause" Text='<%# Bind("ExternalCauseID") %>'
                                EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                                <WebServiceSettings Method="ExternalCauseDiagnose" Path="~/WebService/ComboBoxDataService.asmx" />
                                <ClientItemTemplate>
                                                    <div style="cursor:pointer;" onclick="onExternalCauseNameClick('#= Attributes.DiagnoseName #')">
                                                        <ul class="details">
                                                            <li class="bold"><span>#= Value #</span></li>
                                                            <li class="small"><span>#= Attributes.DiagnoseName #</span></li>
                                                            <li class="smaller"><span>DTD: [#= Attributes.DtdNo #] #= Attributes.DtdName #  </span></li>
                                                        </ul>
                                                    </div>
                                </ClientItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <telerik:RadTextBox ID="txtExternalCauseName" runat="server" Width="300px" Text='<%# ExternalCauseNameValue(Container) %>'
                                TextMode="MultiLine" Resize="Vertical" ReadOnly="true" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnUpdate" ValidationGroup="EntryIcdX" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                CommandName="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </FormTemplate>
        </EditFormSettings>
    </MasterTableView>
</telerik:RadGrid>

